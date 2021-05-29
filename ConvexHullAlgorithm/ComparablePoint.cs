using System;

namespace ConvexHullAlgorithm
{
	public class ComparablePoint : IComparable
	{
		//Coordinates used for displaying shapes on the screen
		public double CanvasX { get; set; }

		public double CanvasY { get; set; }

		//Coordinates used for geometric calculations.
		public double GeometricX
		{
			get => CanvasX;
			set => CanvasX = value;
		}

		public double GeometricY
		{
			get => -CanvasY;
			set => CanvasY = -CanvasY;
		}

		public ComparablePoint()
		{
			CanvasX = 0;
			CanvasY = 0;
		}

		public ComparablePoint(double x, double y)
		{
			CanvasX = x;
			CanvasY = y;
		}

		/// <summary>
		/// Compares two points by their polar angle. If the points have equals polar angles, it compares them by the distance to the origin.
		/// </summary>
		/// <param name="fistPoint">the first point to compared</param>
		/// <param name="secondPoint">the second point to compared</param>
		/// <returns>-1 if the angle or the distance of the first point is smaller
		///						0 if the angle and the distance are identical for both points.
		///						1 if the angle or the distance of the first point is bigger.
		///	</returns>
		public int CompareByAngleAscThenByDistanceDesc(ComparablePoint fistPoint, ComparablePoint secondPoint)
		{
			int resByAngle = CompareByPolarAngleAsc(fistPoint, secondPoint);
			if (resByAngle != 0) return resByAngle;

			return ComapreByDistanceDesc(fistPoint, secondPoint);
		}

		/// <summary>
		/// Compares two points by their distance to an origin.
		/// </summary>
		/// <param name="fistPoint">the first point to compared</param>
		/// <param name="secondPoint">the second point to compared</param>
		/// <returns>-1 if the the distance of the first point is smaller
		///						0 if the distances are identical for both points.
		///						1 if the distance of the first point is bigger.
		///	</returns>
		public int ComapreByDistanceDesc(ComparablePoint firstPoint, ComparablePoint secondPoint)
		{
			double firstPointDistance = GetDistance(this, firstPoint);
			double secondPointDistance = GetDistance(this, secondPoint);

			if (firstPointDistance > secondPointDistance) return -1;
			if (firstPointDistance < secondPointDistance) return 1;
			return 0;
		}

		/// <summary>
		/// Compare the angle of the slope of the two lines, each defined by a given point with this instance of a point.
		/// This instance is considered the reference.
		/// <param name="firstPoint"></param>
		/// <param name="secondPoint"></param>
		/// <returns>-1 if the angle of the slope for the line defined by this point and firstPoint is smaller.
		///           1 if the angle of the slope for the line defined by this point and secondPoint is smaller.
		///           0 if the slope of both lines are equal..
		/// </returns>
		public int CompareByPolarAngleAsc(ComparablePoint firstPoint, ComparablePoint secondPoint)
		{
			double firstPointAngle = CalculateAngle(this, firstPoint);
			double secondPointAngle = CalculateAngle(this, secondPoint);

			if (firstPointAngle < secondPointAngle) return -1;
			if (firstPointAngle > secondPointAngle) return 1;

			return 0;
		}

		/// <summary>
		/// Compares the Y coordinates of two points.
		/// If Y coord are equal, compares the X coordoantes
		/// </summary>
		/// <param name="firstPoint"></param>
		/// <param name="secondPoint"></param>
		/// <returns>-1 if Y coord of the firstPoint is smaller.
		///           1 if Y coord of the secondPoint is smaller.
		///           if Y coord are equal, the X coords are compared in the same way
		///           0, if both Y and X coords are equal
		/// </returns>
		public static int CompareByYCoordAsc(ComparablePoint firstPoint, ComparablePoint secondPoint)
		{
			if (firstPoint.GeometricY < secondPoint.GeometricY) return -1;
			if (firstPoint.GeometricY > secondPoint.GeometricY) return 1;
			if (firstPoint.GeometricX < secondPoint.GeometricX) return -1;
			if (firstPoint.GeometricX > secondPoint.GeometricX) return 1;
			return 0;
		}

		/// <summary>
		/// Calculates the angle of the slope for the line defined by two given points.
		/// </summary>
		/// <returns>the angle of the slope in degrees</returns>
		private static double CalculateAngle(ComparablePoint firstPoint, ComparablePoint secondPoint)
		{
			double deltaY = secondPoint.GeometricY - firstPoint.GeometricY;
			double deltaX = secondPoint.GeometricX - firstPoint.GeometricX;
			double angle = Math.Atan2(deltaY, deltaX) * (180f / Math.PI);

			if (deltaX < 0) angle += 180;
			if (deltaY < 0) angle += 180;
			return angle;
		}

		/// <summary>
		/// Calculates distance between two given points
		/// </summary>
		/// <param name="fistPoint"></param>
		/// <param name="secondPoint"></param>
		/// <returns>the distance between points</returns>
		public static double GetDistance(ComparablePoint fistPoint, ComparablePoint secondPoint)
		{
			double distX = secondPoint.GeometricX - fistPoint.GeometricX;
			double distY = secondPoint.GeometricY - fistPoint.GeometricY;

			double distance = Math.Sqrt(Math.Pow(distX, 2) + Math.Pow(distY, 2));
			return distance;
		}

		/// <summary>
		/// Determines if we go counterclockwise if we connect the p3 points to the line formed by p1 and p2 points.
		/// </summary>
		/// <returns>-1 if the direction is clockwise
		///           1 if the direction is counterclokcwise
		///           0 if all the points are collinear
		/// </returns>
		public static int CalculateCounterClockWiseTurn(ComparablePoint p1, ComparablePoint p2, ComparablePoint p3)
		{
			double doubleArea = (p2.GeometricX - p1.GeometricX) * (p3.GeometricY - p1.GeometricY) - (p2.GeometricY - p1.GeometricY) * (p3.GeometricX - p1.GeometricX);

			if (doubleArea < 0) return -1; //clockwise
			if (doubleArea > 0) return 1; //counterclockwise
			return 0; //collinear
		}

		//Compares points by their distance to the point with coordinates (0,0)
		public int CompareTo(object obj)
		{
			ComparablePoint otherPoint = obj as ComparablePoint;
			ComparablePoint zeroPoint = new ComparablePoint(0, 0);

			var firstPointToZeroDistance = GetDistance(this, zeroPoint);
			var secondPointToZeroDistance = GetDistance(otherPoint, zeroPoint);

			if (firstPointToZeroDistance < secondPointToZeroDistance) return -1;
			if (firstPointToZeroDistance > secondPointToZeroDistance) return 1;

			return 0;
		}
	}
}