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
        // Enable (to invoke the awake method) then disable them immediately
        Sphere.gameObject.SetActive(true);
        Plane.gameObject.SetActive(true);
        Sphere.gameObject.SetActive(false);
        Plane.gameObject.SetActive(false);
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the sphere in the scene.
    /// </summary>
    public SphericalPaths.Sphere Sphere;

    /// <summary>
    /// References the plane in the scene.
    /// </summary>
    public SphericalPaths.Plane Plane;

    #endregion

    #region Methods

    /// <summary>
    /// Generates a sample.
    /// </summary>
    private void GenerateSample()
    {
        Coordinates northPole = new Coordinates(new Vector2(0, 90), Sphere.Radius, Plane.Width);
        Coordinates calgary = new Coordinates(new Vector2(-114.0719f, 51.0447f), Sphere.Radius, Plane.Width);
        Coordinates lebanon = new Coordinates(new Vector2(35.539267f, 33.893940f), Sphere.Radius, Plane.Width);
        Coordinates london = new Coordinates(new Vector2(-0.104788f, 51.485530f), Sphere.Radius, Plane.Width);
        Coordinates mecca = new Coordinates(new Vector2(39.826210f, 21.422486f), Sphere.Radius, Plane.Width);

        Plane.DisplayPoints(northPole, Color.green);
        Plane.DisplayPoints(calgary, Color.red);
        Plane.DisplayPoints(lebanon, Color.blue);
        Plane.DisplayPoints(london, Color.gray);
        Plane.DisplayPoints(mecca, Color.white);

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

        Plane.DisplayPaths(shortestPath, Color.red);
        Plane.DisplayPaths(directPath, Color.green);
        Sphere.DisplayPaths(shortestPath, Color.red);
        Sphere.DisplayPaths(directPath, Color.green);

        Sphere.GetComponent<SphericalPaths.SphereRotation>().Focus(
            calgary.CartesianCoordinates.x, calgary.CartesianCoordinates.y);
    }

    #endregion

}
