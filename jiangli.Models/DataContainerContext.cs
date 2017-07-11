using jiangli.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DataContainerContext:DbContext
    {
        public DataContainerContext() : base("Jiangli") { }
        public DbSet<Case> Case { get; set; }
        public DbSet<Appeal> Appeal { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<ExaminerConte> ExaminerContent { get; set; }
        public DbSet<Involve> Involve { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<StationFeedBack> StationFeedBack { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Permit> Permit { get; set; }
        public DbSet<RolePermit> RolePermit { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
