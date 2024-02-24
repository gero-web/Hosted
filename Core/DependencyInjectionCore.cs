using Core.Interfases;
using Core.Pools;
using Core.Servises;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class DependencyInjectionCore
    {
        public static void AddCoreServise(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICastsImages, CastsImages>();
            serviceCollection.AddTransient<IScreenShotService, ScreenShotService>();
            serviceCollection.AddTransient<IPool, Pool>();
        }
    }
}
