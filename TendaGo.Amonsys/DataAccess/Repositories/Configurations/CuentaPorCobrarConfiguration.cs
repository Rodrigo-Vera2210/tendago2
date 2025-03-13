﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class CuentaPorCobrarConfiguration : IEntityTypeConfiguration<CuentaPorCobrar>
    {
        public void Configure(EntityTypeBuilder<CuentaPorCobrar> entity)
        {
            entity.HasIndex(e => new { e.IdEstado, e.IdSalida }, "IX_CuentaPorCobrar_01");

            entity.HasIndex(e => new { e.IdSalida, e.Numero, e.FechaModificacion }, "UQ_CuentaPorCobrar").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaVencimiento).HasColumnType("date");
            entity.Property(e => e.IdSalida)
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
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UsuarioIngreso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdSalidaNavigation).WithMany(p => p.CuentaPorCobrar)
                .HasForeignKey(d => d.IdSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CuentaPorCobrar_Salida");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CuentaPorCobrar> entity);
    }
}
