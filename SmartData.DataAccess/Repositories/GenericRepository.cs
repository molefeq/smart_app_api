
using Microsoft.EntityFrameworkCore;

using Npgsql;
using SqsLibraries.Common.Enums;
using SqsLibraries.Common.Extensions;
using SqsLibraries.Common.Utilities.ResponseObjects;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartData.DataAccess.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal SmartAppContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository() { }

        public GenericRepository(SmartAppContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public SmartAppContext Context
        {
            set
            {
                context = value;
                this.dbSet = context.Set<TEntity>();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntitiesAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            PageData pageData = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = Include(query, includeProperties);
            }

            if (orderBy != null && pageData != null && !pageData.IncludeAllData)
            {
                return await orderBy(query).Skip(pageData.Skip.Value).Take(pageData.Take.Value).ToListAsync<TEntity>();
            }
            else if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            PageData pageData = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = Include(query, includeProperties);
            }

            if (orderBy != null && pageData != null && !pageData.IncludeAllData)
            {
                return orderBy(query).Skip(pageData.Skip.Value).Take(pageData.Take.Value).ToList<TEntity>();
            }
            else if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual int CountEntities(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }

        public virtual async Task<int> CountEntitiesAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

        public virtual TEntity GetById(Expression<Func<TEntity, bool>> filter)
        {
            return GetById(filter, "");
        }

        public virtual TEntity GetById(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = Include(query, includeProperties);
            }

            return query.Where(filter).FirstOrDefault<TEntity>();
        }

        public Result<TEntity> GetPagedEntities(IQueryable<TEntity> query, PageData pageData, string includeProperties = null)
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = Include(query, includeProperties);
            }

            if (typeof(TEntity).GetProperty(pageData.SortColumn) != null)
            {
                if (pageData.SortOrder == SortOrder.DESC)
                {
                    query = query.OrderByDescending(item => item.GetType().GetProperty(pageData.SortColumn).GetValue(item));
                }
                else
                {
                    query = query.OrderBy(item => item.GetType().GetProperty(pageData.SortColumn).GetValue(item));
                }
            }

            if (pageData != null && !pageData.IncludeAllData)
            {
                return new Result<TEntity>
                {
                    Items = query.Skip(pageData.Skip.Value).Take(pageData.Take.Value).ToList(),
                    TotalItems = query.Count()
                };
            }

            return new Result<TEntity>
            {
                Items = query.ToList(),
                TotalItems = query.Count()
            };
        }

        public virtual async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                query = Include(query, includeProperties);
            }

            return await query.Where(filter).FirstOrDefaultAsync<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);

            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);
        }

        public virtual void DeleteEntries(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return;
            }

            var detachedEntities = entities.Where(item => context.Entry(item).State == EntityState.Detached);

            if (entities != null && entities.Count > 0)
            {
                dbSet.AttachRange(detachedEntities);
            }

            dbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Update(entityToUpdate);
        }

        public virtual DbDataReader ExcuteReader(string commandText, params NpgsqlParameter[] parameters)
        {
            if (context.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                context.Database.GetDbConnection().Open();
            }

            using (DbCommand dbCommand = context.Database.GetDbConnection().CreateCommand())
            {
                dbCommand.CommandText = commandText;
                dbCommand.CommandType = CommandType.StoredProcedure;

                SetCommandParameters(dbCommand, parameters);

                return dbCommand.ExecuteReader();
            }
        }

        public virtual DbDataReader ExcuteReaderResultSet(string commandText, PageData pageData, params NpgsqlParameter[] parameters)
        {
            if (context.Database.GetDbConnection().State == ConnectionState.Closed)
            {
                context.Database.GetDbConnection().Open();
            }

            using (DbCommand dbCommand = context.Database.GetDbConnection().CreateCommand())
            {
                dbCommand.CommandTimeout = 300;
                dbCommand.CommandText = commandText;
                dbCommand.CommandType = CommandType.StoredProcedure;

                SetPagingParamaters(dbCommand, pageData);
                SetCommandParameters(dbCommand, parameters);

                return dbCommand.ExecuteReader();
            }
        }

        public virtual IQueryable<TEntity> Include(IQueryable<TEntity> query, string includeList)
        {
            foreach (var includeProperty in includeList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            return query;
        }

        public NpgsqlParameter AddParameter(string parameterName, object parameterValue, DbType dbType, ParameterDirection parameterDirection = ParameterDirection.Input, bool isNullable = false)
        {
            return new NpgsqlParameter
            {
                ParameterName = parameterName,
                DbType = dbType,
                Value = parameterValue ?? (object)DBNull.Value,
                Direction = parameterDirection,
                IsNullable = isNullable
            };
        }

        public virtual void Dispose()
        {
            context.Dispose();
        }

        #region Private Method

        private void SetPagingParamaters(DbCommand dbCommand, PageData pageData)
        {
            if (pageData == null)
            {
                return;
            }

            dbCommand.Parameters.Add(new NpgsqlParameter { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "Skip", Value = pageData.Skip, IsNullable = true });
            dbCommand.Parameters.Add(new NpgsqlParameter { DbType = DbType.Int32, Direction = ParameterDirection.Input, ParameterName = "Take", Value = pageData.Take, IsNullable = true });
            dbCommand.Parameters.Add(new NpgsqlParameter { DbType = DbType.String, Size = 100, Direction = ParameterDirection.Input, ParameterName = "SortColumn", Value = pageData.SortColumn ?? (object)DBNull.Value, IsNullable = true });
            dbCommand.Parameters.Add(new NpgsqlParameter { DbType = DbType.String, Size = 10, Direction = ParameterDirection.Input, ParameterName = "SortOrder", Value = pageData.SortOrder.ToSqlString(), IsNullable = true });
            dbCommand.Parameters.Add(new NpgsqlParameter { DbType = DbType.Boolean, Direction = ParameterDirection.Input, ParameterName = "IncludeAllDataInd", Value = pageData.IncludeAllData, IsNullable = true });
        }

        private void SetCommandParameters(DbCommand dbCommand, NpgsqlParameter[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                return;
            }

            foreach (NpgsqlParameter parameter in parameters)
            {
                dbCommand.Parameters.Add(parameter);
            }
        }

        #endregion
    }
}