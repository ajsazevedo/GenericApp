﻿// <auto-generated />
using System;
using GenericApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GenericApp.Infra.Data.Migrations.Migrations
{
    [DbContext(typeof(GenericAppContext))]
    [Migration("20210217070435_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GenericApp.Domain.Models.JuridicalPerson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cnpj")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublicName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("JuridicalPerson");

                    b.HasDiscriminator<string>("Discriminator").HasValue("JuridicalPerson");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Person", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Person");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Company", b =>
                {
                    b.HasBaseType("GenericApp.Domain.Models.JuridicalPerson");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.ToTable("Company");

                    b.HasDiscriminator().HasValue("Company");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Employee", b =>
                {
                    b.HasBaseType("GenericApp.Domain.Models.Person");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Admission")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CompanyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employee");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("GenericApp.Domain.Models.Employee", b =>
                {
                    b.HasOne("GenericApp.Domain.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
