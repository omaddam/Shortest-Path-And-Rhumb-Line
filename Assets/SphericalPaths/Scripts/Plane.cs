using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphericalPaths
{
    public class Plane : MonoBehaviour
    {

        #region Fields/Properties

        [Header("Plane")]

        /// <summary>
        /// References the gameobject displays the 3d sphere.
        /// </summary>
        [Tooltip("References the gameobject displays the 2d plane.")]
        [SerializeField]
        private GameObject PlaneParent;

        #endregion

    }
}
