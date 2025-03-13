﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class DetalleSalidaConfiguration : IEntityTypeConfiguration<DetalleSalida>
    {
        public void Configure(EntityTypeBuilder<DetalleSalida> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_KARDEX");

            entity.HasIndex(e => new { e.IdEmpresa, e.Id }, "IX_DetalleSalida_001");

            entity.HasIndex(e => new { e.IdSalida, e.IdEstado }, "IX_DetalleSalida_EstadoSalida");

            entity.HasIndex(e => e.IdEstado, "IX_DetalleSalida_IdEstado");

            entity.HasIndex(e => e.IdTipoUnidad, "IX_DetalleSalida_Ventas");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cantidad)
                .HasComment("Comprada o vendida")
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CostoUnitarioImportacion)
                .HasComment("Costo del producto una vez en bodega del cliente aplicando el factor de distribucion")
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.CostoVenta)
                .HasComment("Costo al que salio o entro el producto despues del metodo de costeo")
                .HasColumnType("decimal(18, 4)");
            entity.Property(e => e.Descuento).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("date");
            entity.Property(e => e.FechaFabricacion).HasColumnType("date");
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.IdSalida)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Codigo de la transaccion de Salida va relacionado con el tipo de transaccion ");
            entity.Property(e => e.IdTipoUnidad).HasComment("Tipo de unidad de medida");
            entity.Property(e => e.IncluyeIva)
                .HasDefaultValue(false)
                .HasColumnName("IncluyeIVA");
            entity.Property(e => e.IpIngreso)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.IpModificacion)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Iva)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("IVA");
            entity.Property(e => e.Lote)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Periodo)
                .IsRequired()
                .HasMaxLength(7)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.PrecioFinal)
                .HasComputedColumnSql("(CONVERT([decimal](18,6),case when [TIENEIVA]=(1) AND [INCLUYEIVA]=(1) then [PRECIO]/((1)+[IVA]/(100)) else [PRECIO] end))", false)
                .HasColumnType("decimal(18, 6)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[Precio]-isnull([Descuento],(0)))", false)
                .HasColumnType("decimal(38, 6)");
            entity.Property(e => e.TieneIva)
                .HasDefaultValue(false)
                .HasColumnName("TieneIVA");
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
            entity.Property(e => e.ValorIva)
                .HasComputedColumnSql("(case when [TIENEIVA]=(1) then case when [INCLUYEIVA]=(1) then [CANTIDAD]*[PRECIO]-([CANTIDAD]*[PRECIO])/((1)+[IVA]/(100)) else ([CANTIDAD]*[PRECIO])*([IVA]/(100)) end else (0) end)", false)
                .HasColumnType("decimal(38, 6)")
                .HasColumnName("ValorIVA");
            entity.Property(e => e.ValorTotal)
                .HasComputedColumnSql("(case when [TIENEIVA]=(1) then case when [INCLUYEIVA]=(1) then [CANTIDAD]*[PRECIO] else [CANTIDAD]*[PRECIO]+([CANTIDAD]*[PRECIO])*([IVA]/(100)) end else (0) end)", false)
                .HasColumnType("decimal(38, 6)");

            entity.HasOne(d => d.IdLocalNavigation).WithMany(p => p.DetalleSalida)
                .HasForeignKey(d => d.IdLocal)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardex_Local");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.DetalleSalida)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleSalida_Entidad");

            entity.HasOne(d => d.IdSalidaNavigation).WithMany(p => p.DetalleSalida)
                .HasForeignKey(d => d.IdSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kardex_Salida");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<DetalleSalida> entity);
    }
}
