// See https://aka.ms/new-console-template for more information

using Host.Interface;
using Host.Services;

ISenderService serv = new SenderService();
var conn = new ConnectionService(serv);

await conn.ServerWorker();



