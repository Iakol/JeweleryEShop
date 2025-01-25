using System.Globalization;

namespace Jewelery.Infrastructure.Localization
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            var cultureSTR = context.Request.Cookies["Culture"];
            if (!string.IsNullOrWhiteSpace(cultureSTR))
            {
                var culture = new CultureInfo(cultureSTR);

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;

            }
            else
            {
                context.Response.Cookies.Append("Culture", "uk", new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMonths(1),
                    HttpOnly = false,
                    Secure = false,
                    SameSite = SameSiteMode.Strict,
                });
                var culture = new CultureInfo("uk");

                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;

            }
            await _next(context);
        }
    }
}
