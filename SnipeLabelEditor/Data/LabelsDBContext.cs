using Microsoft.EntityFrameworkCore;
using SnipeLabelEditor.Models;

namespace SnipeLabelEditor.Data
{
    public class LabelsDBContext : DbContext
    {
        public LabelsDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Label> Labels { get; set; }
    }
}
