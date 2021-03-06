﻿// <auto-generated />
using System;
using Hexagon.Data.EF.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hexagon.Data.EF.Migrations
{
    [DbContext(typeof(SqliteDbContext))]
    partial class SqliteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("Hexagon.Data.Entity.Organization_Employee", b =>
                {
                    b.Property<long>("Employee_Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("Certificate_Code");

                    b.Property<string>("Certificate_Type");

                    b.Property<string>("Contact_Phone");

                    b.Property<DateTime?>("Created_Date");

                    b.Property<string>("Driver_Phone");

                    b.Property<string>("Duty_Desc");

                    b.Property<string>("ESPACE_ACCOUNT");

                    b.Property<string>("Email");

                    b.Property<string>("Employee_Name");

                    b.Property<string>("Gender");

                    b.Property<string>("HeadIcon");

                    b.Property<string>("Home_Address");

                    b.Property<string>("Home_Telephone");

                    b.Property<int?>("Isvalid");

                    b.Property<DateTime?>("Modified_Date");

                    b.Property<string>("Note");

                    b.Property<string>("Office_Fax");

                    b.Property<string>("Office_Telephone1");

                    b.Property<string>("Office_Telephone2");

                    b.Property<string>("Organization_Id");

                    b.Property<string>("Room_Num");

                    b.Property<string>("SERVICE_NUMBER");

                    b.Property<string>("Secretary_Phone");

                    b.Property<int?>("Uc_Enable");

                    b.HasKey("Employee_Id");

                    b.ToTable("Organization_Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
