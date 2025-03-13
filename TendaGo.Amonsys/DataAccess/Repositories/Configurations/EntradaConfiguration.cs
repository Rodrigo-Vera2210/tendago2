﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class EntradaConfiguration : IEntityTypeConfiguration<Entrada>
    {
        public void Configure(EntityTypeBuilder<Entrada> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_Entradas");

            entity.ToTable(tb => tb.HasTrigger("TR_Entrada_Estados"));

            entity.HasIndex(e => e.TransaccionPadre, "IX_Entrada_01");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EstadoActual)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaHoraEntrega).HasColumnType("datetime");
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IpIngreso)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.IpModificacion)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.NumeroFacturaPedido)
                .HasMaxLength(50)
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
            entity.Property(e => e.Subtotal12).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Tasa).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.TipoTransaccion)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.TipoTransaccionPadre)
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
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

            entity.HasOne(d => d.IdLocalNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Entrada_LocalBodega");

            entity.HasOne(d => d.IdMonedaOrigenNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdMonedaOrigen)
                .HasConstraintName("FK_Entrada_Moneda");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK_Entrada_Entidad");

            entity.HasOne(d => d.RucNavigation).WithMany(p => p.Entrada)
                .HasForeignKey(d => d.Ruc)
                .HasConstraintName("FK_Entrada_Ruc");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Entrada> entity);
    }
}
