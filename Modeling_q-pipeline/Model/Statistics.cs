namespace Modeling_q_pipeline.Model;

public class Statistics
{
    public List<List<int>> TimeWorkingDiveces { get; set; } = new();
    public Dictionary<int,double> EffectivityStatistics { get; set; } = new();
    public int countUsedDetails = 0;
    public int countRejectionDetails = 0;
    public int countUnprocessedDetails = 0;
    public int countAllDetails = 0;
    
    private double percentRejectionDetails = 0;
    private double percentUnprocessedDetails = 0;
    private double percentUsedDetails = 0;
    private bool isGettedStatistic = false;

    public Action<List<List<int>>>? TimeWorkingStatitsticsGet;
    public Action<Dictionary<int, double>>? EffectivityStatisticsGet;
    public Action<string>? TextStatisticGet;

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
        PercentUnprocessedDetails =(double) countUnprocessedDetails / (double)countAllDetails;
        PercentRejectionDetails = (double)countRejectionDetails /(double) countAllDetails;
        PercentUsedDetails = (double)countUsedDetails / (double)countAllDetails;
        IsGettedStatistic = true;

        string textIngformation = $"Заявок не обработано: {countUnprocessedDetails}\n" +
                                  $"Обработанные заявки  + {countUsedDetails}\n" +
                                  $"Отказанные заявки  {countRejectionDetails}\n" +
                                  $"Заявок всего: {countAllDetails}\n" +
                                  $"Процент обработанных заявок: {PercentUsedDetails}\n" +
                                  $"Процент отказанных заявок: {PercentRejectionDetails}\n" +
                                  $"Процент не обработанных заявок: {PercentUnprocessedDetails}\n \n";
        
        
        
        
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
        countUsedDetails = 0;
        countRejectionDetails = 0;
        countUnprocessedDetails = 0;
        countAllDetails = 0;
        PercentRejectionDetails = 0;
        PercentUnprocessedDetails = 0;
        PercentUsedDetails = 0;
        IsGettedStatistic = false;
    }
}