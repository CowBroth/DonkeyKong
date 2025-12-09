using UnityEngine;

public class BarrelSpawn : MonoBehaviour
{
    public GameObject barrel;
    public float minTime;
    public float maxTime;
    void Start()
    {
        InstanBarrel();
    }

    void InstanBarrel()
    {
        Instantiate(barrel, transform.position, Quaternion.identity);
        Invoke(nameof(InstanBarrel), Random.Range(minTime, maxTime));
    }
}
