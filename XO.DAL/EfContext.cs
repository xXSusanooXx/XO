using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XO.Model.DbModels;

namespace XO.DAL
{
    public class EfContext : DbContext
    {
        public EfContext() : base("XOConnection") { }

        public DbSet<User> Users { get; set; }
    }
}
