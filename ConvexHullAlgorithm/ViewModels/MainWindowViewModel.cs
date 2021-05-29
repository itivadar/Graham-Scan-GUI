using ConvexHullAlgorithm.AlgorithmLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;

namespace ConvexHullAlgorithm.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		#region Private fields

		private const int CircleSize = 10;

		private IConvexHullAlgorithm _convexHullAlgorithm;
		private List<ComparablePoint> _circlesCoordinates;

		#endregion Private fields

		#region Events

		/// <summary>
		/// Event triggered when the user clicks on canvas.
		/// </summary>
		public event MouseDownEventHandler MouseClickedEvent;

		#endregion Events

		#region Binding Proprieties

		/// <summary>
		/// Gets or sets the list of UI elements on canvas.
		/// </summary>
		public ObservableCollection<UIElement> ItemsOnCanvas { get; set; }

		#endregion Binding Proprieties

		#region Commands

		/// <summary>
		/// Gets the command for clearing the board.
		/// </summary>
		public ICommand ClearBoardCommand { get; private set; }

		/// <summary>
		/// Gets the command for drawing the convex hull.
		/// </summary>
		public ICommand DrawLineCommand { get; private set; }

		/// <summary>
		/// Gets the comamnd
		/// </summary>
		public ICommand ClearLinesCommand { get; private set; }

		/// <summary>
		/// Gets the commadn for drawing random points on the canvas.
		/// </summary>
		public ICommand DrawRandomPointsCommand { get; private set; }

		#endregion Commands

		#region Constructor

		public MainWindowViewModel()
		{
			_convexHullAlgorithm = new ConvexHullComputer();
			_circlesCoordinates = new List<ComparablePoint>();

			ItemsOnCanvas = new ObservableCollection<UIElement>();

			MouseClickedEvent += MouseClickedEventHandler;
			InitCommands();
		}

		#endregion Constructor

		#region Public Methods

		public void OnMouseClick(object sender, MouseDownEventArgs eventArgs)
		{
			MouseClickedEvent?.Invoke(sender, eventArgs);
		}

		#endregion Public Methods

		#region Private Methods

		/// <summary>
		/// Instantiate each command with its command handler.
		/// </summary>
		private void InitCommands()
		{
			ClearBoardCommand = new DelegateCommand(ClearBoardAction);
			DrawLineCommand = new DelegateCommand(DrawLine);
			ClearLinesCommand = new DelegateCommand(ClearLines);
			DrawRandomPointsCommand = new DelegateCommand(DrawRandomPoints);
		}

		//Draw connecting lines for each pair of adjacent points.
		private void DrawConnectingLines(ComparablePoint[] points)
		{
			for (int i = 1; i <= points.Length; i++)
			{
				Line line = new Line
				{
					X1 = points[i - 1].CanvasX,
					Y1 = points[i - 1].CanvasY,
					X2 = points[i % points.Length].CanvasX,
					Y2 = points[i % points.Length].CanvasY,
					StrokeThickness = 3,
					Stroke = Brushes.DarkViolet
				};
				ItemsOnCanvas.Add(line);
			}
		}

		//Draw an circle at the given position
		private void DrawPoint(ComparablePoint position)
		{
			var ellipse = CreateEllipseAtPosition(position);
			_circlesCoordinates.Add(position);
			ItemsOnCanvas.Add(ellipse);
		}

		//Creates an circle at the given position
		//The centre of the circle will be determing by the given point
		private Ellipse CreateEllipseAtPosition(ComparablePoint position)
		{
			Ellipse createdEllipse = new Ellipse
			{
				Height = CircleSize,
				Width = CircleSize,
				Stroke = Brushes.DarkOliveGreen,
				Fill = Brushes.DarkOliveGreen
			};
			
			Canvas.SetLeft(createdEllipse, position.CanvasX - CircleSize / 2);
			Canvas.SetTop(createdEllipse, position.CanvasY - CircleSize / 2);

			return createdEllipse;
		}

		#endregion Private Methods

		#region Command Handlers

		//Handles the MouseClickedEvent
		//Should use Binding
		private void MouseClickedEventHandler(object sender, MouseDownEventArgs eventArgs)
		{
			DrawPoint(eventArgs.PointPosition);
		}

		private void DrawRandomPoints(object param)
		{
			Random rand = new Random();
			for (int i = 1; i <= 500; i++)
			{
				var randX = rand.Next(CircleSize / 2, 610 - CircleSize / 2);
				var randY = rand.Next(CircleSize / 2, 680 - CircleSize / 2);

				var comparablePoint = new ComparablePoint(randX, randY);
				DrawPoint(comparablePoint);
			}
		}

		//Clears all the board from drawn lines and circle
		private void ClearBoardAction(object param)
		{
			ItemsOnCanvas.Clear();
			_circlesCoordinates.Clear();
		}

		private void DrawLine(object param)
		{
			var points = _convexHullAlgorithm.ComputeConvexHull(_circlesCoordinates.ToArray()).ToArray();
			ClearLines(null);
			DrawConnectingLines(points);
		}

		private void ClearLines(object param)
		{
			for (int i = 0; i < ItemsOnCanvas.Count; i++)
			{
				if (ItemsOnCanvas[i] is Line)
				{
					ItemsOnCanvas.RemoveAt(i);
					i--;
				}
			}
		}

		#endregion Command Handlers
	}
}