﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class SalidaConfiguration : IEntityTypeConfiguration<Salida>
    {
        public void Configure(EntityTypeBuilder<Salida> entity)
        {
            entity.ToTable(tb => tb.HasTrigger("TR_Salida_Estados"));

            entity.HasIndex(e => e.IdEstado, "IDX_SALIDA_FECHA");

            entity.HasIndex(e => new { e.IdEmpresa, e.TipoTransaccion, e.Id }, "IX_Salida_01");

            entity.HasIndex(e => new { e.IdEmpresa, e.IdLocal, e.TipoTransaccion, e.EstadoActual }, "IX_Salida_02");

            entity.HasIndex(e => e.TransaccionPadre, "IX_TransaccionPadre");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cuotas).HasDefaultValue(1);
            entity.Property(e => e.Descuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Entrega)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.EstadoActual)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaHoraEntregaPropuesta).HasColumnType("datetime");
            entity.Property(e => e.FechaHoraEntregaReal).HasColumnType("datetime");
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdVendedor)
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
            entity.Property(e => e.Observaciones).IsUnicode(false);
            entity.Property(e => e.Periodo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.Ruc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Subtotal0).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SubtotalIva).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TipoTransaccion)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.TipoTransaccionPadre)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalIva)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TotalIVA");
            entity.Property(e => e.TransaccionPadre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioIngreso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ValorAdicional).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Salida)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_Salida_Entidad");

            entity.HasOne(d => d.IdLocalNavigation).WithMany(p => p.Salida)
                .HasForeignKey(d => d.IdLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salida_Local");

            entity.HasOne(d => d.RucNavigation).WithMany(p => p.Salida)
                .HasForeignKey(d => d.Ruc)
                .HasConstraintName("FK_Salida_Ruc");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Salida> entity);
    }
}
