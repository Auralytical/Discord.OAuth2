namespace Discord.OAuth2
{
    /// <summary> A collection of default values used by DiscordMiddleware. </summary>
    public static class DiscordDefaults
    {
        /// <summary> The default authentication scheme. </summary>
        public const string AuthenticationScheme = "Discord";

        /// <summary> The default oauth authentication endpoint for Discord. </summary>
        public static readonly string AuthorizationEndpoint = "https://discordapp.com/api/oauth2/authorize";
        /// <summary> The default oauth token endpoint for Discord. </summary>
        public static readonly string TokenEndpoint = "https://discordapp.com/api/oauth2/token";
        /// <summary> The default user information endpoint used to generate claims. </summary>
        public static readonly string UserInformationEndpoint = "https://discordapp.com/api/users/@me";
    }
}
