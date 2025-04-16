using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Entites
{
    public class Country : BaseEntity
    {
        public List<City>? Cities { get; set; }
    }

}
