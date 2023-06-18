﻿using Microsoft.EntityFrameworkCore;
using SnipeLabelEditor.Models;

namespace SnipeLabelEditor
{
    public class LabelsDBContext : DbContext
    {
        public LabelsDBContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Label> Labels { get; set; }
    }
}
