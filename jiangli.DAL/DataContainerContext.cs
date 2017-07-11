using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jiangli.Models;

namespace jiangli.DAL
{
    public class DataContainerContext:DbContext
    {
        public DataContainerContext() : base("jiangli") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
