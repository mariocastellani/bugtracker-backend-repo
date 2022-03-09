using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using System.Security.Claims;

namespace IdentityService.Data
{
    internal static class SeedData
    {
        #region Initial Static Data

        private static IEnumerable<IdentityResource> InitialIdentityResources =>
            new[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new[] { "role" }
                }
            };

        private static IEnumerable<ApiScope> InitialApiScopes =>
            new[]
            {
                // BugTracker API specific scopes
                new ApiScope("bugtracker.read", "BugTracker guest."),
                new ApiScope("bugtracker.write", "BugTracker operator."),

                // Identity API specific scopes
                new ApiScope("identity.auth", "Allows requesting tokens."),

                // Shared scopes
                new ApiScope("manage", "Provides administrative access.")
            };

        private static IEnumerable<ApiResource> InitialApiResources =>
            new[]
            {
                new ApiResource("bugtracker-api")
                {
                    Scopes = { "bugtracker.read", "bugtracker.write", "manage" },
                    UserClaims = new IdentityResources.Profile().UserClaims.Concat(new[] { "role" }).ToArray()
                },

                new ApiResource("identity-api")
                {
                    Scopes = { "identity.auth", "manage" }
                }
            };

        private static IEnumerable<Client> InitialClients =>
            new[]
            {
                new Client
                {
                    ClientId =  "m2m.client",
                    ClientName = "Machine to Machine Client",
                    ClientSecrets = { new Secret("SuperSecretClientId".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "bugtracker.read", "identity.auth" }
                },

                new Client
                {
                    ClientId =  "postman.client",
                    ClientName = "Postman Client",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true, // Allow RefreshTokenUsage
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "bugtracker.write",
                        "manage"
                    }
                },

                new Client
                {
                    ClientId =  "interactive.client",
                    ClientName = "Interactive Client",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true, // Allow RefreshTokenUsage
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "bugtracker.write",
                        "manage"
                    },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false,
                    RedirectUris = { "http://localhost:3000/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:3000/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:3000/signout-callback-oidc" }
                }
            };

        #endregion

        #region Private Methods

        private static void EnsureRoles(IServiceScope scope)
        {
            var roles = new[]
            {
                "Administrator",
                "Project Manager",
                "Developer",
                "Tester",
                "Guest"
            };

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            foreach (var roleName in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role != null)
                    continue;

                role = new IdentityRole<int>(roleName);
                
                var result = roleManager.CreateAsync(role).Result;
                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);
            }
        }

        private static void EnsureUsers(IServiceScope scope)
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser<int>>>();
            
            var user = userManager.FindByNameAsync("Admin").Result;
            if (user != null)
                return;

            user = new IdentityUser<int>()
            {
                UserName = "Admin",
                Email = "admin@email.com",
                EmailConfirmed = true,
            };

            var result = userManager.CreateAsync(user, "Admin123$").Result;
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            result = userManager.AddClaimsAsync(user, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, "Mario Castellani"),
                new Claim(JwtClaimTypes.GivenName, "Mario"),
                new Claim(JwtClaimTypes.FamilyName, "Castellani"),
                new Claim("location", "Córdoba")
            }).Result;

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            result = userManager.AddToRolesAsync(user, new string[] { "Administrator" }).Result;

            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);
        }

        private static void EnsureConfigurationData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in InitialClients)
                    context.Clients.Add(client.ToEntity());

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in InitialIdentityResources)
                    context.IdentityResources.Add(resource.ToEntity());

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                foreach (var scope in InitialApiScopes)
                    context.ApiScopes.Add(scope.ToEntity());

                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resourse in InitialApiResources)
                    context.ApiResources.Add(resourse.ToEntity());

                context.SaveChanges();
            }
        }

        #endregion

        public static void EnsureSeedData(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context1 = scope.ServiceProvider.GetRequiredService<IdentityDataContext>();
                context1.Database.Migrate();

                EnsureRoles(scope);
                EnsureUsers(scope);

                var context2 = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context2.Database.Migrate();

                EnsureConfigurationData(context2);

                var context3 = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
                context3.Database.Migrate();
            }
        }
    }
}