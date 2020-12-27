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
        public void Initialize(Coordinates coordinates)
        {
            Coordinates = coordinates;
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
