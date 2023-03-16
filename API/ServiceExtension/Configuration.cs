using AspNetCoreRateLimit;

namespace API.ServiceExtension
{
    public static class Configuration
    {
       
        public static void ConfigureRateLimiting(this IServiceCollection services)
        {

            var rateLimitRule = new List<RateLimitRule>()
            {
                new RateLimitRule
                {
                    Endpoint="*",
                    Limit = 100,
                    Period = "1h"
                }
            };
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = rateLimitRule;
            });
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }

    }
}
