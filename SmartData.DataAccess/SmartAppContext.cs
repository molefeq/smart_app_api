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
        public virtual DbSet<ExchangeRate> ExchangeRate { get; set; }
        public virtual DbSet<PaymentDetail> PaymentDetail { get; set; }
        public virtual DbSet<TopupOption> TopupOption { get; set; }
        public virtual DbSet<Tier> Tier { get; set; }
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
                    .WithMany()
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

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.IsUcloudEnabled).HasColumnName("is_ucloud_enabled");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100);

                entity.Property(e => e.TierId).HasColumnName("tier_id");

                entity.HasOne(d => d.Currency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("fk_country_currency_id");

                entity.HasOne(d => d.Tier)
                    .WithMany()
                    .HasForeignKey(d => d.TierId)
                    .HasConstraintName("fk_country_tier");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("currency");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(10);

                entity.Property(e => e.Symbol)
                    .HasColumnName("symbol")
                    .HasMaxLength(4);
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
            
            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.ToTable("exchange_rate");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BaseCurrencyId).HasColumnName("base_currency_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("date");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("numeric");

                entity.HasOne(d => d.BaseCurrency)
                    .WithMany()
                    .HasForeignKey(d => d.BaseCurrencyId)
                    .HasConstraintName("fk_exchange_rate_base_currency_id");

                entity.HasOne(d => d.Country)
                    .WithMany()
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_exchange_rate_country_id");
            });

            modelBuilder.Entity<PaymentDetail>(entity =>
            {
                entity.ToTable("payment_detail", "payment");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('payment.payment_detail_id_seq'::regclass)");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("date");

                entity.Property(e => e.CreateUserId).HasColumnName("create_user_id");

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.IsPaymentSuccessful)
                    .IsRequired()
                    .HasColumnName("is_payment_successful")
                    .HasDefaultValueSql("true");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.HasOne(d => d.CreateUser)
                    .WithMany()
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_detail_create_user_id");

                entity.HasOne(d => d.Currency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payment_detail_currency_id");
            });

            modelBuilder.Entity<TopupOption>(entity =>
            {
                entity.ToTable("topup_option");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("money");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("create_date")
                    .HasColumnType("date");

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.DataQuantity).HasColumnName("data_quantity");

                entity.Property(e => e.DataQuantityDescription)
                    .IsRequired()
                    .HasColumnName("data_quantity_description")
                    .HasMaxLength(100);

                entity.Property(e => e.DataScale)
                    .IsRequired()
                    .HasColumnName("data_scale")
                    .HasMaxLength(10);

                entity.Property(e => e.TierId).HasColumnName("tier_id");

                entity.HasOne(d => d.Currency)
                    .WithMany()
                    .HasForeignKey(d => d.CurrencyId)
                    .HasConstraintName("fk_topup_option_currency_id");

                entity.HasOne(d => d.Tier)
                    .WithMany(p => p.TopupOption)
                    .HasForeignKey(d => d.TierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_topup_option_tier");
            });

            modelBuilder.Entity<Tier>(entity =>
            {
                entity.ToTable("tier");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TierDescription)
                    .IsRequired()
                    .HasColumnName("tier_description")
                    .HasMaxLength(100);
            });

            modelBuilder.HasSequence("device_detail_id_seq");

            modelBuilder.HasSequence<int>("device_status_id_seq");

            modelBuilder.HasSequence("account_id_seq");

            modelBuilder.HasSequence("role_id_seq");

            modelBuilder.HasSequence("payment_detail_id_seq");
        }
    }
}

