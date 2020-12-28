using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SphericalPaths
{
    public class Plane : MonoBehaviour
    {

        #region Initialization

        /// <summary>
        /// Executes once on start.
        /// </summary>
        private void Awake()
        {
            // Extract the width of the plane
            _Width = PlaneParent.transform.localScale.x * 10;

            // Extract the material used on the plane
            MeshRenderer renderer = PlaneParent.GetComponent<MeshRenderer>();
            PlaneMaterial = new Material(renderer.material);
            renderer.material = PlaneMaterial;

            // Apply texture
            if (Texture != null)
                PlaneMaterial.SetTexture("_MainTex", Texture);

            // Extracts the lights inside the plane
            PlaneLights = PlaneParent.GetComponentsInChildren<Light>()?.ToList();
        }

        /// <summary>
        /// Continuously update the intensity of the plane.
        /// </summary>
        private void Update()
        {
            PlaneLights.ForEach(x => x.intensity = Intensity);
        }

        #endregion

        #region Fields/Properties

        [Header("Plane")]

        /// <summary>
        /// References the gameobject displays the 3d sphere.
        /// </summary>
        [Tooltip("References the gameobject displays the 2d plane.")]
        [SerializeField]
        private GameObject PlaneParent;

        /// <summary>
        /// The width of the plane.
        /// </summary>
        [Tooltip("The width of the plane. Generated on awake.")]
        [SerializeField]
        private float _Width;

        /// <summary>
        /// The width of the plane.
        /// </summary>
        public float Width { get { return _Width; } }

        /// <summary>
        /// References the material used on the plane.
        /// </summary>
        private Material PlaneMaterial;

        /// <summary>
        /// An optional texture that will be applied to the plane.
        /// </summary>
        [Tooltip("An optional texture that will be applied to the plane.")]
        [SerializeField]
        private Texture Texture;

        /// <summary>
        /// References the lights inside the plane.
        /// </summary>
        private List<Light> PlaneLights;

        /// <summary>
        /// The light intensity of the plane.
        /// </summary>
        [Tooltip("The light intensity of the plane.")]
        [Range(0, 1)]
        [SerializeField]
        public float Intensity = 0.31f;

        #endregion

    }
}
