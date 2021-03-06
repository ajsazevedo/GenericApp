// <auto-generated />
using System;
using GenericApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GenericApp.Infra.Data.Migrations.Migrations
{
    [DbContext(typeof(GenericAppContext))]
    partial class GenericAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GenericApp.Domain.Models.JuridicalPerson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cnpj")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("PublicName")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime2");

                    b.Property<long>("creator_id")
                        .HasColumnType("bigint");

                    b.Property<long?>("updater_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("creator_id");

                    b.HasIndex("updater_id");

                    b.ToTable("JuridicalPerson");

                    b.HasDiscriminator<string>("Discriminator").HasValue("JuridicalPerson");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime2");

                    b.Property<long>("CustomerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long?>("creator_id")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<long?>("updater_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("creator_id");

                    b.HasIndex("updater_id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime2");

                    b.Property<long?>("creator_id")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<long?>("updater_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("creator_id");

                    b.HasIndex("updater_id");

                    b.ToTable("Person");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnName("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnName("created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PasswordValidDate")
                        .HasColumnName("password_valid_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnName("role")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnName("updated_at")
                        .HasColumnType("datetime2");

                    b.Property<long?>("creator_id")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<long?>("updater_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("creator_id");

                    b.HasIndex("updater_id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Company", b =>
                {
                    b.HasBaseType("GenericApp.Domain.Models.JuridicalPerson");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.HasIndex("Cnpj")
                        .IsUnique()
                        .HasFilter("[Cnpj] IS NOT NULL");

                    b.ToTable("Company");

                    b.HasDiscriminator().HasValue("Company");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Customer", b =>
                {
                    b.HasBaseType("GenericApp.Domain.Models.Person");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasFilter("[Cpf] IS NOT NULL");

                    b.ToTable("Customer");

                    b.HasDiscriminator().HasValue("Customer");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Employee", b =>
                {
                    b.HasBaseType("GenericApp.Domain.Models.Person");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Admission")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(8)")
                        .HasMaxLength(8);

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("CompanyId");

                    b.HasIndex("Cpf")
                        .IsUnique()
                        .HasName("IX_Person_Cpf1")
                        .HasFilter("[Cpf] IS NOT NULL");

                    b.ToTable("Employee");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.JuridicalPerson", b =>
                {
                    b.HasOne("GenericApp.Domain.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("creator_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GenericApp.Domain.Models.User", "Updater")
                        .WithMany()
                        .HasForeignKey("updater_id")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Order", b =>
                {
                    b.HasOne("GenericApp.Domain.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GenericApp.Domain.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("creator_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GenericApp.Domain.Models.User", "Updater")
                        .WithMany()
                        .HasForeignKey("updater_id")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Person", b =>
                {
                    b.HasOne("GenericApp.Domain.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("creator_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GenericApp.Domain.Models.User", "Updater")
                        .WithMany()
                        .HasForeignKey("updater_id")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("GenericApp.Domain.Models.User", b =>
                {
                    b.HasOne("GenericApp.Domain.Models.User", "Creator")
                        .WithMany()
                        .HasForeignKey("creator_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GenericApp.Domain.Models.User", "Updater")
                        .WithMany()
                        .HasForeignKey("updater_id")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Employee", b =>
                {
                    b.HasOne("GenericApp.Domain.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
