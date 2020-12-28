using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphericalPaths
{
    public class SphereRotation : MonoBehaviour
    {

        #region Initialization

        /// <summary>
        /// Executes once on awake.
        /// </summary>
        private void Awake()
        {
            // Compute offset rotation
            if (FocusAt != null)
            {
                GameObject temp = new GameObject();
                temp.transform.parent = transform;
                temp.transform.LookAt(FocusAt.transform);
                OffsetRotation = -temp.transform.eulerAngles;
                Destroy(temp);
            }
        }

        #endregion

        #region Fields/Properties

        /// <summary>
        /// The longitude the sphere is currently focused on.
        /// </summary>
        [Tooltip("The longitude the sphere is currently focused on.")]
        [SerializeField]
        private float Longitude;

        /// <summary>
        /// The latitude the sphere is currently focused on.
        /// </summary>
        [Tooltip("The latitude the sphere is currently focused on.")]
        [SerializeField]
        private float Latitude;

        /// <summary>
        /// The object used to set the focus angle.
        /// </summary>
        [Tooltip("The object used to set the focus angle.")]
        [SerializeField]
        private GameObject FocusAt;

        /// <summary>
        /// Offset rotation computed and then applied to target our focus object.
        /// </summary>
        private Vector3 OffsetRotation = new Vector3(0, 0, 0);

        #endregion

        #region Methods

        /// <summary>
        /// Focuses on a specific coordinates.
        /// </summary>
        public void Focus(float longitude, float latitude)
        {
            Longitude = longitude;
            Latitude = latitude;

            ApplyPositionToTransform();
        }

        /// <summary>
        /// Rotates the object  to focus on the computed longitude and latitude.
        /// </summary>
        private void ApplyPositionToTransform()
        {
            // Get spherical coordinates
            DataStructure.Coordinates coordinates = new DataStructure.Coordinates(new Vector2(Longitude, Latitude), 5);

            // Get current direction of the focus point
            Vector3 currentDirection = coordinates.SphericalCoordinates;

            // Compute rotation
            GameObject temp = new GameObject();
            temp.transform.localPosition = currentDirection;
            temp.transform.forward = currentDirection;
            Vector3 rotation = temp.transform.eulerAngles;
            Destroy(temp);

            // Apply rotation
            transform.RotateAround(transform.position, transform.right, -rotation.x);
            transform.RotateAround(transform.position, transform.up, -rotation.y);

            // Apply offset rotation
            transform.RotateAround(transform.position, Vector3.up, OffsetRotation.y);
            transform.RotateAround(transform.position, Vector3.right, OffsetRotation.x);
            transform.RotateAround(transform.position, Vector3.forward, OffsetRotation.z);
        }

        #endregion

    }
}
