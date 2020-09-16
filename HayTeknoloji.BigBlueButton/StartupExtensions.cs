using System;
using System.Net.Http;
using HayTeknoloji.BigBlueButton.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HayTeknoloji.BigBlueButton
{
    public static class StartupExtensions
    {
        public static void AddBigBlueButton(this IServiceCollection services, Settings defaultSettings, IHttpClientFactory factory)
        {
            services.AddSingleton<IBigBlueButton, BigBlueButton>(provider => new BigBlueButton(factory, defaultSettings));
        }

        public static void AddBigBlueButtonDefault(this IServiceCollection services, Settings defaultSettings)
        {
            services.AddSingleton<IBigBlueButton, BigBlueButton>(provider =>
            {
                var factory = provider.GetRequiredService<IHttpClientFactory>();
                return new BigBlueButton(factory, defaultSettings);
            });
        }
    }
}
