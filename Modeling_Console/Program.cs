// See https://aka.ms/new-console-template for more information

using Modeling_Console;

int countOfDevices = Convert.ToInt32(Console.ReadLine());
Statistics statistics = new Statistics(countOfDevices);
Pipeline pipeline = new Pipeline(statistics);
pipeline.StartWOBuffer(1000,countOfDevices);
Console.WriteLine("Заявок необработано " + statistics.countUnprocessedDetails);
Console.WriteLine("Обработанные заявки " + statistics.countUsedDetails);
Console.WriteLine("Отказанные заявки " + statistics.countRejectionDetails);
Console.WriteLine("Заявок всего: " + (statistics.countUnprocessedDetails+ statistics.countUsedDetails+statistics.countRejectionDetails));
statistics.SetMainStatistics();
Console.WriteLine(statistics.CountAllDetails);
Console.WriteLine(statistics.PercentUsedDetails);
Console.WriteLine(statistics.PercentRejectionDetails);
Console.WriteLine(statistics.PercentUnprocessedDetails);
Console.WriteLine();
pipeline.StartWithBuffer(1000,countOfDevices);
Console.WriteLine("Заявок необработано " + statistics.countUnprocessedDetails);
Console.WriteLine("Обработанные заявки " + statistics.countUsedDetails);
Console.WriteLine("Отказанные заявки " + statistics.countRejectionDetails);
Console.WriteLine("Заявок всего: " + (statistics.countUnprocessedDetails+ statistics.countUsedDetails+statistics.countRejectionDetails));