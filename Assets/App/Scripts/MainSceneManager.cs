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
        DisplaySphere();
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
    /// Switches between sphere and plane view.
    /// </summary>
    public void SwitchView()
    {
        if (Sphere.gameObject.activeSelf)
            DisplayPlane();
        else
            DisplaySphere();
    }

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
