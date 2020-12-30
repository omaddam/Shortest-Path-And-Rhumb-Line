using System.Collections.Generic;
using UnityEngine;

public class ShortestPathTutorialVisualizer : MonoBehaviour
{

    #region Constants

    /// <summary>
    /// The color of the straight line crossing the sphere to connect the start and end points.
    /// </summary>
    private static readonly Color STRAIGHT_LINE_COLOR = Color.magenta;

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the scriptable object that stores the demo information.
    /// </summary>
    [Tooltip("References the scriptable object that stores the demo information.")]
    [SerializeField]
    private PathManagerScriptableObject PathsScriptableObject;

    /// <summary>
    /// References the sphere in the scene.
    /// </summary>
    public SphericalPaths.Sphere Sphere;

    #endregion

    #region Methods

    /// <summary>
    /// Visualizes the tutorial based on the provided step.
    /// </summary>
    public void Display(int step)
    {
        // Clear all paths
        Sphere.ClearPaths();

        // Display steps
        if (step >= 1)
            DisplayStep1();
        else if (step >= 2)
            DisplayStep2();
        else if (step >= 3)
            DisplayStep3();
        else if (step >= 4)
            DisplayStep4();
    }

    /// <summary>
    /// Displays the first step in the tutorial.
    /// </summary>
    private void DisplayStep1()
    {
        // Display straight line crossing the sphere between the start and end points
        Sphere.DisplayPaths
        (
            new SphericalPaths.DataStructure.Path
            (
                new List<SphericalPaths.DataStructure.Coordinates>
                {
                    PathsScriptableObject.StartCoordinates,
                    PathsScriptableObject.EndCoordinates
                }
            ),
            STRAIGHT_LINE_COLOR
        );
    }

    /// <summary>
    /// Displays the second step in the tutorial.
    /// </summary>
    private void DisplayStep2()
    {

    }

    /// <summary>
    /// Displays the thjird step in the tutorial.
    /// </summary>
    private void DisplayStep3()
    {

    }

    /// <summary>
    /// Displays the fourth step in the tutorial.
    /// </summary>
    private void DisplayStep4()
    {

    }

    #endregion

}
