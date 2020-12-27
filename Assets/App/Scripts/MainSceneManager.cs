using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Executes once on awake.
    /// </summary>
    private void Start()
    {
        SphericalPaths.DataStructure.Coordinates northPole = new SphericalPaths.DataStructure.Coordinates(new Vector2(0, 90), Sphere.Radius, 1);
        SphericalPaths.DataStructure.Coordinates calgary = new SphericalPaths.DataStructure.Coordinates(new Vector2(-114.0719f, 51.0447f), Sphere.Radius, 1);
        SphericalPaths.DataStructure.Coordinates lebanon = new SphericalPaths.DataStructure.Coordinates(new Vector2(35.539267f, 33.893940f), Sphere.Radius, 1);
        SphericalPaths.DataStructure.Coordinates london = new SphericalPaths.DataStructure.Coordinates(new Vector2(-0.104788f, 51.485530f), Sphere.Radius, 1);
        SphericalPaths.DataStructure.Coordinates mecca = new SphericalPaths.DataStructure.Coordinates(new Vector2(39.826210f, 21.422486f), Sphere.Radius, 1);

        Sphere.DisplayPoints(northPole, Color.green);
        Sphere.DisplayPoints(calgary, Color.red);
        Sphere.DisplayPoints(lebanon, Color.blue);
        Sphere.DisplayPoints(london, Color.gray);
        Sphere.DisplayPoints(mecca, Color.white);
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the sphere in the scene.
    /// </summary>
    public SphericalPaths.Sphere Sphere;

    #endregion

}
