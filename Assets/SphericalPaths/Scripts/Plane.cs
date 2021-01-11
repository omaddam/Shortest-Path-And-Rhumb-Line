using SphericalPaths.DataStructure;
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



        [Header("Points")]

        /// <summary>
        /// References the gameobject displays the points.
        /// </summary>
        [Tooltip("References the gameobject displays the points.")]
        [SerializeField]
        private GameObject PointsParent;

        /// <summary>
        /// References the template used when generating points.
        /// </summary>
        [Tooltip("References the template used when generating points.")]
        [SerializeField]
        private GameObject PointsTemplate;



        [Header("Paths")]

        /// <summary>
        /// References the gameobject displays the paths.
        /// </summary>
        [Tooltip("References the gameobject displays the paths.")]
        [SerializeField]
        private GameObject PathsParent;

        /// <summary>
        /// References the template used when generating paths.
        /// </summary>
        [Tooltip("References the template used when generating paths.")]
        [SerializeField]
        private GameObject PathsTemplate;

        #endregion

        #region Methods

        /// <summary>
        /// Clear all points.
        /// </summary>
        public void ClearPoints()
        {
            foreach (Transform entity in PointsParent.transform)
                GameObject.Destroy(entity.gameObject);
        }

        /// <summary>
        /// Appends a list of points to the sphere.
        /// </summary>
        public void DisplayPoints(List<Coordinates> coordinates, Color color)
        {
            foreach (var point in coordinates)
            {
                // Create a new entity instance
                GameObject pointInstance = Instantiate(PointsTemplate, PointsParent.transform);

                // Extract the script
                Point script = pointInstance.GetComponent<Point>();

                // Initialize data
                script.Initialize(point, color, false);
            }
        }

        /// <summary>
        /// Appends a single point to the sphere.
        /// </summary>
        public void DisplayPoints(Coordinates coordinates, Color color)
        {
            DisplayPoints(new List<Coordinates>() { coordinates }, color);
        }



        /// <summary>
        /// Clear all paths.
        /// </summary>
        public void ClearPaths()
        {
            foreach (Transform entity in PathsParent.transform)
                GameObject.Destroy(entity.gameObject);
        }

        /// <summary>
        /// Appends a list of paths to the sphere.
        /// </summary>
        public void DisplayPaths(List<DataStructure.Path> paths, Color color)
        {
            foreach (var path in paths)
            {
                // Create a new entity instance
                GameObject pathInstance = Instantiate(PathsTemplate, PathsParent.transform);

                // Extract the script
                Path script = pathInstance.GetComponent<Path>();

                // Initialize data
                script.Initialize(path, color, false);
            }
        }

        /// <summary>
        /// Appends a single path to the sphere.
        /// </summary>
        public void DisplayPaths(DataStructure.Path path, Color color)
        {
            DisplayPaths(new List<DataStructure.Path>() { path }, color);
        }

        #endregion

    }
}
