using SphericalPaths.DataStructure;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroductionSceneManager : MonoBehaviour
{

    #region Constants

    /// <summary>
    /// The radius of the sphere displaying the points and paths.
    /// </summary>
    private const float SPHERE_RADIUS = 5f;

    /// <summary>
    /// The width of the plane displaying the points and paths.
    /// </summary>
    private const float PLANE_WIDTH = 15f;

    /// <summary>
    /// Predefined list of coordinates.
    /// </summary>
    private static readonly Dictionary<string, Vector2> PREDEFINED_SAMPLE_COORDINATES 
        = new Dictionary<string, Vector2>()
    {
        {  "North Pole", new Vector2(0, 90) },
        {  "Calgary, AB, Canada", new Vector2(-114.0719f, 51.0447f) },
        {  "Beirut, Lebanon", new Vector2(35.5018f, 33.8938f) },
        {  "London, UK", new Vector2(-0.104788f, 51.485530f) },
        {  "Mecca, KSA", new Vector2(39.826210f, 21.422486f) }
    };

    #endregion

    #region Initialization

    /// <summary>
    /// Execute once on awake.
    /// </summary>
    private void Awake()
    {
        // Display version
        VersionText.text = string.Format("Version: {0}", Application.version);

        // Populate samples dropdowns
        PopulateCoordinatesSampleDropdown();
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the text UI element that displays the verison of the application.
    /// </summary>
    [Tooltip("References the text UI element that displays the version of the application.")]
    [SerializeField]
    private Text VersionText;

    /// <summary>
    /// References the scriptable object that stores the demo information.
    /// </summary>
    [Tooltip("References the scriptable object that stores the demo information.")]
    [SerializeField]
    private PathManagerScriptableObject PathsScriptableObject;

    

    [Header("Start Coordinates")]

    /// <summary>
    /// References the input field UI element that contains the latitude.
    /// </summary>
    [Tooltip("References the input field UI element that contains the latitude.")]
    [SerializeField]
    private InputField StartLatitudeInputField;

    /// <summary>
    /// References the input field UI element that contains the longitude.
    /// </summary>
    [Tooltip("References the input field UI element that contains the longitude.")]
    [SerializeField]
    private InputField StartLongitudeInputField;

    /// <summary>
    /// References the dropdown UI element that contains the samples.
    /// </summary>
    [Tooltip("References the dropdown UI element that contains the samples.")]
    [SerializeField]
    private Dropdown StartSamplesDropdown;



    [Header("End Coordinates")]

    /// <summary>
    /// References the input field UI element that contains the latitude.
    /// </summary>
    [Tooltip("References the input field UI element that contains the latitude.")]
    [SerializeField]
    private InputField EndLatitudeInputField;

    /// <summary>
    /// References the input field UI element that contains the longitude.
    /// </summary>
    [Tooltip("References the input field UI element that contains the longitude.")]
    [SerializeField]
    private InputField EndLongitudeInputField;

    /// <summary>
    /// References the dropdown UI element that contains the samples.
    /// </summary>
    [Tooltip("References the dropdown UI element that contains the samples.")]
    [SerializeField]
    private Dropdown EndSamplesDropdown;

    #endregion

    #region Methods

    /// <summary>
    /// Populates the dropdowns with sample coordinates.
    /// </summary>
    private void PopulateCoordinatesSampleDropdown()
    {
        // Clear everything in dropdowns
        StartSamplesDropdown.ClearOptions();
        EndSamplesDropdown.ClearOptions();

        // Set custom coordinates
        StartLatitudeInputField.text = PathsScriptableObject.StartCoordinates.CartesianCoordinates.y.ToString();
        StartLongitudeInputField.text = PathsScriptableObject.StartCoordinates.CartesianCoordinates.x.ToString();
        EndLatitudeInputField.text = PathsScriptableObject.EndCoordinates.CartesianCoordinates.y.ToString();
        EndLongitudeInputField.text = PathsScriptableObject.EndCoordinates.CartesianCoordinates.x.ToString();

        // Display samples in start dropdown
        StartSamplesDropdown.AddOptions(new List<string> { "Custom" });
        StartSamplesDropdown.AddOptions(PREDEFINED_SAMPLE_COORDINATES.Keys.ToList());
        StartSamplesDropdown.onValueChanged.RemoveAllListeners();
        StartSamplesDropdown.onValueChanged.AddListener((index) =>
        {
            // Custom
            if (index == 0)
            {
                StartLatitudeInputField.interactable = true;
                StartLongitudeInputField.interactable = true;
            }

            // Predefined sample
            else
            {
                string selection = StartSamplesDropdown.options[index].text;
                Vector2 coordinates = PREDEFINED_SAMPLE_COORDINATES[selection];

                StartLatitudeInputField.text = coordinates.y.ToString();
                StartLongitudeInputField.text = coordinates.x.ToString();

                StartLatitudeInputField.interactable = false;
                StartLongitudeInputField.interactable = false;
            }
        });
        StartSamplesDropdown.value = PREDEFINED_SAMPLE_COORDINATES.Keys.ToList()
            .IndexOf(PathsScriptableObject.StartLabel) + 1;

        // Display samples in end dropdown
        EndSamplesDropdown.AddOptions(new List<string> { "Custom" });
        EndSamplesDropdown.AddOptions(PREDEFINED_SAMPLE_COORDINATES.Keys.ToList());
        EndSamplesDropdown.onValueChanged.AddListener((index) =>
        {
            // Custom
            if (index == 0)
            {
                EndLatitudeInputField.interactable = true;
                EndLongitudeInputField.interactable = true;
            }

            // Predefined sample
            else
            {
                string selection = EndSamplesDropdown.options[index].text;
                Vector2 coordinates = PREDEFINED_SAMPLE_COORDINATES[selection];

                EndLatitudeInputField.text = coordinates.y.ToString();
                EndLongitudeInputField.text = coordinates.x.ToString();

                EndLatitudeInputField.interactable = false;
                EndLongitudeInputField.interactable = false;
            }
        });
        EndSamplesDropdown.value = PREDEFINED_SAMPLE_COORDINATES.Keys.ToList()
            .IndexOf(PathsScriptableObject.EndLabel) + 1;
    }

    /// <summary>
    /// Computes the paths and loads the main scene to visualize them.
    /// </summary>
    public void GeneratePaths()
    {
        // Get start coordinates
        string startLabel = StartSamplesDropdown.options[StartSamplesDropdown.value].text;
        float.TryParse(StartLongitudeInputField.text, out float startLongitude);
        float.TryParse(StartLatitudeInputField.text, out float startLatitude);
        Coordinates startCoordinates = new Coordinates(new Vector2(startLongitude, startLatitude), 
            SPHERE_RADIUS, PLANE_WIDTH);

        // Get end coordinates
        string endLabel = EndSamplesDropdown.options[EndSamplesDropdown.value].text;
        float.TryParse(EndLongitudeInputField.text, out float endLongitude);
        float.TryParse(EndLatitudeInputField.text, out float endLatitude);
        Coordinates endCoordinates = new Coordinates(new Vector2(endLongitude, endLatitude),
            SPHERE_RADIUS, PLANE_WIDTH);

        // Compute paths
        Path shortestPath = startCoordinates.GetShortestPath(endCoordinates);
        Path rhumbPath = startCoordinates.GetRhumbPath(endCoordinates);

        // Store data
        PathsScriptableObject.Set(startLabel, startCoordinates,
            endLabel, endCoordinates,
            shortestPath, rhumbPath);

        // Switch to main scene
        SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
    }

    #endregion

}
