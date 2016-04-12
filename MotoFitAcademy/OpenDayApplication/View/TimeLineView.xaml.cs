using System.Windows.Shapes;
using MahApps.Metro.Controls;
using OpenDayApplication.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OpenDayApplication.View
{
  /// <summary>
  /// Interaction logic for TimeLineView.xaml
  /// </summary>
  public partial class TimeLineView : MetroWindow
  {
    private List<WorkPlanElement> _workPlanElements;

    public TimeLineView()
    {
      InitializeComponent();
    }

    private void AddHorizontalGridLines()
    {
      var converter = new BrushConverter();
      var columnsCount = timeLineGrid.ColumnDefinitions.Count - 1;
      for (int i = 1; i < timeLineGrid.RowDefinitions.Count; i++)
      {
        var line = new Line()
        {
          StrokeThickness = 1,
          Stroke = converter.ConvertFromString("#B9B9B9") as Brush,
          X2 = 600,
          VerticalAlignment = VerticalAlignment.Bottom
        };
        Grid.SetRow(line, i);
        Grid.SetColumn(line, 1);
        Grid.SetColumnSpan(line, columnsCount);
        timeLineGrid.Children.Add(line);
      }
    }

    private void AddVerticalGridLines()
    {
      var converter = new BrushConverter();
      var rowsCount = timeLineGrid.RowDefinitions.Count - 1;
      for (int i = 1; i < timeLineGrid.ColumnDefinitions.Count; i++)
      {
        var line = new Line()
        {
          StrokeThickness = 1,
          Stroke = converter.ConvertFromString("#B9B9B9") as Brush,
          HorizontalAlignment = HorizontalAlignment.Right,
          Y2 = 900
        };
        Grid.SetRow(line, 1);
        Grid.SetColumn(line, i);
        Grid.SetRowSpan(line, rowsCount);
        timeLineGrid.Children.Add(line);
      }
    }

    private void AddGraphicalElements(List<WorkPlanElement> workPlanElements)
    {
      if (workPlanElements != null)
      {
        var converter = new BrushConverter();
        foreach (var workPlanElement in workPlanElements)
        {
          var border = new Border()
          {
            Background = converter.ConvertFromString("#A0D8F0") as Brush,
            BorderBrush = converter.ConvertFromString("#41B1E1") as Brush,
            BorderThickness = new Thickness(1)
          };
          var stackPanel = new StackPanel()
          {
            Orientation = Orientation.Vertical,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
          };
          var textBox = new TextBlock()
          {
            Text = workPlanElement.Class.Name,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
          };
          var textBox1 = new TextBlock()
          {
            Text = workPlanElement.Room.Name,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
          };
          stackPanel.Children.Add(textBox);
          stackPanel.Children.Add(textBox1);
          border.Child = stackPanel;

          Grid.SetRow(border, GetGridRowIndex(workPlanElement.StartTime));
          Grid.SetRowSpan(border, GetGridRowSpanNumber(workPlanElement.StartTime, workPlanElement.EndTime));
          Grid.SetColumn(border, (int) workPlanElement.DayOfWeek + 1);

          timeLineGrid.Children.Add(border);
        }
      }
    }

    public void SetTimeTable(List<WorkPlanElement> workPlanElements)
    {
      AddGraphicalElements(workPlanElements);
      AddHorizontalGridLines();
      AddVerticalGridLines();
    }
    private int GetGridRowIndex(TimeSpan startTime)
    {
      var dayStart = new TimeSpan(9, 0, 0);
      var diff = startTime - dayStart;
      var result = diff.TotalMinutes / 30 + 1;
      return Convert.ToInt32(result);
    }
    private int GetGridRowSpanNumber(TimeSpan startTime, TimeSpan endTime)
    {
      var result = (endTime - startTime).TotalMinutes / 30;
      return Convert.ToInt32(result);
    }
  }
}
