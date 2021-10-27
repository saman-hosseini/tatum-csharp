using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatumPlatform.MyConsole.Model
{
    public class MyContext : DbContext
    {
        public MyContext()
        {

        }
        public virtual DbSet<MyTest> MyTest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=MyConsole;User Id=sa;Password=aA123456");
            }
        }
    }
}
