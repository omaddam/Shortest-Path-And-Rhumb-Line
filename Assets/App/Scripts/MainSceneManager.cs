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
    private static readonly Color END_POINT_COLOR = Color.black;

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
        // Update legends colors
        StartPointImage.color = START_POINT_COLOR;
        EndPointImage.color = END_POINT_COLOR;
        ShortestPathImage.color = SHORTEST_PATH_COLOR;
        RhumbLineImage.color = RHUMB_PATH_COLOR;

        // Set the sphere rotation speed
        Sphere.GetComponent<SphericalPaths.SphereRotation>().RotationSpeed = SPHERE_ROTATION_SPEED;

        // Enable (to invoke the awake method) then disable them immediately
        Sphere.gameObject.SetActive(true);
        Plane.gameObject.SetActive(true);
        Sphere.gameObject.SetActive(false);
        Plane.gameObject.SetActive(false);

        // Display path information
        PathText.text = string.Format("{0} [Lat: {1:0.000}, Lon: {2:0.000}] to {3} [Lat: {4:0.000}, Lon: {5:0.000}]",
            PathsScriptableObject.StartLabel,
            PathsScriptableObject.StartCoordinates.CartesianCoordinates.y,
            PathsScriptableObject.StartCoordinates.CartesianCoordinates.x,
            PathsScriptableObject.EndLabel,
            PathsScriptableObject.EndCoordinates.CartesianCoordinates.y,
            PathsScriptableObject.EndCoordinates.CartesianCoordinates.x);

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

    /// <summary>
    /// References the text that displays the path start/end point information.
    /// </summary>
    [Tooltip("References the text that displays the path start/end point information.")]
    [SerializeField]
    private Text PathText;



    [Header("Legends")]

    /// <summary>
    /// References the image UI that displays the color of the start point pin.
    /// </summary>
    [Tooltip("References the image UI that displays the color of the start point pin.")]
    [SerializeField]
    private Image StartPointImage;

    /// <summary>
    /// References the image UI that displays the color of the end point pin.
    /// </summary>
    [Tooltip("References the image UI that displays the color of the end point pin.")]
    [SerializeField]
    private Image EndPointImage;

    /// <summary>
    /// References the image UI that displays the color of the shortest path.
    /// </summary>
    [Tooltip("References the image UI that displays the color of the shortest path.")]
    [SerializeField]
    private Image ShortestPathImage;

    /// <summary>
    /// References the image UI that displays the color of the rhumb path.
    /// </summary>
    [Tooltip("References the image UI that displays the color of the rhumb path.")]
    [SerializeField]
    private Image RhumbLineImage;



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



    [Header("Shortest Path Tutorial")]

    /// <summary>
    /// References the planel that holds the shortest path tutorial.
    /// </summary>
    [Tooltip("References the planel that holds the shortest path tutorial.")]
    [SerializeField]
    public GameObject ShortestPathTutorialParentPanel;

    /// <summary>
    /// References the planel that holds the teaser for shortest path tutorial.
    /// </summary>
    [Tooltip("References the planel that holds the teaser for shortest path tutorial.")]
    [SerializeField]
    public GameObject ShortestPathTutorialTeaserPanel;

    /// <summary>
    /// References the planel that holds the actual tutorial.
    /// </summary>
    [Tooltip("References the planel that holds the actual tutorial.")]
    [SerializeField]
    public GameObject ShortestPathTutorialPanel;

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

        // Show plane view
        if (!spherView.Value)
        {
            // Hide the sphere, show the plane, and display the two paths
            DisplayPlane();

            // Hide the shortest path tutorial
            ShortestPathTutorialParentPanel.SetActive(false);
        }

        // Show sphere view
        else
        {
            // Hide the plane, show the sphere, and display the two paths
            DisplaySphere();

            // Display the shortest path tutorial teaser
            DisplayShortestPathTutorial();
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
        Sphere.GetComponent<SphericalPaths.SphereRotation>().Focus(
            PathsScriptableObject.StartCoordinates.CartesianCoordinates.x,
            PathsScriptableObject.StartCoordinates.CartesianCoordinates.y);

        // Change switch button text
        SwitchButton.GetComponentInChildren<Text>().text = "Switch to plane view";
    }

    /// <summary>
    /// Shows the menu that allows the user to start the shortest path tutorial.
    /// </summary>
    private void DisplayShortestPathTutorial()
    {
        // Display the parent panel
        ShortestPathTutorialParentPanel.SetActive(true);

        // Hide the tutorial
        ShortestPathTutorialPanel.SetActive(false);

        // Show the teaser
        ShortestPathTutorialTeaserPanel.SetActive(true);
    }

    /// <summary>
    /// Starts the shortest path tutorial.
    /// </summary>
    public void StartShortestPathTutorial()
    {
        // Hide the teaser
        ShortestPathTutorialTeaserPanel.SetActive(false);

        // Show the tutorial
        ShortestPathTutorialPanel.SetActive(true);

        // Change the opacity of the sphere
        Sphere.Opacity = SPHERE_TUTORIAL_OPACITY;

        // Move the sphere
        Sphere.transform.position = new Vector3(SPHERE_TUTORIAL_X_OFFSET, Sphere.transform.position.y, 0);

        // Start with the first step
        ShowStep1InShortestPathTutorial();
    }

    /// <summary>
    /// Stops the shortest path tutorial.
    /// </summary>
    public void CancelShortestPathTutorial()
    {
        SwitchView(true);
    }

    /// <summary>
    /// Displays the first step in the shoretst path tutorial.
    /// </summary>
    public void ShowStep1InShortestPathTutorial()
    {

    }

    /// <summary>
    /// Displays the second step in the shoretst path tutorial.
    /// </summary>
    public void ShowStep2InShortestPathTutorial()
    {

    }

    /// <summary>
    /// Displays the third step in the shoretst path tutorial.
    /// </summary>
    public void ShowStep3InShortestPathTutorial()
    {

    }

    /// <summary>
    /// Displays the fourth step in the shoretst path tutorial.
    /// </summary>
    public void ShowStep4InShortestPathTutorial()
    {

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
