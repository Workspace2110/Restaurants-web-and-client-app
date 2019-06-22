using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cloud_homework4.Models
{
    public class Cloud_homework4Context : DbContext
    {
        public Cloud_homework4Context (DbContextOptions<Cloud_homework4Context> options)
            : base(options)
        {
        }

        public DbSet<Cloud_homework4.Models.Restaurant> Restaurant { get; set; }
    }
}
