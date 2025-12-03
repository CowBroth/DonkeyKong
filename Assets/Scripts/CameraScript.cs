using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Camera cam;
    Rect angle;
    void Start()
    {
        cam = GetComponent<Camera>();
        float screenSize = (float)Screen.width / Screen.height;
        float screenHeight = screenSize / 0.5625f;
        if (screenHeight < 1)
        {
            angle = cam.rect;
            angle.width = 1;
            angle.height = screenHeight;
            angle.x = 0;
            angle.y = (1 - screenHeight) / 2;
            cam.rect = angle;
        }
        else
        {
            float screenWidth = 1 / screenHeight;
            angle = cam.rect;
            angle.width = screenWidth;
            angle.height = 1;
            angle.x = (1 - screenHeight) / 2;
            angle.y = 0;
            cam.rect = angle;
        }
    }
}
