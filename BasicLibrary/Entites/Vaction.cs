using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Entites
{
    public class Vaction : OtherBaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int NumberOfDays { get; set; }

        [Required]
        public DateTime EndDate => StartDate.AddDays(NumberOfDays);
        public VacationType? VacationType { get; set; }
        [Required]
        public int VacationTypeId { get; set; }

    }

}
