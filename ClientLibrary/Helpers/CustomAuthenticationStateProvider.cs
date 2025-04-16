using BasicLibrary.Dto;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helpers
{
    public class CustomAuthenticationStateProvider(LocalStorageService localStorageService) : AuthenticationStateProvider
    {
        private readonly  ClaimsPrincipal claimsPrincipal = new (new ClaimsPrincipal());
        public  override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var stringToken = await  localStorageService.GetToken();
            if (string.IsNullOrEmpty(stringToken)) return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            var deserializeToken = Serialization.DeserializeJsonString<UserSession>(stringToken);
            if (deserializeToken == null) return await Task.FromResult(new AuthenticationState(claimsPrincipal));

            var getUserClaims = DecryptToken(deserializeToken.Token!);
            if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(claimsPrincipal));

            var claimsP = SetClaimPrincipal(getUserClaims);
            return await Task.FromResult(new AuthenticationState(claimsP));


        }
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            var claimsP = new ClaimsPrincipal();
            if(userSession.Token != null || userSession.RefreshToken != null)
            {
                var serializationSession = Serialization.SerializeObj(userSession);
                await localStorageService.SetToken(serializationSession);
                var getUserClaims = DecryptToken(userSession.Token!);
                claimsP = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                await localStorageService.RemoveToken();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsP)));
        }
        public static CustomUserClaims DecryptToken(string jwtToken)
        {
            if ( string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var userId = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var name = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var email = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            var role = token.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            return new CustomUserClaims(userId!.Value!, name!.Value!, email!.Value!, role!.Value!);

        }


        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(

                new List<Claim>
                {

                    new (ClaimTypes.NameIdentifier, claims.Id!),
                    new (ClaimTypes.Name, claims.Name!),
                    new (ClaimTypes.Email, claims.Email!),
                    new (ClaimTypes.Role, claims.Role!),

                },"JwtAuth"
                ));
        }
    }
}
