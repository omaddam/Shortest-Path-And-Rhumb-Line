using System;

namespace SphericalPaths.DataStructure
{
    public static class PathComputationMethods
    {

		#region Common Methods

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
		/// Converts an angle from radian to degree.
		/// </summary>
		/// <returns>Angle in degree.</returns>
		/// <param name="radian">Angle in radian.</param>
		private static double ConvertFromRadianToDegree(double radian)
		{
			return radian * (180.0 / Math.PI);
		}

		#endregion

	}
}
