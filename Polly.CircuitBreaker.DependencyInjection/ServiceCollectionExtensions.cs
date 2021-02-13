﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.CircuitBreaker.DependencyInjection.Abstractions;

namespace Polly.CircuitBreaker.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCircuitBreaker(this IServiceCollection services,
            IConfiguration configuration = null,
            string configurationSection = "CircuitBreaker",
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            var serviceDescriptor = new ServiceDescriptor(typeof(ICircuitBreakerFactory), typeof(CircuitBreakerFactory), serviceLifetime);

            services.Add(serviceDescriptor);

            services.AddTransient(typeof(ICircuitBreaker<>), typeof(CircuitBreaker<>));

            services.Configure<CircuitBreakerOption>(option => configuration?.GetSection(configurationSection)?.Bind(option));

            return services;
        }
    }
}
