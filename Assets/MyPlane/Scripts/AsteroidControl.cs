using UnityEngine;
using System.Collections;

public class AsteroidControl : MonoBehaviour
{

    //旋转的最大角度
    public float tumble;

    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tumble;
    }

    void Update()
    {

    }
}
