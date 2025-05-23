﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class DetalleEntradaConfiguration : IEntityTypeConfiguration<DetalleEntrada>
    {
        public void Configure(EntityTypeBuilder<DetalleEntrada> entity)
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cantidad)
                .HasComment("Comprada o vendida")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CostoUnitarioImportacion)
                .HasComment("Costo del producto una vez en bodega del cliente aplicando el factor de distribucion")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CostoVenta)
                .HasComment("Costo al que salio o entro el producto despues del metodo de costeo")
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Descuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FactorDistribucion)
                .HasComment("Es el porcentaje de incremento por la importacion del producto, incluye todos los gastos hasta la entrega en bodegas")
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("date");
            entity.Property(e => e.FechaFabricacion).HasColumnType("date");
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdEntrada)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Codigo de la transaccion de entrada va relacionado tipo transaccion");
            entity.Property(e => e.IdTipoUnidad).HasComment("Tipo de unidad de medida");
            entity.Property(e => e.IpIngreso)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.IpModificacion)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Lote)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Periodo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[CostoVenta]-isnull([Descuento],(0)))", false)
                .HasColumnType("decimal(38, 6)");
            entity.Property(e => e.TipoTransaccion)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasComment("COMP para compras, VENT para ventas, DEVCOM devolucion en compras, DEVVENT devolucion en ventas, BAJA por salidas por baja de inventario, GARANT salidas por Garantia, CAMBOD movimiento de local o bodega");
            entity.Property(e => e.UsuarioIngreso)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioModificacion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ValorAdicional)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ValorFob)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("ValorFOB");

            entity.HasOne(d => d.IdEntradaNavigation).WithMany(p => p.DetalleEntrada)
                .HasForeignKey(d => d.IdEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleEntrada_Entrada");

            entity.HasOne(d => d.IdLocalNavigation).WithMany(p => p.DetalleEntrada)
                .HasForeignKey(d => d.IdLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleEntrada_Local");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleEntrada)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleEntrada_Producto");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.DetalleEntrada)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleEntrada_Entidad");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DetalleEntrada> entity);
    }
}
