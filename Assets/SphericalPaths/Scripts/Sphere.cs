using SphericalPaths.DataStructure;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            // Extract the radius of the sphere
            _Radius = SphereParent.transform.localScale.x;

            // Extract the material used on the sphere
            MeshRenderer renderer = SphereParent.GetComponent<MeshRenderer>();
            SphereMaterial = new Material(renderer.material);
            renderer.material = SphereMaterial;

            // Apply texture
            if (Texture != null)
                SphereMaterial.SetTexture("_MainTex", Texture);

            // Extracts the lights inside the sphere
            SphereLights = SphereParent.GetComponentsInChildren<Light>()?.ToList();
        }

        /// <summary>
        /// Continuously update the opacity and intensity of the sphere.
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

            SphereLights.ForEach(x => x.intensity = Intensity);
        }

        #endregion

        #region Fields/Properties

        [Header("Sphere")]

        /// <summary>
        /// References the gameobject displays the 3d sphere.
        /// </summary>
        [Tooltip("References the gameobject displays the 3d sphere.")]
        [SerializeField]
        private GameObject SphereParent;

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

        /// <summary>
        /// References the lights inside the sphere.
        /// </summary>
        private List<Light> SphereLights;

        /// <summary>
        /// The light intensity of the sphere.
        /// </summary>
        [Tooltip("The light intensity of the sphere.")]
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
                script.Initialize(point, color, true);
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
                script.Initialize(path, color, true);
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
