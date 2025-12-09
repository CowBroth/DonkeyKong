using UnityEngine;

public class ResolutionScript : MonoBehaviour
{
    public GameObject marker;
    void FixedUpdate()
    {
        if (gameObject.GetComponent<Camera>().fieldOfView > 30)
        {
            FixRes();
        }
    }
    void FixRes()
    {
        if (marker.GetComponent<MeshRenderer>().isVisible)
        {
            gameObject.GetComponent<Camera>().fieldOfView--;
            print("Setting Ratio...");
        }
    }
}
