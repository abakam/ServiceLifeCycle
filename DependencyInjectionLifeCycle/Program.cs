using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DependencyInjectionLifeCycle
{
    class Program
    {
        static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            // Set all total values to 1
            Util.SetEnvironmentVariables();
            // First Request -- Creates the first Scope
            FirstRequest(host.Services);
            // Second Request -- Creates the second Scope
            SecondRequest(host.Services);

        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<TranscientService>()
                            .AddScoped<ScopedService>()
                            .AddSingleton<SingletonService>());

        static void FirstRequest(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;

            //First TranscientService Call
            TranscientService transcient = provider.GetRequiredService<TranscientService>();
            Console.WriteLine($"First Request -- First Transcient Call: {transcient.Total}");  // Output = 1
            // Increment Transcient Total from 1 to 2
            Util.IncrementTranscientValue();
            // Second TranscientService  Call
            transcient = provider.GetRequiredService<TranscientService>();
            Console.WriteLine($"First Request -- Second Transcient call: {transcient.Total}"); // Output = 2 because a new instance of TranscientService is created

            // First ScopedService Call
            ScopedService scoped = provider.GetRequiredService<ScopedService>();
            Console.WriteLine($"First Request -- First Scope Call: {scoped.Total}");  // Output = 1
            // Increment Scope Total from 1 to 2
            Util.IncrementScopedValue();
            // Second ScopedService Call
            scoped = provider.GetRequiredService<ScopedService>();
            Console.WriteLine($"First Request -- Second Scope call: {scoped.Total}"); // Output = 1 because NO new instance of ScopedService is created

            // First SingletonService Call
            SingletonService singleton = provider.GetRequiredService<SingletonService>();
            Console.WriteLine($"First Request -- First Singleton Call: {singleton.Total}");  // Output = 1
            // Increment Singleton Total from 1 to 2
            Util.IncrementSingletonValue();
            // Second SingletonService Call
            singleton = provider.GetRequiredService<SingletonService>();
            Console.WriteLine($"First Request -- Second Singleton call: {singleton.Total}"); // Output = 1 because NO new instance of SingletonService is created

        }

        static void SecondRequest(IServiceProvider services)
        {
            using IServiceScope serviceScope = services.CreateScope();
            IServiceProvider provider = serviceScope.ServiceProvider;
            // First TranscientService  Call
            TranscientService transcient = provider.GetRequiredService<TranscientService>();
            Console.WriteLine($"Second Request -- First Transcient Call: {transcient.Total}");  // Output = 2 because a new instance of TranscientService is created
            // Increment Transcient Total from 2 to 3
            Util.IncrementTranscientValue();
            // Second TranscientService  Call
            transcient = provider.GetRequiredService<TranscientService>();
            Console.WriteLine($"Second Request -- Second Transcient call: {transcient.Total}"); // Output = 3 because another instance of TranscientService is created

            // First ScopedService Call
            ScopedService scoped = provider.GetRequiredService<ScopedService>();
            Console.WriteLine($"Second Request -- First Scope Call: {scoped.Total}");  // Output = 2 because now we have ScopedService-2
            // Increment Scope Total from 2 to 3
            Util.IncrementScopedValue();
            // Second ScopedService Call
            scoped = provider.GetRequiredService<ScopedService>();
            Console.WriteLine($"Second Request -- Second Scope call: {scoped.Total}"); // Output = 2 because NO new instance of ScopedService is created, still use ScopedService-2

            // First SingletonService Call
            SingletonService singleton = provider.GetRequiredService<SingletonService>();
            Console.WriteLine($"Second Request -- First Singleton Call: {singleton.Total}");  // Output = 1 -- still using SingletonService from First Request -- First Call
            // Increment Singleton Total from 2 to 3
            Util.IncrementSingletonValue();
            // Second SingletonService Call
            singleton = provider.GetRequiredService<SingletonService>();
            Console.WriteLine($"Second Request -- Second Singleton call: {singleton.Total}"); // Output = 1 -- still using SingletonService from First Request -- First Call

        }
    }
}
