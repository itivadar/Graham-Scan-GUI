using System;
using System.ComponentModel;

namespace ConvexHullAlgorithm
{
	public delegate void MouseDownEventHandler(object sender, MouseDownEventArgs eventArgs);

	/// <summary>
	/// The event args for mouse down event.
	/// </summary>
	public class MouseDownEventArgs : EventArgs
	{
		public ComparablePoint PointPosition { get; set; }

		public MouseDownEventArgs(ComparablePoint point)
		{
			PointPosition = point;
		}
	}

	/// <summary>
	/// Implementing the default property changed behavior.
	/// </summary>
	public class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}