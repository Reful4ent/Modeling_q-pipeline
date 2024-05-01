using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Modeling_q_pipeline.Model;
using Modeling_q_pipeline.View;

namespace Modeling_q_pipeline;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Statistics statistics;
    private Pipeline pipeline;
    private AdditionalWindow additionalWindow;
    private TextStatisticWindow textStatisticWindow;
    public MainWindow()
    {
        InitializeComponent();
        statistics = new Statistics();
        pipeline = new Pipeline(statistics);
        additionalWindow = new AdditionalWindow();
        textStatisticWindow = new TextStatisticWindow();
        statistics.EffectivityStatisticsGet += DrawDots;
        statistics.TimeWorkingStatitsticsGet += SetOtherStatistics;
        statistics.TextStatisticGet += SetTextStatistic;
        Graphics.Plot.XLabel("Время моделирования");
        Graphics.Plot.YLabel("Процент обработанных заявок");
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        int time = Convert.ToInt32(Modeling_Time.Text.ToString());
        int countOfDevices = Convert.ToInt32(Modeling_DevCount.Text.ToString());
        int bufferSize = Convert.ToInt32(Modeling_BufSize.Text.ToString());
        statistics.SetStartStatistics(countOfDevices);
        await pipeline.Start(time, countOfDevices, bufferSize);
        statistics.SetMainStatistics();
    }

    private void DrawDots(Dictionary<int, double> dots)
    {
        double[] data = dots.Values.ToArray();
        int[] time = dots.Keys.ToArray();
        var sp = Graphics.Plot.Add.Scatter(time, data);
        Graphics.Plot.Axes.SetLimits(time[0],time[time.Length-1],0,1);
        Graphics.Plot.Axes.AutoScale();
        sp.LegendText = $"{Modeling_DevCount.Text} : {Modeling_BufSize.Text} : {Modeling_Time.Text}";
        Graphics.Plot.ShowLegend();
        Graphics.Refresh();
    }

    private void SetOtherStatistics(List<List<int>> TimeWorkingStatitstics)
    {
        additionalWindow.GetStatistic(TimeWorkingStatitstics);
    }

    private void SetTextStatistic(string text)
    {
        textStatisticWindow.GetStatistic(text);
    }
    
    private void MenuItemOthers_OnClick(object sender, RoutedEventArgs e)
    {
        additionalWindow.Show();
    }

    private void ClearPlot(object sender, RoutedEventArgs e)
    {
        Graphics.Plot.Clear();
        Graphics.Refresh();
        textStatisticWindow.Clear();
        additionalWindow.Clear();
    }

    private void MenuItemStatistic_OnClick(object sender, RoutedEventArgs e)
    {
        textStatisticWindow.Show();
    }
}