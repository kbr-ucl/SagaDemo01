﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleBatch.Components;

#nullable disable

namespace SampleBatch.Service.Migrations
{
    [DbContext(typeof(SampleBatchDbContext))]
    [Migration("20230301143107_MyFirstMigration")]
    partial class MyFirstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SampleBatch.Components.StateMachines.BatchJobState", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BatchJobId");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("BatchId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExceptionMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("ReceiveTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("CorrelationId");

                    b.HasIndex("BatchId");

                    b.ToTable("JobStates");
                });

            modelBuilder.Entity("SampleBatch.Components.StateMachines.BatchState", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("BatchId");

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ActiveThreshold")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProcessingOrderIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReceiveTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ScheduledId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Total")
                        .HasColumnType("int");

                    b.Property<string>("UnprocessedOrderIds")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTimestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("CorrelationId");

                    b.ToTable("BatchStates");
                });

            modelBuilder.Entity("SampleBatch.Components.StateMachines.BatchJobState", b =>
                {
                    b.HasOne("SampleBatch.Components.StateMachines.BatchState", "Batch")
                        .WithMany("Jobs")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batch");
                });

            modelBuilder.Entity("SampleBatch.Components.StateMachines.BatchState", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
