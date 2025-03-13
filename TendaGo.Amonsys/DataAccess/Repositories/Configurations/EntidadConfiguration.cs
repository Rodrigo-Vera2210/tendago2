﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class EntidadConfiguration : IEntityTypeConfiguration<Entidad>
    {
        public void Configure(EntityTypeBuilder<Entidad> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Celular)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.EsCliente).HasDefaultValue(true);
            entity.Property(e => e.EsConsumidorFinal).HasDefaultValue(false);
            entity.Property(e => e.EsProveedor).HasDefaultValue(false);
            entity.Property(e => e.EstadoCivil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaNacimiento).HasColumnType("date");
            entity.Property(e => e.Genero)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Identificacion)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IpIngreso)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.IpModificacion)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Latitud).HasMaxLength(150);
            entity.Property(e => e.Longitud).HasMaxLength(150);
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NivelEstudio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Observaciones)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Profesion)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .IsRequired()
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoEntidad)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioIngreso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Entidad)
                .HasForeignKey(d => d.IdCiudad)
                .HasConstraintName("FK_Entidad_Ciudad");

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Entidad)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK_Entidad_Pais");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Entidad)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK_Entidad_Provincia");

            entity.HasOne(d => d.IdSectorNavigation).WithMany(p => p.Entidad)
                .HasForeignKey(d => d.IdSector)
                .HasConstraintName("FK_Entidad_Sector");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Entidad> entity);
    }
}
