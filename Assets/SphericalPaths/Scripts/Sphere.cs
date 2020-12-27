using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphericalPaths
{
    public class Sphere : MonoBehaviour
    {

        #region Initialization

        /// <summary>
        /// Executes once on start.
        /// </summary>
        private void Awake()
        {
            // Extract the radius of thye sphere
            _Radius = SphereParent.transform.localScale.x / 2f;
        }

        #endregion

        #region Fields/Properties

        [Header("Sphere")]

        /// <summary>
        /// References the gameobject displays the 3d sphere.
        /// </summary>
        [Tooltip("References the gameobject displays the 3d sphere.")]
        public GameObject SphereParent;

        /// <summary>
        /// The radius of the sphere.
        /// </summary>
        [Tooltip("The radius of the sphere. Generated on awake.")]
        [SerializeField]
        private float _Radius;

        /// <summary>
        /// The radius of the sphere.
        /// </summary>
        public float Radius { get { return _Radius; } }

        #endregion

    }
}
