using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Entites
{
    public class City : BaseEntity
    {

        public Country? Country { get; set; }
        public int CountryId { get; set; }

        public List<Town>? Towns { get; set; }

    }

}
