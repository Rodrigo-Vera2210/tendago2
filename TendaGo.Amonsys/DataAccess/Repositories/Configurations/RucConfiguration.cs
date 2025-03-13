﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class RucConfiguration : IEntityTypeConfiguration<Ruc>
    {
        public void Configure(EntityTypeBuilder<Ruc> entity)
        {
            entity.HasKey(e => e.Ruc1);

            entity.Property(e => e.Ruc1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Ruc");
            entity.Property(e => e.ActividadEconomica)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IpIngreso)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.IpModificacion)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.LimiteFacturacion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Rise).HasColumnName("RISE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TokenFactElectonica).IsUnicode(false);
            entity.Property(e => e.TotalFacturado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioIngreso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Ruc)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ruc_Empresa");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Ruc> entity);
    }
}
