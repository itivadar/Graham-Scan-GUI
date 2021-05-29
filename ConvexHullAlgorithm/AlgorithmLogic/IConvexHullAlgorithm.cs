using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace ConvexHullAlgorithm
{
    public interface IConvexHullAlgorithm
    {
       /// <summary>
       /// Calculates the smallest perimeter to contains all the given points
       /// </summary>
       /// <param name="allPoints">Area of the points given</param>
       /// <returns>Stack of point which are in the perimeter</returns>
       Stack<ComparablePoint> ComputeConvexHull(ComparablePoint[] allPoints);
    }
}
