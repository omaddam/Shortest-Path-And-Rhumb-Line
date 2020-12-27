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

            // Extract the material used on the sphere
            MeshRenderer renderer = SphereParent.GetComponent<MeshRenderer>();
            SphereMaterial = new Material(renderer.material);
            renderer.material = SphereMaterial;

            // Apply texture
            if (Texture != null)
                renderer.material.SetTexture("_MainTex", Texture);
        }

        /// <summary>
        /// Continuously update the opacity of the sphere.
        /// </summary>
        private void Update()
        {
            SphereMaterial.color = new Color
            (
                SphereMaterial.color.r,
                SphereMaterial.color.g,
                SphereMaterial.color.b,
                Opacity
            );
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

        /// <summary>
        /// References the material used on the sphere.
        /// </summary>
        private Material SphereMaterial;

        /// <summary>
        /// An optional texture that will be applied to the sphere.
        /// </summary>
        [Tooltip("An optional texture that will be applied to the sphere.")]
        [SerializeField]
        private Texture Texture;

        /// <summary>
        /// The opacity of the sphere.
        /// </summary>
        [Tooltip("The opacity of the sphere.")]
        [Range(0, 1)]
        [SerializeField]
        public float Opacity = 1;

        #endregion

    }
}
