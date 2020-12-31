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
        // Inform listeners
        OnCancel.Invoke();
    }

    #endregion

}
