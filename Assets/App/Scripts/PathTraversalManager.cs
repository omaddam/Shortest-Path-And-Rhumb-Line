using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTraversalManager : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Executes once on start.
    /// </summary>
    private void Awake()
    {
        // Seta a default camera
        if (Camera == null)
            Camera = Camera.main;
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the camera that will traverse the path.
    /// </summary>
    [Tooltip("References the camera that will traverse the path. By default, it references the main camera.")]
    [SerializeField]
    private Camera Camera;

    /// <summary>
    /// References the sphere that the camera will rotate around.
    /// </summary>
    [Tooltip("References the sphere that the camera will rotate around.")]
    [SerializeField]
    private SphericalPaths.Sphere Sphere;

    #endregion

}
