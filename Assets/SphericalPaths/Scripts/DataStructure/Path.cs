using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SphericalPaths.DataStructure
{
    [Serializable]
    public class Path
    {

        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Path()
            : this(new List<Coordinates>())
        {
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="coordinates">List of coordinates sorted in the order that defines the path.</param>
        public Path(List<Coordinates> coordinates)
        {
            _Coordinates = coordinates;
        }

        /// <summary>
        /// Clone constructor.
        /// </summary>
        /// <param name="coordinates">Instance to clone.</param>
        public Path(Path path)
            : this()
        {
            if (path.Coordinates != null)
                foreach (var coordinates in path.Coordinates)
                    _Coordinates.Add(new Coordinates(coordinates));
        }

        #endregion

        #region Fields/Properties

        /// <summary>
        /// List of coordinates sorted in the order that defines the path.
        /// </summary>
        [SerializeField]
        [Tooltip("List of coordinates sorted in the order that defines the path.")]
        private List<Coordinates> _Coordinates;

        /// <summary>
        /// List of coordinates sorted in the order that defines the path.
        /// </summary>
        public List<Coordinates> Coordinates { get { return _Coordinates; } }



        /// <summary>
        /// Start first coordinates in the path.
        /// </summary>
        public Coordinates Start { get { return _Coordinates?.FirstOrDefault(); } }

        /// <summary>
        /// Start last coordinates in the path.
        /// </summary>
        public Coordinates Last { get { return _Coordinates?.LastOrDefault(); } }

        #endregion

        #region Methods

        /// <summary>
        /// Displays first and last coordinates in the path.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[Lat: {0} | Long: {1}] --{2}--> [Lat: {3} | Long: {4}]",
                Start?.CartesianCoordinates.y, Start?.CartesianCoordinates.x,
                _Coordinates?.Count,
                Last?.CartesianCoordinates.y, Last?.CartesianCoordinates.x);
        }

        #endregion

    }
}
