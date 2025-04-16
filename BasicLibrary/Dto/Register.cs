using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Dto
{
    public class Register : AccountBase
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string? Email { get; set; }

        public string? FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }




    }
}
