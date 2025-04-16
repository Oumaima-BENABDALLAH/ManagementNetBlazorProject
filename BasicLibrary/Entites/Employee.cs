

using System.ComponentModel.DataAnnotations;

namespace BasicLibrary.Entites
{
    public class Employee :BaseEntity
    {


        [Required]
        public string? FullNumber { get; set; } = string.Empty;
        [Required]
        public string? FullName { get; set; } = string.Empty;
        [Required]
        public string? JobName { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        [Required, DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string? Photo { get; set; } = string.Empty;

        public string? Note { get; set; }

         // Relation : Many to one  with Branch
        public Branch? Branch { get; set; }
        public int BranchId { get; set; }


        public Town? Town { get; set; }
        public int TownId { get; set; }






    }
}
