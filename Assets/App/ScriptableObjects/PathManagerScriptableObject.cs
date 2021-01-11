using SphericalPaths.DataStructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathsData", menuName = "ScriptableObjects/PathManagerScriptableObject", order = 1)]
public class PathManagerScriptableObject : ScriptableObject
{

    #region Fields/Properties

    [Header("Start Coordinates")]

    /// <summary>
    /// The label of the start coordinates.
    /// </summary>
    [SerializeField]
    [Tooltip("The label of the start coordinates.")]
    private string _StartLabel;

    /// <summary>
    /// The label of the start coordinates.
    /// </summary>
    public string StartLabel { get { return _StartLabel; } }

    /// <summary>
    /// The first point in the paths.
    /// </summary>
    [SerializeField]
    [Tooltip("The first point in the paths.")]
    private Coordinates _StartCoordinates;

    /// <summary>
    /// The first point in the paths.
    /// </summary>
    public Coordinates StartCoordinates { get { return _StartCoordinates; } }



    [Header("End Coordinates")]

    /// <summary>
    /// The label of the end coordinates.
    /// </summary>
    [SerializeField]
    [Tooltip("The label of the end coordinates.")]
    private string _EndLabel;

    /// <summary>
    /// The label of the end coordinates.
    /// </summary>
    public string EndLabel { get { return _EndLabel; } }

    /// <summary>
    /// The last point in the paths.
    /// </summary>
    [SerializeField]
    [Tooltip("The last point in the paths.")]
    private Coordinates _EndCoordinates;

    /// <summary>
    /// The last point in the paths.
    /// </summary>
    public Coordinates EndCoordinates { get { return _EndCoordinates; } }



    [Header("Paths")]

    /// <summary>
    /// The shortest path generated between the start and end coordinates.
    /// </summary>
    [SerializeField]
    [Tooltip("The shortest path generated between the start and end coordinates.")]
    private Path _ShortestPath;

    /// <summary>
    /// The shortest path generated between the start and end coordinates.
    /// </summary>
    public Path ShortestPath { get { return _ShortestPath; } }

    /// <summary>
    /// The rhumb path generated between the start and end coordinates.
    /// </summary>
    [SerializeField]
    [Tooltip("The rhumb path generated between the start and end coordinates.")]
    private Path _RhumbPath;

    /// <summary>
    /// The rhumb path generated between the start and end coordinates.
    /// </summary>
    public Path RhumbPath { get { return _RhumbPath; } }

    #endregion

    #region Methods

    /// <summary>
    /// Clear all the dataset.
    /// </summary>
    public void Clear()
    {
        // Clear coordinates
        _StartLabel = string.Empty;
        _StartCoordinates = null;
        _EndLabel = string.Empty;
        _EndCoordinates = null;

        // Clear paths
        _ShortestPath = null;
        _RhumbPath = null;
    }

    /// <summary>
    /// Sets the data.
    /// </summary>
    public void Set(string startLabel, Coordinates startCoordinates,
        string endLabel, Coordinates endCoordinates,
        Path shortestPath, Path rhumbPath)
    {
        // Set coordinates
        _StartLabel = startLabel;
        _StartCoordinates = startCoordinates;
        _EndLabel = endLabel;
        _EndCoordinates = endCoordinates;

        // Set paths
        _ShortestPath = shortestPath;
        _RhumbPath = rhumbPath;
    }

    #endregion

}
