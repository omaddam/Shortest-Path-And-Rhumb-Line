using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShortestPathTutorialUIManager : MonoBehaviour
{

    #region Constants

    /// <summary>
    /// The color applied to the button of current step.
    /// </summary>
    private static readonly Color BUTTON_CURRENT_COLOR = Color.yellow;

    /// <summary>
    /// The color applied to the buttons of completed steps.
    /// </summary>
    private static readonly Color BUTTON_COMPLETED_COLOR = Color.green;

    /// <summary>
    /// The color applied to the buttons of pending steps.
    /// </summary>
    private static readonly Color BUTTON_PENDING_COLOR = Color.white;

    #endregion

    #region Initialization

    /// <summary>
    /// Executes once on start.
    /// </summary>
    private void Awake()
    {
        StartButton.onClick.AddListener(StartTutorial);
        CancelButton.onClick.AddListener(Cancel);

        Step1Button.onClick.AddListener(() => SetStep(1));
        Step2Button.onClick.AddListener(() => SetStep(2));
        Step3Button.onClick.AddListener(() => SetStep(3));
        Step4Button.onClick.AddListener(() => SetStep(4));
    }

    #endregion

    #region Events

    /// <summary>
    /// Triggers when the user wants to start the tutorial.
    /// </summary>
    public UnityEvent OnStart;

    /// <summary>
    /// Triggers when the user clicks or sets a step.
    /// </summary>
    public UnityEvent OnChange;

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
    /// References the planel that holds the tutorial.
    /// </summary>
    [Tooltip("References the planel that holds the tutorial.")]
    [SerializeField]
    private GameObject TutorialPanel;

    /// <summary>
    /// References the start button.
    /// </summary>
    [Tooltip("References the start button.")]
    [SerializeField]
    private Button StartButton;

    /// <summary>
    /// References the cancel button.
    /// </summary>
    [Tooltip("References the cancel button.")]
    [SerializeField]
    private Button CancelButton;

    /// <summary>
    /// References the button in the first step of the tutorial.
    /// </summary>
    [Tooltip("References the button in the first step of the tutorial.")]
    [SerializeField]
    private Button Step1Button;

    /// <summary>
    /// References the button in the second step of the tutorial.
    /// </summary>
    [Tooltip("References the button in the second step of the tutorial.")]
    [SerializeField]
    private Button Step2Button;

    /// <summary>
    /// References the button in the third step of the tutorial.
    /// </summary>
    [Tooltip("References the button in the third step of the tutorial.")]
    [SerializeField]
    private Button Step3Button;

    /// <summary>
    /// References the button in the fourth step of the tutorial.
    /// </summary>
    [Tooltip("References the button in the fourth step of the tutorial.")]
    [SerializeField]
    private Button Step4Button;

    /// <summary>
    /// The current step of the tutorial.
    /// </summary>
    public int CurrentStep { private set; get; }

    #endregion

    #region Methods

    /// <summary>
    /// Shows the teaser panel.
    /// </summary>
    public void Show()
    {
        // Show teaser panel
        TeaserPanel.SetActive(true);

        // Hide tutorial panel
        TutorialPanel.SetActive(false);
    }

    /// <summary>
    /// Hides all the panels.
    /// </summary>
    public void Hide()
    {
        // Hide teaser panel
        TeaserPanel.SetActive(false);

        // Hide tutorial panel
        TutorialPanel.SetActive(false);
    }

    /// <summary>
    /// Starts the tutorial.
    /// </summary>
    private void StartTutorial()
    {
        // Hide teaser panel
        TeaserPanel.SetActive(false);

        // Show tutorial poanel
        TutorialPanel.SetActive(true);

        // Inform listeners
        OnStart.Invoke();

        // Set step
        SetStep(1);
    }

    /// <summary>
    /// Sets the tutorial's step.
    /// </summary>
    private void SetStep(int step)
    {
        // Store locally
        CurrentStep = step;

        // Change the buttons colors
        Step1Button.GetComponent<Image>().color = step == 1 ? BUTTON_CURRENT_COLOR
            : (step < 1 ? BUTTON_PENDING_COLOR : BUTTON_COMPLETED_COLOR);
        Step2Button.GetComponent<Image>().color = step == 2 ? BUTTON_CURRENT_COLOR
            : (step < 2 ? BUTTON_PENDING_COLOR : BUTTON_COMPLETED_COLOR);
        Step3Button.GetComponent<Image>().color = step == 3 ? BUTTON_CURRENT_COLOR
            : (step < 3 ? BUTTON_PENDING_COLOR : BUTTON_COMPLETED_COLOR);
        Step4Button.GetComponent<Image>().color = step == 4 ? BUTTON_CURRENT_COLOR
            : (step < 4 ? BUTTON_PENDING_COLOR : BUTTON_COMPLETED_COLOR);

        // Inform listeners that the step has changed
        OnChange.Invoke();
    }

    /// <summary>
    /// Cancels the tutorial.
    /// </summary>
    private void Cancel()
    {
        // Inform listeners
        OnCancel.Invoke();
    }

    #endregion

}
