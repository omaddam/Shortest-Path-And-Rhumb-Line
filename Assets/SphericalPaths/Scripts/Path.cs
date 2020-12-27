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

        #endregion

        #region Fields/Properties

        /// <summary>
        /// References the line renderer attached to this object that displays the path.
        /// </summary>
        private LineRenderer LineRenderer;

        #endregion

    }
}
