namespace Modeling_q_pipeline.Model;

public class Statistics : IStatistics
{
    public List<List<int>> TimeWorkingDiveces { get; set; } = new();
    public Dictionary<int,double> EffectivityStatistics { get; set; } = new();
    public int CountUsedDetails { get; set; } = 0;
    public int CountRejectionDetails { get; set; } = 0;
    public int CountUnprocessedDetails { get; set; } = 0;
    public int CountAllDetails  { get; set; } = 0;
    
    private double percentRejectionDetails = 0;
    private double percentUnprocessedDetails = 0;
    private double percentUsedDetails = 0;
    private bool isGettedStatistic = false;

    public event Action<List<List<int>>>? TimeWorkingStatitsticsGet;
    public event Action<Dictionary<int, double>>? EffectivityStatisticsGet;
    public event Action<string>? TextStatisticGet;

    public static Statistics Instance() => new();
    public bool IsGettedStatistic
    {
        get => isGettedStatistic;
        private set => isGettedStatistic = value;
    }
    public double PercentUsedDetails
    {
        get => percentUsedDetails;
        private set => percentUsedDetails = value;
    }
    public double PercentRejectionDetails
    {
        get => percentRejectionDetails;
        private set => percentRejectionDetails = value;
    }
    public double PercentUnprocessedDetails
    {
        get => percentUnprocessedDetails;
        private set => percentUnprocessedDetails = value;
    }
    
    public void SetStartStatistics(int countOfDevices)
    {
        for (int i = 0; i < countOfDevices; i++)
            TimeWorkingDiveces.Add(new List<int>());
    }
    
    public void SetMainStatistics()
    {
        PercentUnprocessedDetails =(double) CountUnprocessedDetails / (double)CountAllDetails;
        PercentRejectionDetails = (double)CountRejectionDetails /(double) CountAllDetails;
        PercentUsedDetails = (double)CountUsedDetails / (double)CountAllDetails;
        IsGettedStatistic = true;

        string textIngformation = $"Заявок не обработано: {CountUnprocessedDetails}\n" +
                                  $"Обработанные заявки: {CountUsedDetails}\n" +
                                  $"Отказанные заявки: {CountRejectionDetails}\n" +
                                  $"Заявок всего: {CountAllDetails}\n" +
                                  $"Процент обработанных заявок: {PercentUsedDetails}\n" +
                                  $"Процент отказанных заявок: {PercentRejectionDetails}\n" +
                                  $"Процент не обработанных заявок: {PercentUnprocessedDetails}\n";

        for (int i = 0; i < TimeWorkingDiveces.Count; i++)
        {
            textIngformation += $"Среднее время работы устройства под номером {i + 1}: {(double)TimeWorkingDiveces[i].Sum()/TimeWorkingDiveces[i].Count}\n";
            if (i == TimeWorkingDiveces.Count - 1)
                textIngformation += "\n";
        }
        TimeWorkingStatitsticsGet?.Invoke(TimeWorkingDiveces);
        TextStatisticGet?.Invoke(textIngformation);
        EffectivityStatisticsGet?.Invoke(EffectivityStatistics);
    }

    public void ResetStatistic(int countOfDevices)
    {
        TimeWorkingDiveces.Clear();
        for (int i = 0; i < countOfDevices; i++)
            TimeWorkingDiveces.Add(new List<int>());
        EffectivityStatistics.Clear();
        CountUsedDetails = 0;
        CountRejectionDetails = 0;
        CountUnprocessedDetails = 0;
        CountAllDetails = 0;
        PercentRejectionDetails = 0;
        PercentUnprocessedDetails = 0;
        PercentUsedDetails = 0;
        IsGettedStatistic = false;
    }
}