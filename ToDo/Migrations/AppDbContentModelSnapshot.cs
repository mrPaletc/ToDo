﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ToDo;

namespace ToDo.Migrations
{
    [DbContext(typeof(AppDbContent))]
    partial class AppDbContentModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ToDo.Models.MyTask", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("listOfPerformers")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("masterTaskid")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("planedTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("realTime")
                        .HasColumnType("time");

                    b.Property<DateTime>("registrationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("masterTaskid");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("ToDo.Models.MyTask", b =>
                {
                    b.HasOne("ToDo.Models.MyTask", "masterTask")
                        .WithMany("subTasks")
                        .HasForeignKey("masterTaskid");

                    b.Navigation("masterTask");
                });

            modelBuilder.Entity("ToDo.Models.MyTask", b =>
                {
                    b.Navigation("subTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
