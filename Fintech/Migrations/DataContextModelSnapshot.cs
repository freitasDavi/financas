﻿// <auto-generated />
using System;
using Fintech.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fintech.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Fintech.Entities.DespesaParcelada", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CodigoUsuario")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NumeroParcelas")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalParcela")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("ValorParcela")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CodigoUsuario");

                    b.ToTable("despesas_parceladas", (string)null);
                });

            modelBuilder.Entity("Fintech.Entities.DespesaProgramada", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("CodigoUsuario")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DataFinal")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataInicial")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CodigoUsuario");

                    b.ToTable("despesas_programadas", (string)null);
                });

            modelBuilder.Entity("Fintech.Entities.Despesas", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("CodigoDespesaParcelada")
                        .HasColumnType("bigint");

                    b.Property<long?>("CodigoDespesaProgramada")
                        .HasColumnType("bigint");

                    b.Property<long>("CodigoUsuario")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("FormaDePagamento")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Origem")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CodigoDespesaParcelada");

                    b.HasIndex("CodigoDespesaProgramada");

                    b.HasIndex("CodigoUsuario");

                    b.ToTable("despesas", (string)null);
                });

            modelBuilder.Entity("Fintech.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Premium")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Fintech.Entities.DespesaParcelada", b =>
                {
                    b.HasOne("Fintech.Entities.User", "Usuario")
                        .WithMany("DespesaParcelada")
                        .HasForeignKey("CodigoUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Fintech.Entities.DespesaProgramada", b =>
                {
                    b.HasOne("Fintech.Entities.User", "Usuario")
                        .WithMany("DespesaProgramada")
                        .HasForeignKey("CodigoUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Fintech.Entities.Despesas", b =>
                {
                    b.HasOne("Fintech.Entities.DespesaParcelada", "DespesaParcelada")
                        .WithMany("Despesas")
                        .HasForeignKey("CodigoDespesaParcelada");

                    b.HasOne("Fintech.Entities.DespesaProgramada", "DespesaProgramada")
                        .WithMany("Despesas")
                        .HasForeignKey("CodigoDespesaProgramada");

                    b.HasOne("Fintech.Entities.User", "Usuario")
                        .WithMany("Despesas")
                        .HasForeignKey("CodigoUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DespesaParcelada");

                    b.Navigation("DespesaProgramada");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Fintech.Entities.DespesaParcelada", b =>
                {
                    b.Navigation("Despesas");
                });

            modelBuilder.Entity("Fintech.Entities.DespesaProgramada", b =>
                {
                    b.Navigation("Despesas");
                });

            modelBuilder.Entity("Fintech.Entities.User", b =>
                {
                    b.Navigation("DespesaParcelada");

                    b.Navigation("DespesaProgramada");

                    b.Navigation("Despesas");
                });
#pragma warning restore 612, 618
        }
    }
}
