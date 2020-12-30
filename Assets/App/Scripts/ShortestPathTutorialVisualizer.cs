using UnityEngine;

public class ShortestPathTutorialVisualizer : MonoBehaviour
{

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

    }

    #endregion

}
