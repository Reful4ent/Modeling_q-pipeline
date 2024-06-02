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
using Modeling_DeliveryService.ConsoleV.Model;
using Modeling_q_pipeline.Model;
using Modeling_q_pipeline;

namespace Modeling_q_pipeline;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IStatistics statistics;
    private Service service;
    public MainWindow()
    {
        InitializeComponent();
        statistics = Statistics.Instance();
        service = new Service(statistics);
        statistics.EffectivityStatisticsGet += DrawDots;
        Graphics.Plot.XLabel("Время моделирования");
        Graphics.Plot.YLabel("Доля обработанных заявок");
    }

    private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        int time = Convert.ToInt32(Modeling_Time.Text.ToString());
        int countOfDevices = Convert.ToInt32(Modeling_DevCount.Text.ToString());
        await service.StartAsync(time, countOfDevices);
        statistics.SetMainStatistics();
    }

    private void DrawDots(Dictionary<int, double> dots)
    {
        double[] data = dots.Values.ToArray();
        int[] time = dots.Keys.ToArray();
        var sp = Graphics.Plot.Add.Scatter(time, data);
        Graphics.Plot.Axes.SetLimits(time[0],time[time.Length-1],0,1);
        Graphics.Plot.Axes.AutoScale();
        sp.LegendText = $"{Modeling_DevCount.Text} :  {Modeling_Time.Text}";
        Graphics.Plot.ShowLegend();
        Graphics.Refresh();
    }
    
    

    private void ClearPlot(object sender, RoutedEventArgs e)
    {
        Graphics.Plot.Clear();
        Graphics.Refresh();
    }
    
}