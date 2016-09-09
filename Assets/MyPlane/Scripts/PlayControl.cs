using UnityEngine;
using System.Collections;

[System.Serializable]
public class Bunnder
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;

}
public class PlayControl : MonoBehaviour
{
    //移动距离设置
    private float moveVertical;
    private float moveHorizontal;
    //设置移动的范围
    public Bunnder mBunnder;
    //移动的速度
    public float speed;
    //设置移动时候的迁徙度(z轴方向)
    public float titte;

    //发射子弹相关
    public GameObject shot;
    //双子弹
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public float fireRate;
    private float nextFire;

    void Update()
    {
        //自动开火
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn1.position, shotSpawn1.rotation);
            Instantiate(shot, shotSpawn2.position, shotSpawn2.rotation);

            GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate()
    {
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");
        //android设备上的移动
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            moveHorizontal = Input.GetTouch(0).deltaPosition.x * Time.deltaTime * 6;
            moveVertical = Input.GetTouch(0).deltaPosition.y * Time.deltaTime * 6;
        }
        //移动距离设置
        GetComponent<Rigidbody>().velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        //移动范围设置
        GetComponent<Rigidbody>().position = new Vector3
            (
            Mathf.Clamp(GetComponent<Rigidbody>().position.x, mBunnder.xMin, mBunnder.xMax),
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, mBunnder.zMin, mBunnder.zMax)
            );
        //移动迁徙度设置,根据x平移来设置
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * (-titte));
    }

    
}
