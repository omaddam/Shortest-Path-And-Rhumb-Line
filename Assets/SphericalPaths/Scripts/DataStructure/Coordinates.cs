using System;
using UnityEngine;

namespace SphericalPaths.DataStructure
{
    [Serializable]
    public class Coordinates
    {

        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Coordinates()
            : this(Vector2.zero)
        {
        }

        /// <summary>
        /// Cartesion constructor.
        /// </summary>
        /// <param name="cartesianCoordinates">
        /// Longitude and latitude coordinates.
        /// x = longitude [-180, 180].
        /// y = latitude [-90, 90].
        /// </param>
        /// <param name="radius">Radius of the 3d sphere displaying coordinates.</param>
        /// <param name="width">Width of the 2d plane displaying coordinates.</param>
        public Coordinates(Vector2 cartesianCoordinates, 
            float radius = 1, float width = 1)
        {
            // Set cartesian coordinates
            _CartesianCoordinates = cartesianCoordinates;

            // Set plane coordinates
            _Width = width;
            _PlaneCoordinates = ConvertCartesianToPlaneCoordinates(cartesianCoordinates, width);

            // Set spherical coordinates
            _Radius = radius;
            _SphericalCoordinates = ConvertCartesianToSphericalCoordinates(cartesianCoordinates, radius);
        }

        /// <summary>
        /// Spherical constructor.
        /// </summary>
        /// <param name="sphericalCoordinates">
        /// X, Y, and Z coordinates normalized to be displayed on the 3d sphere.
        /// </param>
        /// <param name="radius">Radius of the 3d sphere displaying coordinates.</param>
        /// <param name="width">Width of the 2d plane displaying coordinates.</param>
        public Coordinates(Vector3 sphericalCoordinates,
            float radius = 1, float width = 1)
        {
            // Set spherical coordinates
            _Radius = radius;
            _SphericalCoordinates = sphericalCoordinates;

            // Set cartesian coordinates
            _CartesianCoordinates = ConvertSphericalToCartesianCoordinates(sphericalCoordinates, radius);

            // Set plane coordinates
            _Width = width;
            _PlaneCoordinates = ConvertCartesianToPlaneCoordinates(_CartesianCoordinates, width);
        }

        /// <summary>
        /// Clone constructor.
        /// </summary>
        /// <param name="coordinates">Instance to clone.</param>
        public Coordinates(Coordinates coordinates)
            : this(coordinates.CartesianCoordinates, coordinates.Radius, coordinates.Width)
        {
        }

        #endregion

        #region Fields/Properties

        [Header("Cartesian")]

        /// <summary>
        /// Longitude and latitude coordinates.
        /// x = longitude [-180, 180].
        /// y = latitude [-90, 90].
        /// </summary>
        [SerializeField]
        [Tooltip("Longitude and latitude coordinates. [x = longitude, y = latitude]")]
        private Vector2 _CartesianCoordinates;

        /// <summary>
        /// Longitude and latitude coordinates.
        /// x = longitude [-180, 180].
        /// y = latitude [-90, 90].
        /// </summary>
        public Vector2 CartesianCoordinates { get { return _CartesianCoordinates; } }



        [Header("Plane")]

        /// <summary>
        /// Width of the 2d plane displaying coordinates.
        /// </summary>
        [SerializeField]
        [Tooltip("Width of the 2d plane displaying coordinates.")]
        private float _Width;

        /// <summary>
        /// Width of the 2d plane displaying coordinates.
        /// </summary>
        public float Width { get { return _Width; } }

        /// <summary>
        /// X and Y coordinates normalized to be displayed on the 2d plane.
        /// </summary>
        [SerializeField]
        [Tooltip("X and Y coordinates normalized to be displayed on the 2d plane.")]
        private Vector2 _PlaneCoordinates;

        /// <summary>
        /// X and Y coordinates normalized to be displayed on the 2d plane.
        /// </summary>
        public Vector2 PlaneCoordinates { get { return _PlaneCoordinates; } }




