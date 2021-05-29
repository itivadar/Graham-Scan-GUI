using System;
using System.Collections.Generic;

namespace ConvexHullAlgorithm.AlgorithmLogic
{
	internal class ConvexHullComputer : IConvexHullAlgorithm
	{
		/// <summary>
		/// Calculates the smallest perimeter to contains all the given points
		/// </summary>
		/// <param name="allPoints">Area of the points given</param>
		/// <returns>Stack of point which are in the perimeter</returns>
		public Stack<ComparablePoint> ComputeConvexHull(ComparablePoint[] allPoints)
		{
			var convexHull = new Stack<ComparablePoint>();
			if (allPoints.Length == 0) return convexHull;

			SortArray(allPoints, ComparablePoint.CompareByYCoordAsc);
			SortArray(allPoints, allPoints[0].CompareByAngleAscThenByDistanceDesc);

			//if we have two points or less, the perimeter are constructed by these points
			if (allPoints.Length <= 2)
			{
				for (int i = 0; i < allPoints.Length; i++)
				{
					convexHull.Push(allPoints[i]);
				}
				return convexHull;
			}
			//first two point after sorting will be alwas in the convex hull
			convexHull.Push(allPoints[0]);
			convexHull.Push(allPoints[1]);

			for (int i = 2; i < allPoints.Length; i++)
			{
				var topPoint = convexHull.Pop();
				while (convexHull.Count > 0 && ComparablePoint.CalculateCounterClockWiseTurn(convexHull.Peek(), topPoint, allPoints[i]) <= 0)
				{
					topPoint = convexHull.Pop();
				}
				convexHull.Push(topPoint);
				convexHull.Push(allPoints[i]);
			}

			return convexHull;
		}

		/// <summary>
		/// Sorts an array of points.
		/// </summary>
		/// <param name="points">the points array that needs to be sorted.</param>
		/// <param name="comparison">the comparasion function</param>
		private void SortArray(ComparablePoint[] points, Comparison<ComparablePoint> comparison)
		{
			Array.Sort(points, comparison);
		}
	}
}