namespace Modeling_Console;

public class Statistics
{
    public List<List<int>> TimeWorkingDiveces { get; set; } = new();
    public int countUsedDetails = 0;
    public int countRejectionDetails = 0;
    public int countUnprocessedDetails = 0;
    
    private int countAllDetails = 0;
    private double percentRejectionDetails = 0;
    private double percentUnprocessedDetails = 0;
    private double percentUsedDetails = 0;

    public int CountAllDetails
    {
        get => countAllDetails;
        private set => countAllDetails = value;
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
    public Statistics(int countOfDevices)
    {
        for (int i = 0; i < countOfDevices; i++)
            TimeWorkingDiveces.Add(new List<int>());
    }

    public void SetMainStatistics()
    {
        CountAllDetails = countUnprocessedDetails + countRejectionDetails + countUsedDetails;
        PercentUnprocessedDetails =(double) countUnprocessedDetails / (double)countAllDetails;
        PercentRejectionDetails = (double)countRejectionDetails /(double) countAllDetails;
        PercentUsedDetails = (double)countUsedDetails / (double)countAllDetails;
    }
    
}