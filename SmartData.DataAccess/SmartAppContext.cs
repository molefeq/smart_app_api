using Microsoft.EntityFrameworkCore;
using SmartData.DataAccess.Models;

namespace SmartData.DataAccess
{
    public partial class SmartAppContext : DbContext
    {
        public SmartAppContext()
        {
        }

        public SmartAppContext(DbContextOptions<SmartAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<DeviceDetail> DeviceDetail { get; set; }
        public virtual DbSet<DeviceStatus> DeviceStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account", "security");

                entity.HasIndex(e => e.Username)
                    .HasName("ck_account_username")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('security.account_id_seq'::regclass)");

                entity.Property(e => e.ContactNumber)
                    .HasColumnName("contact_number")
                    .HasMaxLength(20);

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.DisableDate)
                    .HasColumnName("disable_date")
                    .HasColumnType("date");

                entity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasColumnName("email_address")
                    .HasMaxLength(500);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(200);

                entity.Property(e => e.IsFirstTimeLogin).HasColumnName("is_first_time_login");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(200);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password");

                entity.Property(e => e.PasswordResetKey).HasColumnName("password_reset_key");

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasColumnName("password_salt");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_country_country_id");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.InverseCreateUser)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("fk_account_create_user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_account_role_roleid");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.HasIndex(e => e.Code)
                    .HasName("ck_country_code")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role", "security");

                entity.HasIndex(e => e.Code)
                    .HasName("ck_role_code")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('security.role_id_seq'::regclass)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<DeviceDetail>(entity =>
            {
                entity.ToTable("device_detail", "device");

                entity.HasIndex(e => e.SerailNumber)
                    .HasName("ck_device_detail_serail_number")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('device.device_detail_id_seq'::regclass)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");
                
                entity.Property(e => e.DeviceName)
                    .IsRequired()
                    .HasColumnName("device_name")
                    .HasMaxLength(200);

                entity.Property(e => e.LinkedUserId).HasColumnName("linked_user_id");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnName("modified_date")
                    .HasColumnType("date");

                entity.Property(e => e.ModifiedUserId).HasColumnName("modified_user_id");

                entity.Property(e => e.SerailNumber)
                    .IsRequired()
                    .HasColumnName("serail_number")
                    .HasMaxLength(200);

                entity.HasOne(d => d.CreateUser)
                    .WithMany()
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_device_detail_create_user_id");

                entity.HasOne(d => d.LinkedUser)
                    .WithMany(p => p.LinkedDevices)
                    .HasForeignKey(d => d.LinkedUserId)
                    .HasConstraintName("fk_device_detail_linked_user_id");

                entity.HasOne(d => d.ModifiedUser)
                    .WithMany()
                    .HasForeignKey(d => d.ModifiedUserId)
                    .HasConstraintName("fk_device_detail_modified_user_id");
            });

            modelBuilder.Entity<DeviceStatus>(entity =>
            {
                entity.ToTable("device_status", "device");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('device.device_status_id_seq'::regclass)");

                entity.Property(e => e.Code)
                    .HasColumnName("code")
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(200);
            });

            modelBuilder.HasSequence("device_detail_id_seq");

            modelBuilder.HasSequence<int>("device_status_id_seq");

            modelBuilder.HasSequence("account_id_seq");

            modelBuilder.HasSequence("role_id_seq");
        }
    }
}

