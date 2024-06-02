namespace Modeling_q_pipeline.Model;

public interface IStatistics
{
    public Dictionary<int,double> EffectivityStatistics { get; set; }
    public int CountUsedDetails { get; set; }
    public int CountRejectionDetails { get; set; }
    public int CountUnprocessedDetails { get; set; }
    public int CountAllDetails  { get; set; }
    
    
    event Action<Dictionary<int, double>>? EffectivityStatisticsGet;

    public bool IsGettedStatistic { get; }
    public double PercentUsedDetails { get; }
    public double PercentRejectionDetails { get; }
    public double PercentUnprocessedDetails  { get; }

    public void SetMainStatistics();

    public void ResetStatistic();
}