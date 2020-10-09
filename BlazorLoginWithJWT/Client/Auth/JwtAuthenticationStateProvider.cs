using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Security.Claims;
using System.Net.Http;
using BlazorLoginWithJWT.Client.Utility;
using BlazorLoginWithJWT.Shared;

namespace BlazorLoginWithJWT.Client.Auth
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient httpClient;

        private AuthenticationState anonymous;

        public JwtAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            this.localStorageService = localStorageService;
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //確認localstorage裡面是否有token
            string tokenInLocalStorage = await localStorageService.GetItemAsStringAsync("authToken");
            if (string.IsNullOrEmpty(tokenInLocalStorage))
            {
                //沒有的話，回傳匿名使用者
                return anonymous;
            }

            //將token取出轉為claim
            var claims = JwtParser.ParseClaimsFromJwt(tokenInLocalStorage);

            //在每次request的header中帶入bearer token
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", tokenInLocalStorage);

            //回傳帶有user claim的AuthenticationState物件
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        public void NotifyUserAuthentication(string token)
        {
            var claims = JwtParser.ParseClaimsFromJwt(token);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogOut()
        {
            var authState = Task.FromResult(anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}