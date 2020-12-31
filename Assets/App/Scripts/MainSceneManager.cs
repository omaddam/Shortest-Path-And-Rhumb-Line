using UnityEngine;
using SphericalPaths.DataStructure;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{

    #region Constants

    /// <summary>
    /// The color used to display the start point.
    /// </summary>
    private static readonly Color START_POINT_COLOR = Color.blue;

    /// <summary>
    /// The color used to display the end point.
    /// </summary>
    private static readonly Color END_POINT_COLOR = new Color(1f, 0.4661931f, 0, 1f);

    /// <summary>
    /// The color used to display the shortest path.
    /// </summary>
    private static readonly Color SHORTEST_PATH_COLOR = Color.red;

    /// <summary>
    /// The color used to display the rhumb path.
    /// </summary>
    private static readonly Color RHUMB_PATH_COLOR = Color.green;

    /// <summary>
    /// The light intensity applied to the sphere.
    /// </summary>
    private const float SPHERE_LIGHT_INTENSITY = 0.5f;

    /// <summary>
    /// The light intensity applied to the plane.
    /// </summary>
    private const float PLANE_LIGHT_INTENSITY = 0.7f;

#if !UNITY_EDITOR && UNITY_WEBGL

    /// <summary>
    /// The speed of sphere rotation upon dragging.
    /// </summary>
    private const float SPHERE_ROTATION_SPEED = 3f;

#else

    /// <summary>
    /// The speed of sphere rotation upon dragging.
    /// </summary>
    private const float SPHERE_ROTATION_SPEED = 5f;

#endif

    /// <summary>
    /// The opacity of the sphere when running a tutorial.
    /// </summary>
    private const float SPHERE_TUTORIAL_OPACITY = 0.3f;

    /// <summary>
    /// The x position of the sphere when in tutorial mode.
    /// </summary>
    private const float SPHERE_TUTORIAL_X_OFFSET = -1f;

    #endregion

    #region Initialization

    /// <summary>
    /// Executes once on awake.
    /// </summary>
    private void Awake()
    {
        // Update legend colors
        LegendsUI.Initialize(START_POINT_COLOR, END_POINT_COLOR,
            SHORTEST_PATH_COLOR, RHUMB_PATH_COLOR);

        // Set the sphere rotation speed
        Sphere.GetComponent<SphericalPaths.SphereRotation>().RotationSpeed = SPHERE_ROTATION_SPEED;

        // Enable (to invoke the awake method) then disable them immediately
        Sphere.gameObject.SetActive(true);
        Plane.gameObject.SetActive(true);
        Sphere.gameObject.SetActive(false);
        Plane.gameObject.SetActive(false);

        // Initialize the shortest path tutorial
        InitializeShortestPathTutorial();

        // Display the sphere
        SwitchView(true);
    }

    #endregion

    #region Fields/Properties

    [Header("Paths")]

    /// <summary>
    /// References the scriptable object that stores the demo information.
    /// </summary>
    [Tooltip("References the scriptable object that stores the demo information.")]
    [SerializeField]
    private PathManagerScriptableObject PathsScriptableObject;



    [Header("Views")]

    /// <summary>
    /// References the button that swithces between the sphere and plane view.
    /// </summary>
    [Tooltip("References the button that swithces between the sphere and plane view.")]
    [SerializeField]
    private Button SwitchButton;

    /// <summary>
    /// References the sphere in the scene.
    /// </summary>
    public SphericalPaths.Sphere Sphere;

    /// <summary>
    /// References the plane in the scene.
    /// </summary>
    public SphericalPaths.Plane Plane;



    [Header("Tutorials")]

    /// <summary>
    /// References ths visualizer that will display the shortest path tutorial.
    /// </summary>
    [Tooltip("References ths visualizer that will display the shortest path tutorial.")]
    [SerializeField]
    private ShortestPathTutorialVisualizer ShortestPathTutorialVisualizer;

    /// <summary>
    /// References the manager that allows the camera to traverse a path.
    /// </summary>
    [Tooltip("References the manager that allows the camera to traverse a path.")]
    [SerializeField]
    private PathTraversalManager PathTraversal;



    [Header("UI Managers")]

    /// <summary>
    /// References the UI manager that displays the colors used in a legend.
    /// </summary>
    [Tooltip("References the UI manager that displays the colors used in a legend.")]
    [SerializeField]
    private LegendsUIManager LegendsUI;

    /// <summary>
    /// References the UI manager that displays the shortest path's tutorial.
    /// </summary>
    [Tooltip("References the UI manager that displays the shortest path's tutorial.")]
    [SerializeField]
    private ShortestPathTutorialUIManager ShortestPathTutorialUI;

    #endregion

    #region Methods

    /// <summary>
    /// Switches back to the introduction scene.
    /// </summary>
    public void SwitchBackToIntroduction()
    {
        SceneManager.LoadScene("IntroductionScene", LoadSceneMode.Single);
    }

    /// <summary>
    /// Toggles between sphere and plane view.
    /// </summary>
    public void SwitchView()
    {
        SwitchView(null);
    }

    /// <summary>
    /// Switches between sphere and plane view.
    /// </summary>
    private void SwitchView(bool? spherView = null)
    {
        // Check if sphere view or plane view
        spherView ??= !Sphere.gameObject.activeSelf;

        // Stop path traversion
        PathTraversal.Stop();

        // Show plane view
        if (!spherView.Value)
        {
            // Disable sphere rotation
            Sphere.GetComponent<SphericalPaths.SphereRotation>().enabled = false;

            // Hide the sphere, show the plane, and display the two paths
            DisplayPlane();

            // Hide the shortest path tutorial
            ShortestPathTutorialUI.Hide();
        }

        // Show sphere view
        else
        {
            // Enable sphere rotation
            Sphere.GetComponent<SphericalPaths.SphereRotation>().enabled = true;

            // Hide the plane, show the sphere, and display the two paths
            DisplaySphere();

            // Display the shortest path tutorial
            ShortestPathTutorialUI.Show();
        }
    }

    #endregion

    #region Sphere Methods

    /// <summary>
    /// Displays the points and paths on the sphere.
    /// </summary>
    private void DisplaySphere()
    {
        // Hide plane
        Plane.gameObject.SetActive(false);

        // Show sphere
        Sphere.gameObject.SetActive(true);
        Sphere.transform.eulerAngles = Vector3.zero;

        // Clear everything displayed on the sphere
        Sphere.ClearPoints();
        Sphere.ClearPaths();

        // Set opacity
        Sphere.Opacity = 1;

        // Set position
        Sphere.transform.position = new Vector3(0, Sphere.transform.position.y, 0);

        // Set light intensity
        Sphere.Intensity = SPHERE_LIGHT_INTENSITY;

        // Display points
        Sphere.DisplayPoints(PathsScriptableObject.StartCoordinates, START_POINT_COLOR);
        Sphere.DisplayPoints(PathsScriptableObject.EndCoordinates, END_POINT_COLOR);

        // Display paths
        Sphere.DisplayPaths(PathsScriptableObject.ShortestPath, SHORTEST_PATH_COLOR);
        Sphere.DisplayPaths(PathsScriptableObject.RhumbPath, RHUMB_PATH_COLOR);

        // Focus on the start coordinates
        Sphere.GetComponent<SphericalPaths.SphereRotation>().enabled = true;
        Sphere.GetComponent<SphericalPaths.SphereRotation>().Focus(
            (PathsScriptableObject.StartCoordinates.CartesianCoordinates.x + PathsScriptableObject.EndCoordinates.CartesianCoordinates.y) / 2f,
            (PathsScriptableObject.StartCoordinates.CartesianCoordinates.y + PathsScriptableObject.EndCoordinates.CartesianCoordinates.y) / 2f);

        // Change switch button text
        SwitchButton.GetComponentInChildren<Text>().text = "Switch to plane view";
    }

    /// <summary>
    /// Initializes the shortest path tutorial events.
    /// </summary>
    private void InitializeShortestPathTutorial()
    {
        // Handle tutorial starting
        ShortestPathTutorialUI.OnStart.AddListener(() => 
        {
            // Change the opacity of the sphere
            Sphere.Opacity = SPHERE_TUTORIAL_OPACITY;

            // Move the sphere
            Sphere.transform.position = new Vector3(SPHERE_TUTORIAL_X_OFFSET, Sphere.transform.position.y, 0);
        });

        // Handle tutorial ending
        ShortestPathTutorialUI.OnCancel.AddListener(() =>
        {
            SwitchView(true);
        });

        // Handle tutorial step change
        ShortestPathTutorialUI.OnChange.AddListener(() =>
        {
            ShortestPathTutorialVisualizer.Display(ShortestPathTutorialUI.CurrentStep);
        });
    }

    #endregion

    #region Plane Methods

    /// <summary>
    /// Displays the points and paths on the plane.
    /// </summary>
    private void DisplayPlane()
    {
        // Hide sphere
        Sphere.gameObject.SetActive(false);

        // Show plane
        Plane.gameObject.SetActive(true);

        // Clear everything displayed on the plane
        Plane.ClearPoints();
        Plane.ClearPaths();

        // Set light intensity
        Plane.Intensity = PLANE_LIGHT_INTENSITY;

        // Display points
        Plane.DisplayPoints(PathsScriptableObject.StartCoordinates, START_POINT_COLOR);
        Plane.DisplayPoints(PathsScriptableObject.EndCoordinates, END_POINT_COLOR);

        // Display paths
        Plane.DisplayPaths(PathsScriptableObject.ShortestPath, SHORTEST_PATH_COLOR);
        Plane.DisplayPaths(PathsScriptableObject.RhumbPath, RHUMB_PATH_COLOR);

        // Change switch button text
        SwitchButton.GetComponentInChildren<Text>().text = "Switch to sphere view";
    }

    #endregion

}
