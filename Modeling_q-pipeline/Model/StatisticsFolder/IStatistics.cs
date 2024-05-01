namespace Modeling_q_pipeline.Model;

public interface IStatistics
{
    public List<List<int>> TimeWorkingDiveces { get; set; }
    public Dictionary<int,double> EffectivityStatistics { get; set; }
    public int CountUsedDetails { get; set; }
    public int CountRejectionDetails { get; set; }
    public int CountUnprocessedDetails { get; set; }
    public int CountAllDetails  { get; set; }
    

    event Action<List<List<int>>>? TimeWorkingStatitsticsGet;
    event Action<Dictionary<int, double>>? EffectivityStatisticsGet;
    event Action<string>? TextStatisticGet;

    public bool IsGettedStatistic { get; }
    public double PercentUsedDetails { get; }
    public double PercentRejectionDetails { get; }
    public double PercentUnprocessedDetails  { get; }

    public void SetStartStatistics(int countOfDevices);

    public void SetMainStatistics();

    public void ResetStatistic(int countOfDevices);
}