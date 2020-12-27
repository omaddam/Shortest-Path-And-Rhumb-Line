using SphericalPaths.DataStructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphericalPaths
{
    public class Point : MonoBehaviour
    {

        #region Initialization

        /// <summary>
        /// Initializes the point.
        /// </summary>
        public void Initialize(Coordinates coordinates, bool displayOnSphere = true)
        {
            // Set the data
            Coordinates = coordinates;

            // Set coordinates
            transform.position = displayOnSphere
                ? coordinates.SphericalCoordinates
                : coordinates.PlaneCoordinates;

            // Set rotation
            transform.up = displayOnSphere
                ? coordinates.SphericalCoordinates.normalized
                : transform.parent.up;
        }

        #endregion

        #region Fields/Properties

        /// <summary>
        /// Coordinates presented by this point.
        /// </summary>
        [Tooltip("An Coordinates presented by this point.")]
        [SerializeField]
        private Coordinates Coordinates;

        #endregion

    }
}
