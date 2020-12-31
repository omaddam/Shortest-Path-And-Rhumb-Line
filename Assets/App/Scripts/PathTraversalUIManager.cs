using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathTraversalUIManager : MonoBehaviour
{

    #region Fields/Properties

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

    #endregion

}
