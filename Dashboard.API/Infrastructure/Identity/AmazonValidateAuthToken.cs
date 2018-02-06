using Dashboard.API.Application.Infrastructure.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;

namespace Dashboard.API.Infrastructure.Identity
{
    public class AmazonValidateAuthToken : IValidateAuthToken
    {
        private const string Key = "wrKRINg8RfKm2tcRVIGuXyi67e0sgwjvqPACsuL5-U-IHe1uj3tlITyIy4nB_6d1Pic7r3Gg5izK7xciEPIhqiEZAoTFyt4J1QV6WyysG7uGMwkQyMQGNiycToF42TkU9wJMvSA90Rw7C1qId2P495I1z36NQEMNp-PPy4s5XDLYI7QW_99AGZ1xOJKUiP9Jj4_m0W8aicY6O4UNspBJhF2okbJNA77KT4E4wL8ULea26_wEwEvkS-GFUinhqeniuph7OlJwf7KZmLLxCzs0GkG3XOkJBvYYgk53tFbNk8Ygje3avG9dDX5PsNxzUpHTlaFz5C0MaIvzF4xPQWVY4Q";

        private const string Expo = "AQAB";

        private const string Issuer = "https://cognito-idp.eu-west-1.amazonaws.com/eu-west-1_Pr1gL8stH";

        public TokenValidationParameters TokenValidationParameters =>
            new TokenValidationParameters
            {
                // Basic settings - signing key to validate with, IssuerSigningKey and issuer.
                IssuerSigningKey = SigningKey(Key, Expo),
                ValidIssuer = Issuer,

                // when receiving a token, check that the signing key
                ValidateIssuerSigningKey = true,

                // When receiving a token, check that we've signed it.
                ValidateIssuer = true,

                // When receiving a token, check that it is still valid.
                ValidateLifetime = true,

                // Do not validate Audience on the "access" token since Cognito does not supply it but it is      on the "id"
                ValidateAudience = false,

                // This defines the maximum allowable clock skew - i.e. provides a tolerance on the token expiry time 
                // when validating the lifetime. As we're creating the tokens locally and validating them on the same 
                // machines which should have synchronised time, this can be set to zero. Where external tokens are
                // used, some leeway here could be useful.
                ClockSkew = TimeSpan.FromMinutes(0)
            };

        private static RsaSecurityKey SigningKey(string key, string expo)
        {
            var rrr = RSA.Create();

            rrr.ImportParameters(
                new RSAParameters
                {
                    Modulus = Base64UrlEncoder.DecodeBytes(key),
                    Exponent = Base64UrlEncoder.DecodeBytes(expo)
                }
            );

            return new RsaSecurityKey(rrr);
        }
    }
}
