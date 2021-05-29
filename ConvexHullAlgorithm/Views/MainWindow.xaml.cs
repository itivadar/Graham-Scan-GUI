
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ConvexHullAlgorithm.ViewModels;

namespace ConvexHullAlgorithm
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {

    private MainWindowViewModel _mainWindowViewModel;

    public MainWindow()
    {
      _mainWindowViewModel = new MainWindowViewModel();
      DataContext = _mainWindowViewModel;
      InitializeComponent();
    }


    private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      var circlePosition = e.GetPosition(sender as ItemsControl);
      MouseDownEventArgs mouseDownEventArgs = new MouseDownEventArgs(new ComparablePoint(circlePosition.X, circlePosition.Y));
      _mainWindowViewModel.OnMouseClick(sender, mouseDownEventArgs);
    }

  }
}
