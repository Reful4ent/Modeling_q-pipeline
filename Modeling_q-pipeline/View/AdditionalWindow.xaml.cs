using System.ComponentModel;
using System.Windows;

namespace Modeling_q_pipeline.View;

public partial class AdditionalWindow : Window
{
    public AdditionalWindow()
    {
        InitializeComponent();
    }

    public void GetStatistic(List<List<int>> TimeWorkingStatitstics)
    {
        string text = "";
        double[] devicesScore = new double[TimeWorkingStatitstics.Count];
        double[] countUsedDetails = new double[TimeWorkingStatitstics.Count];
        for (int i = 0; i < TimeWorkingStatitstics.Count; i++)
        {
            devicesScore[i] = i + 1;
            countUsedDetails[i] = TimeWorkingStatitstics[i].Count;
            text += $"{TimeWorkingStatitstics[i].Count} ";
        }
        var barsPlot = WpfPlot.Plot.Add.Bars(devicesScore,countUsedDetails);
        barsPlot.LegendText = text;
        WpfPlot.Refresh();
    }

    private void AdditionalWindow_OnClosing(object? sender, CancelEventArgs e)
    {
        e.Cancel = true;
        this.Visibility = Visibility.Hidden;
    }

    public void Clear()
    {
        WpfPlot.Plot.Clear();
        WpfPlot.Refresh();
    }
}