using Infrastructure.DependencyInjection.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.DependencyInjection.Extensions;

public static class JWTExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            JWTOptions jwtOption = new JWTOptions();
            configuration.GetSection(nameof(JWTOptions)).Bind(jwtOption);
            /*
            public async Task<IActionResult> SomeAction()
            {
                // using Microsoft.AspNetCore.Authentication;
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                // ...
            }
            */

            x.SaveToken = true;

            var key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // on production set true
                ValidateAudience = false, // on production set true
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,


                ValidIssuer = jwtOption.Issuer,
                ValidAudience = jwtOption.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),   // Khi len moi truong Product thi nen dat Key vao
                ClockSkew = TimeSpan.Zero
            };

            x.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();
    }
}
