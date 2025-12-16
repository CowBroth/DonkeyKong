using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public static ManagerScript instance;
    public GameObject player;
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void DestroyLife1()
    {
        Destroy(life3);
    }
    public void DestroyLife2()
    {
        Destroy(life2);
    }
    public void DestroyLife3()
    {
        Destroy(life1);
    }
    public void DestroyLife4()
    {
        Application.Quit();
    }
}
