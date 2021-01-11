using UnityEngine;

namespace SphericalPaths
{
    public class Path : MonoBehaviour
    {

        #region Initialization

        /// <summary>
        /// Executes once on awake.
        /// </summary>
        private void Awake()
        {
            LineRenderer = GetComponent<LineRenderer>();
        }

        /// <summary>
        /// Initializes the path.
        /// </summary>
        public void Initialize(DataStructure.Path path, Color color,
            bool displayOnSphere = true)
        {
            // Set the data
            _Path = path;

            // Initialize the line renderer
            LineRenderer.positionCount = path.Coordinates.Count;

            // Set color
            LineRenderer.startColor = color;
            LineRenderer.endColor = color;

            // Display line
            LineRenderer.useWorldSpace = false;
            for (int i = 0; i < path.Coordinates.Count; i++)
            {
                LineRenderer.SetPosition(i, displayOnSphere
                ? path.Coordinates[i].SphericalCoordinates
                : new Vector3(path.Coordinates[i].PlaneCoordinates.x, 0, path.Coordinates[i].PlaneCoordinates.y));
            }
        }

        #endregion

        #region Fields/Properties

        /// <summary>
        /// References the line renderer attached to this object that displays the path.
        /// </summary>
        private LineRenderer LineRenderer;

        /// <summary>
        /// The path being presented.
        /// </summary>
        [Tooltip("The path being presented.")]
        [SerializeField]
        private DataStructure.Path _Path;

        #endregion

    }
}
