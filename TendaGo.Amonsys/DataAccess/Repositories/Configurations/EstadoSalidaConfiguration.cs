﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class EstadoSalidaConfiguration : IEntityTypeConfiguration<EstadoSalida>
    {
        public void Configure(EntityTypeBuilder<EstadoSalida> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_EstadoSalida_1");

            entity.HasIndex(e => e.Salida, "IX_EstadoSalida_Salida");

            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Salida)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.SalidaNavigation).WithMany(p => p.EstadoSalida)
                .HasForeignKey(d => d.Salida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EstadoSalida_Salida");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<EstadoSalida> entity);
    }
}
