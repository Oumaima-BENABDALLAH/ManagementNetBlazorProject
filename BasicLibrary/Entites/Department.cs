using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Entites
{
    public  class Department :BaseEntity 
    {
        //Many t one relationship with General Department
        public GeneralDepartment? GeneralDepartment { get; set; }
        public int GeneralDepartmentId { get; set; }

        // One to many with Branch
        public List<Branch>? Branches { get; set; }
    }
}
