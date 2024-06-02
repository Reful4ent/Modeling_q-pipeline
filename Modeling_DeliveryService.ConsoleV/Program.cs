// See https://aka.ms/new-console-template for more information

using Modeling_DeliveryService.ConsoleV.Model;
using Modeling_q_pipeline.Model;

Console.WriteLine("Hello, World!");
IStatistics statistics = new Statistics();
Service service = new Service(statistics);
service.Start(1000,5);