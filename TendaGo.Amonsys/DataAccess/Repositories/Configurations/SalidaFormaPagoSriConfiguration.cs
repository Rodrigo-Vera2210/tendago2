﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class SalidaFormaPagoSriConfiguration : IEntityTypeConfiguration<SalidaFormaPagoSri>
    {
        public void Configure(EntityTypeBuilder<SalidaFormaPagoSri> entity)
        {
            entity.HasKey(e => e.IdSalidaFormaPagoSri);

            entity.HasIndex(e => e.IdSalida, "UC_SalidaFormaPagoSri").IsUnique();

            entity.Property(e => e.IdSalida)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdFormaPagoSriNavigation).WithMany(p => p.SalidaFormaPagoSri)
                .HasForeignKey(d => d.IdFormaPagoSri)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalidaFormaPagoSri_FormaPagoSri");

            entity.HasOne(d => d.IdSalidaNavigation).WithOne(p => p.SalidaFormaPagoSri)
                .HasForeignKey<SalidaFormaPagoSri>(d => d.IdSalida)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SalidaFormaPagoSri_Salida");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<SalidaFormaPagoSri> entity);
    }
}
