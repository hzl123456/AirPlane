using UnityEngine;
using System.Collections;

public class EnemyPlaneControl : MonoBehaviour
{
    //移动的范围
    public Bunnder boundary;
    //飞机移动时候翻转的角度
    public float tilt;
    //移动的速度
    public float currentSpeed;
    //移动方式为追击飞机
    private  GameObject planeObject;
    //追击飞机的刷新频率
    public float moveTime;
    //运动的方向,每隔一定的时间刷新一次
    private Vector3 moveVector;

    //发射子弹相关
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    void Start()
    {
        planeObject = GameObject.FindGameObjectWithTag("player");
        StartCoroutine(getNewMove());
    }

    void Update()
    {
        //自动开火
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate()
    {
        //只进行x方向上的追逐
        GetComponent<Rigidbody>().velocity = new Vector3(moveVector.x*currentSpeed, 0.0f ,-currentSpeed);

        GetComponent<Rigidbody>().position = new Vector3
        (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, GetComponent<Rigidbody>().velocity.x * tilt);
    }

    //在这里刷新时间
    IEnumerator getNewMove()
    {
        while (true)
        {
            if (planeObject!=null&&GetComponent<Rigidbody>()!=null&& planeObject.GetComponent<Rigidbody>()!=null)
            {
            //计算要运动的方向,转化为相对比例
            moveVector = planeObject.GetComponent<Rigidbody>().position - GetComponent<Rigidbody>().position;
            //计算两点间的距离,也就是平方和开二次方
            float path = Mathf.Pow(Mathf.Pow(moveVector.x, 2) + Mathf.Pow(moveVector.y, 2) + Mathf.Pow(moveVector.z, 2), 0.5f);
            moveVector = moveVector / path;
            yield return new WaitForSeconds(moveTime);
            }
        }
    }

    //播放爆炸声音
    public void openAudio()
    {
        GetComponent<AudioSource>().Play();
    }
}