        [Header("Sphere")]

        /// <summary>
        /// Radius of the 3d sphere displaying coordinates.
        /// </summary>
        [SerializeField]
        [Tooltip("Radius of the 3d sphere displaying coordinates.")]
        private float _Radius;

        /// <summary>
        /// Radius of the 3d sphere displaying coordinates.
        /// </summary>
        public float Radius { get { return _Radius; } }

        /// <summary>
        /// X, Y, and Z coordinates normalized to be displayed on the 3d sphere.
        /// </summary>
        [SerializeField]
        [Tooltip("X and Y coordinates normalized to be displayed on the 2d plane.")]
        private Vector3 _SphericalCoordinates;

        /// <summary>
        /// X, Y, and Z coordinates normalized to be displayed on the 3d sphere.
        /// </summary>
        public Vector3 SphericalCoordinates { get { return _SphericalCoordinates; } }

        #endregion

        #region Methods

        /// <summary>
        /// Converts cartesian coordinates to a plane coordinates.
        /// </summary>
        /// <param name="cartesianCoordinates">Longitude and latitude coordinates. x = longitude [-180, 180]. y = latitude [-90, 90].</param>
        /// <param name="radius">Width of the 2d plane displaying coordinates.</param>
        private Vector2 ConvertCartesianToPlaneCoordinates(Vector2 cartesianCoordinates, float radius)
        {
            return new Vector2
            (
                cartesianCoordinates.x / 180f * radius,
                cartesianCoordinates.y / 90f * radius
            );
        }

        /// <summary>
        /// Converts cartesian coordinates to a spherical coordinates.
        /// </summary>
        /// <param name="cartesianCoordinates">Longitude and latitude coordinates. x = longitude [-180, 180]. y = latitude [-90, 90].</param>
        /// <param name="radius">Width of the 2d plane displaying coordinates.</param>
        private Vector3 ConvertCartesianToSphericalCoordinates(Vector2 cartesianCoordinates, float radius)
        {
            double longitude = Math.PI * cartesianCoordinates.x / 180.0f;
            double latitude = Math.PI * cartesianCoordinates.y / 180.0f;

            // Adjust position by radians	
            latitude -= 1.570795765134f; // subtract 90 degrees (in radians)
            
            return new Vector3
            (
                (float)(radius * Math.Sin(latitude) * Math.Cos(longitude)),
                (float)(radius * Math.Cos(latitude)),
                (float)(radius * Math.Sin(latitude) * Math.Sin(longitude))
            );
        }

        /// <summary>
        /// Converts spherical coordinates to cartesian coordinates.
        /// </summary>
        /// <param name="radius">Radius of the 3d sphere displaying coordinates.</param>
        private Vector2 ConvertSphericalToCartesianCoordinates(Vector3 sphericalCoordinates, float radius)
        {
            float latitude = (float)Math.Acos(sphericalCoordinates.y / radius);
            float longitude = (float)Math.Atan(sphericalCoordinates.z / sphericalCoordinates.x);

            latitude = 90.0f - (latitude * Mathf.Rad2Deg);

            if (sphericalCoordinates.x < 0)
                longitude = longitude * Mathf.Rad2Deg;
            else if (sphericalCoordinates.z > 0 && sphericalCoordinates.x > 0)
                longitude = -(180.0f - longitude * Mathf.Rad2Deg);
            else if (sphericalCoordinates.z < 0 && sphericalCoordinates.x > 0)
                longitude = 180.0f + longitude * Mathf.Rad2Deg;

            return new Vector2(longitude, latitude);
        }

        /// <summary>
        /// Displays all the coordinates.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Cartesian coordinates: {0}\nPlane coordinates: {1} => {2}\nSpherical coordinates: {3} => {4}",
                _CartesianCoordinates, _Width, _PlaneCoordinates, _Radius, _SphericalCoordinates);
        }

        #endregion

    }
}
