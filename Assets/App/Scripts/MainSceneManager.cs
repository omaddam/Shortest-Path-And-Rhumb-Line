using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Executes once on awake.
    /// </summary>
    private void Awake()
    {
        SphericalPaths.DataStructure.Coordinates coordinates = new SphericalPaths.DataStructure.Coordinates(new Vector2(0, 90), Sphere.Radius, 1);
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the sphere in the scene.
    /// </summary>
    public SphericalPaths.Sphere Sphere;

    #endregion

}
