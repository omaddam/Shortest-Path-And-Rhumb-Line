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
    [Tooltip("References the text UI element that displays the verison of the application.")]
    [SerializeField]
    private Text VersionText;

    #endregion

}
