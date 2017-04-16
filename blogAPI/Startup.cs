using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;

using blogAPI.Models;
using blogAPI.Data;
using blogAPI.Options;

namespace blogAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("authkey.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"authkey.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        private IConfigurationRoot _config { get; }

        private string SecretKey;
        private SymmetricSecurityKey _signingKey;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);

            // getting secret key
            SecretKey = _config.GetSection("SecretKey").Value;
            _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            // configuring MongoDB options
            services.Configure<Settings>(options =>
            {
                options.ConnectionString = _config.GetSection("MongoConnection:ConnectionString").Value;
                options.Database = _config.GetSection("MongoConnection:Database").Value;
            });

            services.AddOptions();

            services.AddLogging();

            // adding repositories
            services.AddSingleton<UsersRepository>();
            services.AddSingleton<CredentialsRepository>();
            services.AddSingleton<StoriesRepository>();
            services.AddSingleton<CommentsRepository>();
            services.AddSingleton<OpinionsRepository>();
            
            // Add framework services.
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Refresh",
                                policy => policy.RequireClaim("TokenType", "Refresh"));

                options.AddPolicy("User",
                                policy => policy.RequireClaim("AuthorizedUser", "User"));

                options.AddPolicy("Admin",
                                policy => policy.RequireClaim("AuthorizedUser", "Admin"));
            });

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,

                ValidateAudience = false,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseMvc();
        }
    }
}
