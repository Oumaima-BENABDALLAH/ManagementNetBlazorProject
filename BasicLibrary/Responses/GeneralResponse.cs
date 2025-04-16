using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Responses
{
    public record GeneralResponse(bool Flaag, string Message = null!);
   
}
