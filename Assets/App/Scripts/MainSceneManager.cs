using SphericalPaths.DataStructure;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Executes once on start.
    /// </summary>
    private void Awake()
    {
        Coordinates coordinates = new Coordinates(new Vector2(0, 90), 1, 1);
        Debug.Log(coordinates);
    }

    #endregion

}
