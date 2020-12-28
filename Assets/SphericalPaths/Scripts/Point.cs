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
        public void Initialize(Coordinates coordinates, Color color,
            bool displayOnSphere = true)
        {
            // Set the data
            Coordinates = coordinates;

            // Set coordinates
            transform.localPosition = displayOnSphere
                ? coordinates.SphericalCoordinates
                : new Vector3(coordinates.PlaneCoordinates.x, 0, coordinates.PlaneCoordinates.y);

            // Set rotation
            transform.up = displayOnSphere
                ? coordinates.SphericalCoordinates.normalized
                : transform.parent.up;

            // Set material color
            foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.material = new Material(meshRenderer.material);
                meshRenderer.material.color = color;
            }
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
