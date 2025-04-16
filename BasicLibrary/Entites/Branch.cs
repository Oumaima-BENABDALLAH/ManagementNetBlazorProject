using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Entites
{
    public class Branch: BaseEntity
    {
        // Many to One with department

        public Department? Department { get; set; }
        public int DepartmentId { get; set; }

        // One to many with Employee
        public List<Employee>? Employees { get; set; }
    }
}
