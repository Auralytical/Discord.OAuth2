using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;

namespace Discord.OAuth2
{
    /// <summary> Extension methods to add Discord authentication capabilities to an HTTP application pipeline. </summary>
    public static class DiscordAppBuilderExtensions
    {
        /// <summary> Adds the <see cref="DiscordMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>, which enables Discord authentication capabilities. </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        public static IApplicationBuilder UseDiscordAuthentication(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<DiscordMiddleware>();
        }

        /// <summary> Adds the <see cref="DiscordMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>, which enables Discord authentication capabilities. </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <param name="options">A <see cref="DiscordOptions"/> that specifies options for the middleware.</param>
        public static IApplicationBuilder UseDiscordAuthentication(this IApplicationBuilder app, DiscordOptions options)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            if (options == null) throw new ArgumentNullException(nameof(options)); 

            return app.UseMiddleware<DiscordMiddleware>(Options.Create(options));
        }
    }
}
