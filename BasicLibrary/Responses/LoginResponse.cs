using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary.Responses
{
    public record LoginResponse(bool Flaag, string Message = null!, string Token = null!, string RefreshToken = null!);

}
