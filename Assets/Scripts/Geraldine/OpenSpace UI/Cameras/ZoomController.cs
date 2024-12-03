using UnityEngine;

namespace Geraldine.OpenSpaceUI.Cameras
{
    public class ZoomController : MonoBehaviour
    {
        public Camera mainCamera => Camera.main;
        public float zoomSpeed = 10f;
        public float minZoom = 5f;
        public float maxZoom = 1000f;

        private float currentZoom;

        void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            currentZoom = Mathf.Clamp(mainCamera.fieldOfView - scroll * zoomSpeed, minZoom, maxZoom);
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, currentZoom, Time.deltaTime * zoomSpeed);
            AdjustLOD(currentZoom);
        }

        void AdjustLOD(float zoom)
        {
            if (zoom < 50f)
                SetPlanetaryLOD();
            else if (zoom < 200f)
                SetSystemLOD();
            else
                SetGalaxyLOD();
        }
        void SetPlanetaryLOD()
        {
            // Enable high-detail objects.
        }

        void SetSystemLOD()
        {
            // Enable system-level objects, disable high-detail planets.
        }

        void SetGalaxyLOD()
        {
            // Enable galaxy-level icons, hide detailed systems and planets.
        }

    }

}
