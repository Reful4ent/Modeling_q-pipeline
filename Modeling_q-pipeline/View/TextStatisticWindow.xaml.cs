using System.ComponentModel;
using System.Windows;

namespace Modeling_q_pipeline.View;

public partial class TextStatisticWindow : Window
{
    public TextStatisticWindow()
    {
        InitializeComponent();
    }
    

    private void TextStatistic_OnClosing(object? sender, CancelEventArgs e)
    {
        e.Cancel = true;
        this.Visibility = Visibility.Hidden;
    }

    public void GetStatistic(string text)
    {
        TextStatisticInfo.Text += text;
    }

    public void Clear()
    {
        TextStatisticInfo.Text = "";
    }
}