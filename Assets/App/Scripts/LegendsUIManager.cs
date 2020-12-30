using UnityEngine;
using UnityEngine.UI;

public class LegendsUIManager : MonoBehaviour
{

    #region Initialization

    /// <summary>
    /// Initializes the displayed colors.
    /// </summary>
    public void Initialize(Color startPointColor, Color endPointColor,
        Color shortestPathColor, Color rhumbPathColor)
    {
        StartPointImage.color = startPointColor;
        EndPointImage.color = endPointColor;
        ShortestPathImage.color = shortestPathColor;
        RhumbLineImage.color = rhumbPathColor;
    }

    #endregion

    #region Fields/Properties

    /// <summary>
    /// References the image UI element that displays the color of the start point pin.
    /// </summary>
    [Tooltip("References the image UI element that displays the color of the start point pin.")]
    [SerializeField]
    private Image StartPointImage;

    /// <summary>
    /// References the image UI element that displays the color of the end point pin.
    /// </summary>
    [Tooltip("References the image UI element that displays the color of the end point pin.")]
    [SerializeField]
    private Image EndPointImage;

    /// <summary>
    /// References the image UI element that displays the color of the shortest path.
    /// </summary>
    [Tooltip("References the image UI element that displays the color of the shortest path.")]
    [SerializeField]
    private Image ShortestPathImage;

    /// <summary>
    /// References the image UI element that displays the color of the rhumb path.
    /// </summary>
    [Tooltip("References the image UI element that displays the color of the rhumb path.")]
    [SerializeField]
    private Image RhumbLineImage;

    #endregion

}
