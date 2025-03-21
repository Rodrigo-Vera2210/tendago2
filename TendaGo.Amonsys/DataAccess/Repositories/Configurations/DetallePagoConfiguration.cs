﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class DetallePagoConfiguration : IEntityTypeConfiguration<DetallePago>
    {
        public void Configure(EntityTypeBuilder<DetallePago> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdPagoCredito)
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
            entity.Property(e => e.UsuarioIngreso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCuentaPorPagarNavigation).WithMany(p => p.DetallePago)
                .HasForeignKey(d => d.IdCuentaPorPagar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePago_CuentaPorPagar");

            entity.HasOne(d => d.IdPagoCreditoNavigation).WithMany(p => p.DetallePago)
                .HasForeignKey(d => d.IdPagoCredito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetallePago_PagoCredito");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DetallePago> entity);
    }
}
