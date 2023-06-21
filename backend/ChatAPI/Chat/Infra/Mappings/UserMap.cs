﻿using Chat.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Infra.Mappings
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(prop => prop.Id);
            builder.Property(prop => prop.Name).HasMaxLength(40);
            builder.Property(prop => prop.UserName).HasMaxLength(15);
        }
    }
}