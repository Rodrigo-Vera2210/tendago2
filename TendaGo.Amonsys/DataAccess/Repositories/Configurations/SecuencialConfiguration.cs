﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class SecuencialConfiguration : IEntityTypeConfiguration<Secuencial>
    {
        public void Configure(EntityTypeBuilder<Secuencial> entity)
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Autorizacion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Establecimiento)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.FechaVigencia).HasColumnType("date");
            entity.Property(e => e.IdTipoDocumento)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.PuntoVenta)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Ruc)
                .IsRequired()
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RUC");
            entity.Property(e => e.Secuencial1).HasColumnName("Secuencial");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Secuencial> entity);
    }
}
