using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTraversalManager : MonoBehaviour
{

    #region Constants

    /// <summary>
    /// The distance the camera travels above the sphere.
    /// </summary>
    private const float DISTANCE_ABOVE_SPHERE = 0.2f;

    /// <summary>
    /// The total time it takes to travel a path (in seconds).
    /// </summary>
    private const float TOTAL_TIME = 10f;

    #endregion

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

    /// <summary>
    /// The position of the camera before starting the traversion.
    /// </summary>
    private Vector3? InitialCameraPosition;

    /// <summary>
    /// The rotation of the camera before starting the traversion.
    /// </summary>
    private Vector3? InitialCameraRotation;

    #endregion

    #region Methods

    /// <summary>
    /// Requests the camera to start traversing the provided path.
    /// </summary>
    public void TraversePath(SphericalPaths.DataStructure.Path path)
    {
        // Reset the rotation of the sphere to zeros
        // This is required to make the math easier
        Sphere.transform.eulerAngles = Vector3.zero;

        // Store the camera's position and rotation
        InitialCameraPosition = Camera.transform.position;
        InitialCameraRotation = Camera.transform.eulerAngles;

        // Initialize the sequence
        var sequence = LeanTween.sequence(true);

        // Add the move operations
        int currentIndex = 0;
        float timePerSegment = (path.Coordinates.Count - 2) / TOTAL_TIME;
        for (int i = 0; i < path.Coordinates.Count - 2; i++)
        {
            sequence.append(() =>
            {
                MoveCamera
                (
                    from: path.Coordinates[currentIndex],
                    to: path.Coordinates[currentIndex + 1],
                    distanceAboveSphere: DISTANCE_ABOVE_SPHERE,
                    time: timePerSegment,
                    setInitialRotationAsTargetRotation: currentIndex == 0 // Upon start, make sure to start with a proper angle
                );
                currentIndex++;
            });
            sequence.append(timePerSegment);
        }
    }

    /// <summary>
    /// Moves the camera between two coordinates.
    /// </summary>
    private void MoveCamera(SphericalPaths.DataStructure.Coordinates from,
        SphericalPaths.DataStructure.Coordinates to, 
        float distanceAboveSphere = 0.2f,
        float time = 1f,
        bool setInitialRotationAsTargetRotation = false)
    {
        // Compute initial position
        Vector3 initialPosition = Sphere.transform.position +
            from.SphericalCoordinates + from.SphericalCoordinates.normalized * distanceAboveSphere;

        // Compute target position
        Vector3 targetPosition = Sphere.transform.position +
            to.SphericalCoordinates + to.SphericalCoordinates.normalized * distanceAboveSphere;

        // Compute camera's target rotation
        GameObject temp = new GameObject();
        temp.transform.position = initialPosition;
        temp.transform.LookAt(targetPosition, from.SphericalCoordinates);
        Vector3 targetRotation = temp.transform.eulerAngles;
        GameObject.Destroy(temp);

        // Set camera's initial position
        Camera.transform.position = initialPosition;

        // Set camera's initial rotation
        if (setInitialRotationAsTargetRotation)
            Camera.transform.eulerAngles = targetRotation;

        // Rotate camera
        LeanTween.rotate(Camera.gameObject, targetRotation, time);

        // Move camera
        LeanTween.move(Camera.gameObject, targetPosition, time);
    }

    /// <summary>
    /// Stops the camera from traversing the current path.
    /// </summary>
    public void Stop()
    {
        // Stop all tweens
        LeanTween.cancelAll();

        // Reset camera's position and rotation
        if (InitialCameraPosition.HasValue)
            Camera.transform.position = InitialCameraPosition.Value;
        if (InitialCameraRotation.HasValue)
            Camera.transform.eulerAngles = InitialCameraRotation.Value;
    }

    #endregion

}
