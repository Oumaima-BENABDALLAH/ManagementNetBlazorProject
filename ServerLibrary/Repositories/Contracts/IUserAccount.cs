using BasicLibrary.Dto;
using BasicLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface  IUserAccount
    {

        Task<GeneralResponse> CreateAsync(Register user);

        Task<LoginResponse> SignInAsync(Login user);

        Task<LoginResponse> RefreshTokenAsync(RefreshToken refreshToken);

    }
}
