using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hexagon.Data.Entity.EntityConfiguration
{
    public class Organization_EmployeeConfiguration : IEntityTypeConfiguration<Organization_Employee>
    {
        public void Configure(EntityTypeBuilder<Organization_Employee> builder)
        {
            builder.HasKey(t => t.Employee_Id);
            builder.Property(t => t.Employee_Name).IsRequired().HasMaxLength(50);
        }

    }
}
