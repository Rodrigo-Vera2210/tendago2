﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using ER.DA.Models;
using ER.DA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace ER.DA.Repositories.Configurations
{
    public partial class ChoferConfiguration : IEntityTypeConfiguration<Chofer>
    {
        public void Configure(EntityTypeBuilder<Chofer> entity)
        {
            entity.Property(e => e.InicioSesion)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEntidadNavigation).WithMany(p => p.Chofer)
                .HasForeignKey(d => d.IdEntidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chofer_Entidad");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Chofer> entity);
    }
}
