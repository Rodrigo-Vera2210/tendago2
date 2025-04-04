﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class CostoPromedioConfiguration : IEntityTypeConfiguration<CostoPromedio>
    {
        public void Configure(EntityTypeBuilder<CostoPromedio> entity)
        {
            entity.HasKey(e => e.IdProducto).HasName("PK_CostoPromedioPonderado");

            entity.ToTable(tb => tb.HasTrigger("TG_CostoPromdio_Stock"));

            entity.Property(e => e.IdProducto).ValueGeneratedNever();
            entity.Property(e => e.CostoPromedioPonderado).HasColumnType("decimal(18, 4)");
            entity.Property(e => e.FechaUltimaTransaccion).HasColumnType("datetime");
            entity.Property(e => e.IdDetalle)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SaldoInventario)
                .HasComputedColumnSql("([StockTotal]*[CostoPromedioPonderado])", false)
                .HasColumnType("decimal(37, 6)");
            entity.Property(e => e.StockTotal).HasColumnType("decimal(18, 2)");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<CostoPromedio> entity);
    }
}
