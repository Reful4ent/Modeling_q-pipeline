

using Modeling_DeliveryService.ConsoleV.Model;
using Modeling_q_pipeline.Model;
Console.WriteLine("FDDSFSD");
IStatistics statistics = new Statistics();
Service service = new Service(statistics);
int N = 1000;
double epsilon = 0.01d;
double t = 1.645d;
double summ = 0;
int nn = 0;
double result = 0;
List<double> RESULTS = new();
for (int m = 0; m < 10; m++)
{
    do
    {
        summ = 0;
        if (nn > N)
            N = nn;
        for (int i = 0; i < N; i++)
        {
            statistics.ResetStatistic();
            service.Start(1000,5);
            statistics.SetMainStatistics();
            summ += service.statistics.PercentUsedDetails;
        }

        summ /= N;
        nn = (int)Math.Ceiling((summ*(1-summ))/ Math.Pow(epsilon, 2) * t);
        //Console.WriteLine(nn);
    }while(nn>N);
    RESULTS.Add(summ);
    Console.WriteLine(summ);
}
Console.WriteLine();
Console.WriteLine(N);
Console.WriteLine(nn);
Console.WriteLine(summ);
Console.WriteLine();
Console.WriteLine(RESULTS.Min());
Console.WriteLine(RESULTS.Max());
Console.WriteLine(RESULTS.Max()-RESULTS.Min());