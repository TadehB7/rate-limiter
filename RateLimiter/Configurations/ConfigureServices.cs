﻿using Microsoft.Extensions.DependencyInjection;
using RateLimiter.Enums;
using RateLimiter.Interfaces;
using RateLimiter.Services;
using System;

namespace RateLimiter.Configurations
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<RateLimitRuleAService>();
            services.AddScoped<RateLimitRuleBService>();

            services.AddTransient<Func<RateLimitRules, IRateLimitRule>>(serviceProvider => key =>
            {
                return key switch
                {
                    RateLimitRules.RuleA => serviceProvider.GetRequiredService<RateLimitRuleAService>(),
                    RateLimitRules.RuleB => serviceProvider.GetRequiredService<RateLimitRuleBService>(),
                    _ => throw new NotSupportedException($"Service with key '{key}' not found.")
                };
            });

            return services;

        }
    }
}
