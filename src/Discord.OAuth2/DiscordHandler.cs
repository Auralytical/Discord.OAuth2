using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http.Authentication;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Discord.OAuth2
{
    internal class DiscordHandler : OAuthHandler<DiscordOptions>
    {
        public DiscordHandler(HttpClient httpClient)
            : base(httpClient)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);
            response.EnsureSuccessStatusCode();

            var user = JObject.Parse(await response.Content.ReadAsStringAsync());

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), properties, Options.AuthenticationScheme);
            var context = new OAuthCreatingTicketContext(ticket, Context, Options, Backchannel, tokens, user);

            AddClaim(identity, user, "id", ClaimTypes.NameIdentifier, ClaimValueTypes.UInteger64);
            AddClaim(identity, user, "username", ClaimTypes.Name, ClaimValueTypes.String);
            AddClaim(identity, user, "discriminator", "urn:discord:discriminator", ClaimValueTypes.UInteger32);
            AddClaim(identity, user, "avatar", "urn:discord:avatar", ClaimValueTypes.String);
            AddClaim(identity, user, "verified", "urn:discord:verified", ClaimValueTypes.Boolean);
            AddClaim(identity, user, "email", ClaimTypes.Email, ClaimValueTypes.Email);

            await Options.Events.CreatingTicket(context);
            return context.Ticket;
        }

        private void AddClaim(ClaimsIdentity identity, JToken user, string fieldName, string claimId, string valueType)
        {
            string value = user.Value<string>(fieldName);
            if (!string.IsNullOrEmpty(value))
                identity.AddClaim(new Claim(claimId, value, valueType, Options.ClaimsIssuer));
        }
    }
}
