﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class FanIdConfiguration : IEntityTypeConfiguration<FanId>
    {
        public void Configure(EntityTypeBuilder<FanId> entity)
        {
            entity.Property(e => e.GuestId)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GuestId2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdEstado).HasDefaultValue((short)1);
            entity.Property(e => e.NumeroOrden)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");

            entity.HasOne(d => d.IdEntidadNavigation).WithMany(p => p.FanId)
                .HasForeignKey(d => d.IdEntidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FanId_Entidad");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<FanId> entity);
    }
}
