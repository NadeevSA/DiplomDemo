using DesignTask.Providers;
using KnowledgeUberization.Providers;
using Microsoft.Extensions.Configuration;
using RabbitCommand.Rabbit.Sender;
using Unity;
using Unity.Injection;

namespace DesignTask
{
    public class UnityConfig
    {
        public static IUnityContainer GetConfiguredContainer(IConfiguration configuration, bool diagnosticMode = false)
        {
            var container = diagnosticMode
                ? new UnityContainer().AddExtension(new Diagnostic())
                : new UnityContainer();

            DoRegistration(container, configuration);
            return container;
        }

        public static void DoRegistration(IUnityContainer container, IConfiguration configuration)
        {
            container.RegisterType<IMqSender, MqSender>();
            container.RegisterType<IDbConnector, PostgresDbConnector>(
                new InjectionConstructor(
                    $"Host={configuration.GetSection("Postgres:DATABASE_HOST").Value}; " + 
                    $"Port={configuration.GetSection("Postgres:DATABASE_PORT").Value}; " + 
                    $"User ID={configuration.GetSection("Postgres:DATABASE_USER").Value}; " +
                    $"Password={configuration.GetSection("Postgres:DATABASE_PASSWORD").Value}; " +
                    $"Timeout = 60; " +
                    $"Command Timeout = 300; "));
        }
    }
}