using UnityEngine;
using System.Collections;

//该运动方式是朝着飞机的初始位置进行快速移动，类似流星效果
public class AnForPlaneRun : MonoBehaviour
{
    //移动的速度
    public float currentSpeed;
    //移动方式为追击飞机
    private GameObject planeObject;
    //运动的方向,在这里就不进行刷新了
    private Vector3 moveVector;

    void Start()
    {
        planeObject = GameObject.FindGameObjectWithTag("player");
        getNewMove();
        //只进行x方向上的追逐
        GetComponent<Rigidbody>().velocity = new Vector3(moveVector.x * currentSpeed, 0.0f, -currentSpeed);

    }

    void getNewMove()
    {
        //计算要运动的方向,转化为相对比例
        moveVector = planeObject.GetComponent<Rigidbody>().position - GetComponent<Rigidbody>().position;
        //计算两点间的距离,也就是平方和开二次方
        float path = Mathf.Pow(Mathf.Pow(moveVector.x, 2) + Mathf.Pow(moveVector.y, 2) + Mathf.Pow(moveVector.z, 2), 0.5f);
        moveVector = moveVector / path;
    }
}
