﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Entites
{
    public class SanctionType :BaseEntity
    {
        public List<Sanction>? Sanctions { get; set; }
    }
}
