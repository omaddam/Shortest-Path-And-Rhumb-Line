using SphericalPaths.DataStructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PathTraversalUIManager : MonoBehaviour
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

        StartShortestPathButton.onClick.AddListener(() => StartTraversing(true));
        StartRhumbPathButton.onClick.AddListener(() => StartTraversing(false));
        CancelButton.onClick.AddListener(Cancel);
    }

    #endregion

    #region Events

    /// <summary>
    /// Triggers when the user wants to traverse the shortest path.
    /// </summary>
    public UnityEvent OnShortestPathStart;

    /// <summary>
    /// Triggers when the user wants to traverse the rhumb path.
    /// </summary>
    public UnityEvent OnRhumbPathStart;

    /// <summary>
    /// Triggers when the user wants to cancel.
    /// </summary>
    public UnityEvent OnCancel;

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the camera that will traverse the path.
    /// </summary>
    [Tooltip("References the camera that will traverse the path. By default, it references the main camera.")]
    [SerializeField]
    private Camera Camera;

    /// <summary>
    /// References the sphere in the scene.
    /// </summary>
    public SphericalPaths.Sphere Sphere;

    /// <summary>
    /// References the scriptable object that stores the demo information.
    /// </summary>
    [Tooltip("References the scriptable object that stores the demo information.")]
    [SerializeField]
    private PathManagerScriptableObject PathsScriptableObject;

    /// <summary>
    /// References the planel that holds the teaser.
    /// </summary>
    [Tooltip("References the planel that holds the teaser.")]
    [SerializeField]
    private GameObject TeaserPanel;

    /// <summary>
    /// References the planel that holds the compass.
    /// </summary>
    [Tooltip("References the planel that holds the compass.")]
    [SerializeField]
    private GameObject CompassPanel;

    /// <summary>
    /// References the start button.
    /// </summary>
    [Tooltip("References the shortest path start button.")]
    [SerializeField]
    private Button StartShortestPathButton;

    /// <summary>
    /// References the start button.
    /// </summary>
    [Tooltip("References the rhumb path start button.")]
    [SerializeField]
    private Button StartRhumbPathButton;

    /// <summary>
    /// References the cancel button.
    /// </summary>
    [Tooltip("References the cancel button.")]
    [SerializeField]
    private Button CancelButton;

    /// <summary>
    /// References the arrow in the compass.
    /// </summary>
    [Tooltip("References the arrow in the compass.")]
    [SerializeField]
    private GameObject CompassDirection;

    /// <summary>
    /// References the UI element that displays the bearing angle.
    /// </summary>
    [Tooltip("References the UI element that displays the bearing angle.")]
    [SerializeField]
    private Text BearingText;

    /// <summary>
    /// States if we are currently traversing shortest path or not.
    /// Null: no traversing. True: shortest path traversing. False: rhumb path traversing.
    /// </summary>
    private bool? TraversingShortestPath;

    #endregion

    #region Methods

    /// <summary>
    /// Shows the teaser panel.
    /// </summary>
    public void Show()
    {
        // Show teaser panel
        TeaserPanel.SetActive(true);

        // Hide compass panel
        CompassPanel.SetActive(false);
    }

    /// <summary>
    /// Hides all the panels.
    /// </summary>
    public void Hide()
    {
        // Hide teaser panel
        TeaserPanel.SetActive(false);

        // Hide compass panel
        CompassPanel.SetActive(false);
    }

    /// <summary>
    /// Starts traversing.
    /// </summary>
    private void StartTraversing(bool shortestPath)
    {
        TraversingShortestPath = shortestPath;

        // Hide teaser panel
        TeaserPanel.SetActive(false);

        // Show compass panel
        CompassPanel.SetActive(true);

        // Inform listeners
        if (shortestPath)
            OnShortestPathStart.Invoke();
        else
            OnRhumbPathStart.Invoke();
    }

    /// <summary>
    /// Cancels the traversing.
    /// </summary>
    private void Cancel()
    {
        TraversingShortestPath = null;

        // Inform listeners
        OnCancel.Invoke();
    }

    /// <summary>
    /// Continuously compute the bearing angle.
    /// </summary>
    private void Update()
    {
        if (!TraversingShortestPath.HasValue)
            return;

        // Get the projection of the camera onto the sphere
        Vector3 cameraProjection = (Camera.transform.position - Sphere.transform.position).normalized * PathsScriptableObject.StartCoordinates.Radius;

        // Get current coordinates
        Coordinates cameraCoordinates = new Coordinates(cameraProjection,
            PathsScriptableObject.StartCoordinates.Radius, PathsScriptableObject.StartCoordinates.Width);

        // Compute bearing angle
        float bearing = PathComputationMethods.ComputeRhumbPathBearingAngle(
            cameraCoordinates, PathsScriptableObject.EndCoordinates);

        // Update arrow rotation
        CompassDirection.transform.localEulerAngles = new Vector3(0, 0, -1) * bearing;

        // Display the angle
        BearingText.text = string.Format("{0:0.000}°", bearing);
    }

    #endregion

}
