using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
    //爆炸物存活时间
    public float lifeTime;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

}
