# Discord.OAuth2 v1.0.0

ASP.Net Core middleware that enables an application to support Discord's OAuth 2.0 authentication workflow.

Based on the [ASP.Net Core Facebook OAuth](https://github.com/aspnet/Security/tree/dev/src/Microsoft.AspNetCore.Authentication.Facebook)

### Usage
```cs
app.UseDiscordAuthentication(new DiscordOptions
{
    AppId = Configuration["Discord:AppId"],
    AppSecret = Configuration["Discord:AppSecret"],
    Scope = { "identify", "guilds" }
});
```
