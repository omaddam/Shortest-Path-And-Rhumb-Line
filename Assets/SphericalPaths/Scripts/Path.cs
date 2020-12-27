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
        /// <param name="path"></param>
        public void Initialize(DataStructure.Path path)
        {
            _Path = path;
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
