using BasicLibrary.Dto;
using BasicLibrary.Entites;
using BasicLibrary.Responses;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository (IOptions<JwtSection> config , AppDbContext appDbContext): IUserAccount
    {
        public async  Task<GeneralResponse> CreateAsync(Register user)
        {

            if (user == null) return new GeneralResponse(false, "Model Is Empty !!");
            var checkUser = await FindUserByEmail(user.Email!);

      //     if (user != null) return new GeneralResponse(false, "User is already!");


            // save user
            var applicationUser = await addToDatabase(new ApplicationUser()
            {
                FullName = user!.FullName,
                Email =user.Email,
                Password =BCrypt.Net.BCrypt.HashPassword( user.Password)
            } );

            // check, create and assign role

            var checkAdminRole = await appDbContext.SystemUsers.FirstOrDefaultAsync(x => x.Name!.Equals(Constants.Admin));
            if (checkAdminRole is null)
            {
                var createAdminRole = await addToDatabase(new SystemUser()
                {
                    Name = Constants.Admin

                });
                await addToDatabase(new UserRole() { RoleId = createAdminRole.Id, UserId = applicationUser.Id });
                return new GeneralResponse(true, "Account created !");
            }

            var checkUserRole = await appDbContext.SystemUsers.FirstOrDefaultAsync(x => x.Name!.Equals(Constants.User));
            SystemUser reponse = new();
            if (checkUserRole is null)
            {
                reponse = await addToDatabase(new SystemUser() { Name = Constants.User });
                await addToDatabase(new UserRole() { RoleId = reponse.Id, UserId = applicationUser.Id });

            }
            else
            {
                await addToDatabase(new UserRole() { RoleId = checkUserRole.Id, UserId = applicationUser.Id });

            }
            return new GeneralResponse(true, "Account created !");
           
        }
        public async Task<ApplicationUser> FindUserByEmail(string email) =>

             await appDbContext.ApplicationUser.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower()));
        

        public async Task<LoginResponse> SignInAsync(Login user)
        {

            if (user == null) return new LoginResponse(false,"Model is empty!");
            var applicationUser = await FindUserByEmail(user.Email!);
            if (applicationUser == null) return new LoginResponse(false, "User not found");
            //verify password 


            if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
            {
                return new LoginResponse(false, "Email/Password not found");
            }
            var getUserRole = await appDbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == applicationUser.Id);
            if (getUserRole is null) return new LoginResponse(false, "user role not found");

            var getUserName = await appDbContext.SystemUsers.FirstOrDefaultAsync(x => x.Id == getUserRole.RoleId);
            if (getUserRole is null) return new LoginResponse(false, "User role not found");

            string jwtToken = GenerateToken(applicationUser, getUserName!.Name);
            string refreshToken = GenerateRefreshToken();

            var findUser = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.UserId == applicationUser.Id);
            if(findUser is not null)
            {

                findUser!.Token = refreshToken;
                await appDbContext.SaveChangesAsync();

            }
            else
            {
                await addToDatabase(new RefreshTokenInfo() { Token = refreshToken, UserId = applicationUser.Id });
            }


            return new LoginResponse(true, "Login successfully", jwtToken, refreshToken);


        }

        private string GenerateToken(ApplicationUser user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.key!));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
               {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.FullName!),
                new Claim(ClaimTypes.Email,user.Email!),
                new Claim(ClaimTypes.Role,role!),
                };

            var token = new JwtSecurityToken
                (
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials :credentials

                
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        async Task<T> addToDatabase<T>(T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;


        }
        private async Task<UserRole> FindUserRole(int userId) => await appDbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
        private async Task<SystemUser> FindUserName(int roleId) => await appDbContext.SystemUsers.FirstOrDefaultAsync(x => x.Id! == roleId!);

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken refreshToken)
        {
            if (refreshToken is null) return new LoginResponse(false, "Model is empty");

            var findToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.Token!.Equals(refreshToken.Token));
            if (findToken is null) return new LoginResponse(true, "Refresh token is required");

            // get user details 

            var user = await appDbContext.ApplicationUser.FirstOrDefaultAsync(x => x.Id == findToken.Id);
            if (user is null) return new LoginResponse(false, "Refresh token could not be generated because user not found ");

            var userRole = await FindUserRole(user.Id);
            var roleName = await FindUserName(userRole.RoleId);

            string jwtToken = GenerateToken(user, roleName.Name!);
            string token = GenerateRefreshToken();

            var updateRefreshToken = await appDbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (updateRefreshToken is null) return new LoginResponse(false, "Refresh token could not be generated because user has not signined ");


            updateRefreshToken.Token = token;
            await appDbContext.SaveChangesAsync();
            return new LoginResponse(true, "Token refreshed successfully", jwtToken, token);









        }
    }
}
