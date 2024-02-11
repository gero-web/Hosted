using Core.Interfases;
using Core.Servises;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjectionCore
    {
        public static void AddCoreServise(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICastsImages, CastsImages>();
        }
    }
}
