using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SphericalPaths.DataStructure
{
    public static class PathComputationMethods
    {

        #region Constants

        /// <summary>
        /// The default number of coordinates required to form a path.
        /// </summary>
        private const int RHUMB_PATH_SEGMENTS_COUNT = 15;

        /// <summary>
        /// The default number of coordinates required to form a path.
        /// </summary>
        private const int SHORTEST_PATH_SEGMENTS_COUNT = 15;

        #endregion

        #region Conversion Methods

        /// <summary>
        /// Converts an angle from degree to radian.
        /// </summary>
        /// <returns>Angle in radian.</returns>
        /// <param name="degree">Angle in degree.</param>
        private static double ConvertFromDegreeToRadian(double degree)
		{
			return Math.PI * degree / 180.0;
		}

        /// <summary>
        /// Converts cartesian coordinates from degrees to radian.
        /// </summary>
        private static Vector2 ToRadian(this Vector2 cartesian)
        {
            return new Vector2
            (
                (float)ConvertFromDegreeToRadian(cartesian.x),
                (float)ConvertFromDegreeToRadian(cartesian.y)
            );
        }

		/// <summary>
		/// Converts an angle from radian to degree.
		/// </summary>
		/// <returns>Angle in degree.</returns>
		/// <param name="radian">Angle in radian.</param>
		private static double ConvertFromRadianToDegree(double radian)
		{
			return radian * (180.0 / Math.PI);
		}

        /// <summary>
        /// Converts cartesian coordinates from radian to degrees.
        /// </summary>
        private static Vector2 ToDegree(this Vector2 cartesian)
        {
            return new Vector2
            (
                (float)ConvertFromRadianToDegree(cartesian.x),
                (float)ConvertFromRadianToDegree(cartesian.y)
            );
        }

        #endregion

        #region Rhumb Path Methods

        /// <summary>
        /// Generates the path between the two coordinates using rhumb line.
        /// </summary>
        /// <param name="start">The first coordinates in the path.</param>
        /// <param name="end">The last coordinates in the path.</param>
        /// <param name="segmentsCount">The default number of coordinates required to form the path.</param>
		public static Path GetRhumbPath(this Coordinates start, Coordinates end, 
            int segmentsCount = RHUMB_PATH_SEGMENTS_COUNT)
        {
            List<Coordinates> coordinates = GetRhumbLine(start, end, segmentsCount);
            coordinates.Insert(0, start);
            coordinates.Add(end);

            return new Path(coordinates);
        }

        /// <summary>
        /// Computes the coordinates between the two points using rhumb line.
        /// </summary>
        /// <param name="start">The first coordinates in the path.</param>
        /// <param name="end">The last coordinates in the path.</param>
        /// <param name="segmentsCount">The default number of coordinates required to form the path.</param>
        private static List<Coordinates> GetRhumbLine(Coordinates start, Coordinates end,
            int segmentsCount)
        {
            // Compute midpoint coordinates
            Coordinates midCoordinates = GetRhumMidPoint(start, end);

            // Decrement segments count
            segmentsCount--;

            // Compute left coordinates
            List<Coordinates> leftCoordinates = segmentsCount <= 0 ? null
                : GetRhumbLine(start, midCoordinates, segmentsCount / 2);

            // Compute right coordinates
            List<Coordinates> rightCoordinates = segmentsCount <= 0 ? null
                : GetRhumbLine(midCoordinates, end, segmentsCount / 2);

            // Set coordinates
            List<Coordinates> coordinates = new List<Coordinates>();
            if (leftCoordinates != null)
                coordinates.AddRange(leftCoordinates);
            coordinates.Add(midCoordinates);
            if (rightCoordinates != null)
                coordinates.AddRange(rightCoordinates);
            return coordinates;
        }

        /// <summary>
        /// Computes the midpoint between two coordinates on a rhumb line.
        /// </summary>
        private static Coordinates GetRhumMidPoint(Coordinates start, Coordinates end)
        {
            // Convert from degree to radian
            Vector2 startRadian = start.CartesianCoordinates.ToRadian();
            Vector2 endRadian = end.CartesianCoordinates.ToRadian();

            // Crossing anti-meridian
            if (Math.Abs(endRadian.x - startRadian.x) > Math.PI)
                startRadian.x += (float)(2 * Math.PI);

            // Compute midpoint latitude
            float midLat = (startRadian.y + endRadian.y) / 2f;

            // Compute midpoint longitude
            double f1 = Math.Tan(Math.PI / 4 + startRadian.y / 2);
            double f2 = Math.Tan(Math.PI / 4 + endRadian.y / 2);
            double f3 = Math.Tan(Math.PI / 4 + midLat / 2);
            float midLong = 
                (float)
                ((
                    (endRadian.x - startRadian.x) * Math.Log(f3) 
                    +
                    startRadian.x * Math.Log(f2) 
                    -
                    endRadian.x * Math.Log(f1)
                ) / Math.Log(f2 / f1));

            if (double.IsInfinity(midLong) || double.IsNaN(midLong)) // parallel of latitude
                midLong = (startRadian.x + endRadian.x) / 2;

            midLong = (float)((midLong + 3 * Math.PI) % (2 * Math.PI) - Math.PI); // normalise to -180..+180°

            // Compute midpoint
            Vector2 midRadian = new Vector2(midLong, midLat);
            return new Coordinates(midRadian.ToDegree(), start.Radius, start.Width);
        }

        #endregion

        #region Shortest Path Methods

        /// <summary>
        /// Generates the path between the two coordinates using shortest path.
        /// </summary>
        /// <param name="start">The first coordinates in the path.</param>
        /// <param name="end">The last coordinates in the path.</param>
        /// <param name="segmentsCount">The default number of coordinates required to form the path.</param>
        public static Path GetShortestPath(this Coordinates start, Coordinates end,
            int segmentsCount = SHORTEST_PATH_SEGMENTS_COUNT)
        {
            // Compute the straight line
            List<Vector3> segmentPoints = GetStraightLine(
                start.SphericalCoordinates, end.SphericalCoordinates, segmentsCount);

            // Find the projection of the straight line onto the sphere
            List<Vector3> arc = ProjectPointsOnSphere(segmentPoints, start.Radius);

            // Generate list of coordinates
            List<Coordinates> coordinates = arc.Select(x => new Coordinates(x, start.Radius, start.Width)).ToList();
            coordinates.Insert(0, start);
            coordinates.Add(end);

            return new Path(coordinates);
        }

        /// <summary>
        /// Computes the positions of points that form a straight path.
        /// </summary>
        /// <param name="start">The first coordinates in the path.</param>
        /// <param name="end">The last coordinates in the path.</param>
        /// <param name="segmentsCount">The default number of coordinates required to form the path.</param>
        /// <returns></returns>
        public static List<Vector3> GetStraightLine(Vector3 start, Vector3 end,
            int segmentsCount)
        {
            // Compute midpoint
            Vector3 midCoordinates = new Vector3
            (
                (start.x + end.x) / 2f,
                (start.y + end.y) / 2f,
                (start.z + end.z) / 2f
            );

            // Decrement segments count
            segmentsCount--;

            // Compute left coordinates
            List<Vector3> leftCoordinates = segmentsCount <= 0 ? null
                : GetStraightLine(start, midCoordinates, segmentsCount / 2);

            // Compute right coordinates
            List<Vector3> rightCoordinates = segmentsCount <= 0 ? null
                : GetStraightLine(midCoordinates, end, segmentsCount / 2);

            // Set coordinates
            List<Vector3> coordinates = new List<Vector3>();
            if (leftCoordinates != null)
                coordinates.AddRange(leftCoordinates);
            coordinates.Add(midCoordinates);
            if (rightCoordinates != null)
                coordinates.AddRange(rightCoordinates);
            return coordinates;
        }

        /// <summary>
        /// Projects points onto the sphere.
        /// </summary>
        /// <param name="points">The points to project.</param>
        /// <param name="radius">The radius of the sphere projecting the points.</param>
        public static List<Vector3> ProjectPointsOnSphere(List<Vector3> points, float radius)
        {
            // Initialize projection list
            List<Vector3> projectedPoints = new List<Vector3>();

            // Go through each point and compute its projection
            foreach (var point in points)
                projectedPoints.Add(point.normalized * radius);

            return projectedPoints;
        }

        #endregion

        #region Bearing Methods

        /// <summary>
        /// Computes the bearing angle between two coordinates.
        /// https://www.sunearthtools.com/tools/distance.php#txtDist_3
        /// </summary>
        public static float ComputeBearingAngle(Coordinates start, Coordinates end)
        {
            double lat1 = ConvertFromDegreeToRadian(start.CartesianCoordinates.y);
            double lat2 = ConvertFromDegreeToRadian(end.CartesianCoordinates.y);
            double lon1 = ConvertFromDegreeToRadian(start.CartesianCoordinates.x);
            double lon2 = ConvertFromDegreeToRadian(end.CartesianCoordinates.x);

            var dLon = Math.Abs(lon2 - lon1);
            var dLat = Math.Log(Math.Tan(lat2 / 2 + Math.PI / 4) / Math.Tan(lat1 / 2 + Math.PI / 4));

            var brng = Math.Atan2(dLon, dLat);

            return (float)ConvertFromRadianToDegree(brng);
        }

        #endregion

    }
}
