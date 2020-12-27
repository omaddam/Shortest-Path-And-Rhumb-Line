using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphericalPaths
{
    public class SphereRotation : MonoBehaviour
    {

        #region Initialization



        #endregion

        #region Fields/Properties

        /// <summary>
        /// The longitude the sphere is currently focused on.
        /// </summary>
        [Tooltip("The longitude the sphere is currently focused on.")]
        [SerializeField]
        private float Longitude;

        /// <summary>
        /// The latitude the sphere is currently focused on.
        /// </summary>
        [Tooltip("The latitude the sphere is currently focused on.")]
        [SerializeField]
        private float Latitude;

        #endregion

        #region Methods

        /// <summary>
        /// Focuses on a specific coordinates.
        /// </summary>
        public void Focus(float longitude, float latitude)
        {
            Longitude = longitude;
            Latitude = latitude;

            ApplyPositionToTransform();
        }

        /// <summary>
        /// Rotates the object  to focus on the computed longitude and latitude.
        /// </summary>
        private void ApplyPositionToTransform()
        {
            Quaternion rotation = Quaternion.Euler(Latitude, -Longitude, 0);
            transform.rotation = rotation;
        }

        #endregion

    }
}
