// See https://aka.ms/new-console-template for more information

using Core;
using Host.Interface;
using Host.Services;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();
services.AddCoreServise();

services.AddTransient<ISenderService, SenderService>(); 

var provider = services.BuildServiceProvider();
var scopeFactory = provider.GetService<IServiceScopeFactory>();

using (var scope = scopeFactory.CreateScope())
{
    var serv = scope.ServiceProvider.GetService<ISenderService>();
    var conn = new ConnectionService(serv);
    await conn.ServerWorker();
}


 





