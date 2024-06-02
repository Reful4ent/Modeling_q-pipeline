namespace Modeling_q_pipeline.Model;

public class Statistics : IStatistics
{
    public Dictionary<int,double> EffectivityStatistics { get; set; } = new();
    public int CountUsedDetails { get; set; } = 0;
    public int CountRejectionDetails { get; set; } = 0;
    public int CountUnprocessedDetails { get; set; } = 0;
    public int CountAllDetails  { get; set; } = 0;
    
    private double percentRejectionDetails = 0;
    private double percentUnprocessedDetails = 0;
    private double percentUsedDetails = 0;
    private bool isGettedStatistic = false;

    public event Action<Dictionary<int, double>>? EffectivityStatisticsGet;

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

        EffectivityStatisticsGet?.Invoke(EffectivityStatistics);
    }

    public void ResetStatistic()
    {
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