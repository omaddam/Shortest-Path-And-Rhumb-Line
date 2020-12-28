using UnityEngine;
using SphericalPaths.DataStructure;

public class MainSceneManager : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Executes once on awake.
    /// </summary>
    private void Start()
    {
        Coordinates northPole = new Coordinates(new Vector2(0, 90), Sphere.Radius, 1);
        Coordinates calgary = new Coordinates(new Vector2(-114.0719f, 51.0447f), Sphere.Radius, 1);
        Coordinates lebanon = new Coordinates(new Vector2(35.539267f, 33.893940f), Sphere.Radius, 1);
        Coordinates london = new Coordinates(new Vector2(-0.104788f, 51.485530f), Sphere.Radius, 1);
        Coordinates mecca = new Coordinates(new Vector2(39.826210f, 21.422486f), Sphere.Radius, 1);

        Sphere.DisplayPoints(northPole, Color.green);
        Sphere.DisplayPoints(calgary, Color.red);
        Sphere.DisplayPoints(lebanon, Color.blue);
        Sphere.DisplayPoints(london, Color.gray);
        Sphere.DisplayPoints(mecca, Color.white);

        Path shortestPath = calgary.GetShortestPath(mecca);
        Path directPath = calgary.GetRhumbPath(mecca);

        //Path shortestPath = lebanon.GetShortestPath(mecca);
        //Path directPath = lebanon.GetRhumbPath(mecca);

        //Path shortestPath = london.GetShortestPath(mecca);
        //Path directPath = london.GetRhumbPath(mecca);

        Sphere.DisplayPaths(shortestPath, Color.red);
        Sphere.DisplayPaths(directPath, Color.green);

        Sphere.GetComponent<SphericalPaths.SphereRotation>().Focus(
            calgary.CartesianCoordinates.x, calgary.CartesianCoordinates.y);
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the sphere in the scene.
    /// </summary>
    public SphericalPaths.Sphere Sphere;

    #endregion

}
