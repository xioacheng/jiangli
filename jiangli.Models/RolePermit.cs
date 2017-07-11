using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jiangli.Models
{
    public class RolePermit
    {
        public int id { get; set; }
        public int RoleID { get; set; }
        public int PermitID { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public int ModifyBy { get; set; }
        public DateTime ModifyOn { get; set; }
    }
}
