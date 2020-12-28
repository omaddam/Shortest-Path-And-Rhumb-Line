using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionSceneManager : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Execute once on awake.
    /// </summary>
    private void Awake()
    {
        // Display version
        VersionText.text = string.Format("Version: {0}", Application.version);
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the text UI element that displays the verison of the application.
    /// </summary>
    [Tooltip("References the text UI element that displays the version of the application.")]
    [SerializeField]
    private Text VersionText;



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

}
