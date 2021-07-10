﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProspectosAPI.Models;

namespace ProspectosAPI.Migrations
{
    [DbContext(typeof(ProspectoContext))]
    partial class ProspectoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProspectosAPI.Models.Prospectos", b =>
                {
                    b.Property<int>("IdProspecto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CP")
                        .HasColumnType("varchar(5)");

                    b.Property<string>("Calle")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Colonia")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Estatus")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NumeroCalle")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("Observaciones")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Rfc")
                        .HasColumnType("varchar(13)");

                    b.Property<string>("Telefono")
                        .HasColumnType("varchar(10)");

                    b.HasKey("IdProspecto");

                    b.ToTable("Prospectos");
                });
#pragma warning restore 612, 618
        }
    }
}
