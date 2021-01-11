using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathUIManager : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Executes once on awake.
    /// </summary>
    private void Awake()
    {
        GetComponent<Text>().text = string.Format
        (
            "{0} [Lat: {1:0.000}, Lon: {2:0.000}] to {3} [Lat: {4:0.000}, Lon: {5:0.000}] | Bearing: {6:0.000}°",
            PathsScriptableObject.StartLabel,
            PathsScriptableObject.StartCoordinates.CartesianCoordinates.y,
            PathsScriptableObject.StartCoordinates.CartesianCoordinates.x,
            PathsScriptableObject.EndLabel,
            PathsScriptableObject.EndCoordinates.CartesianCoordinates.y,
            PathsScriptableObject.EndCoordinates.CartesianCoordinates.x,
            SphericalPaths.DataStructure.PathComputationMethods.ComputeRhumbPathBearingAngle(PathsScriptableObject.StartCoordinates, PathsScriptableObject.EndCoordinates)
        );
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the scriptable object that stores the demo information.
    /// </summary>
    [Tooltip("References the scriptable object that stores the demo information.")]
    [SerializeField]
    private PathManagerScriptableObject PathsScriptableObject;

    #endregion

}
