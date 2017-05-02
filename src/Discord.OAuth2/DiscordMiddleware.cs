using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text.Encodings.Web;

namespace Discord.OAuth2
{
    /// <summary> An ASP.NET Core middleware for authenticating users using Discord. </summary>
    public class DiscordMiddleware : OAuthMiddleware<DiscordOptions>
    {
        /// <summary> Initializes a new <see cref="DiscordMiddleware"/>. </summary>
        /// <param name="next">The next middleware in the HTTP pipeline to invoke.</param>
        /// <param name="dataProtectionProvider"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="encoder"></param>
        /// <param name="sharedOptions"></param>
        /// <param name="options">Configuration options for the middleware.</param>
        public DiscordMiddleware(
            RequestDelegate next, IDataProtectionProvider dataProtectionProvider, ILoggerFactory loggerFactory,  UrlEncoder encoder,
            IOptions<SharedAuthenticationOptions> sharedOptions, IOptions<DiscordOptions> options)
            : base(next, dataProtectionProvider, loggerFactory, encoder, sharedOptions, options)
        {
            if (string.IsNullOrEmpty(Options.AppId))
                throw new ArgumentException($"The {nameof(Options.AppId)} option must be provided."); 
            if (string.IsNullOrEmpty(Options.AppSecret))
                throw new ArgumentException($"The {nameof(Options.AppSecret)} option must be provided.");
        }

        /// <summary> Provides the <see cref="AuthenticationHandler{T}"/> object for processing authentication-related requests. </summary>
        /// <returns>An <see cref="AuthenticationHandler{T}"/> configured with the <see cref="DiscordOptions"/> supplied to the constructor.</returns>
        protected override AuthenticationHandler<DiscordOptions> CreateHandler()
        {
            return new DiscordHandler(Backchannel);
        }
    }
}
