using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{
    //实例化初始追踪的陨石
    public GameObject hezardForPlane;
    //实例化的小行星物体
    public GameObject hezard;
    //实例化敌方战机
    public GameObject enemyPlane;
    //实例化需要的位置坐标
    public Vector3 spanValues;
    //没波出现的小行星个数
    public int spanCount;
    //每个小怪出现的时间间隔
    public float littleTime;
    //每波怪物的间隔
    public float largeTime;
    //开始的间隔时间
    public float startTime;
    //更新得分
    public GUIText scoreText;
    public int score;

    public GUIText gameOverText;
    public GUIText restartText;

    private bool gameover;
    private bool restartGame;

    //敌机的刷新频率
    private float enemyPlaneTime = 4.0f;
    //追踪陨石的刷新频率
    private float hezardForPlaneTime = 1.0f;

    void Start()
    {
        gameOverText.text = "";
        restartText.text = "";
        updataScore();
        StartCoroutine(spanValuesForGame());
        StartCoroutine(getHezardForPlane());
        StartCoroutine(getEnemyPlaneForGame());
    }
    void Update()
    {
        if (restartGame && (Input.GetKeyDown(KeyCode.R)||Input.GetKeyDown(KeyCode.Joystick1Button2)))//手柄的x键
        {
            //重新加载本场景
            Application.LoadLevel(Application.loadedLevel);
        }

    }


    IEnumerator spanValuesForGame()
    {
        yield return new WaitForSeconds(startTime);
        //创建垂直降落的小行星
        while (!gameover)
        {
            for (int i = 0; i < spanCount; i++)
            {
                //在位置上y是0与飞机同平面，z的位置固定在上面，x的位置随机
                Vector3 span = new Vector3(Random.Range(-spanValues.x, spanValues.x), spanValues.y, spanValues.z);
                Instantiate(hezard, span, Quaternion.identity);
                //类似与java里面的休眠，不过线程的调用方式跟java有点不太一样
                yield return new WaitForSeconds(littleTime);
            }
            yield return new WaitForSeconds(largeTime);
        }


    }

    IEnumerator getEnemyPlaneForGame()
    {
        yield return new WaitForSeconds(startTime);
        //创建可以发射子弹的敌机
        while (!gameover)
        {
            //在位置上y是0与飞机同平面，z的位置固定在上面，x的位置随机
            Vector3 span = new Vector3(Random.Range(-spanValues.x, spanValues.x), spanValues.y, spanValues.z);
            Instantiate(enemyPlane, span, Quaternion.identity);
            //类似与java里面的休眠，不过线程的调用方式跟java有点不太一样
            yield return new WaitForSeconds(enemyPlaneTime);
        }

        if (gameover)
        {
            restartText.text = "Press 'R' to Restart";
            restartGame = true;
        }
    }

    IEnumerator getHezardForPlane()
    {
        yield return new WaitForSeconds(startTime);
        //创建追踪的陨石
        while (!gameover)
        {
            //在位置上y是0与飞机同平面，z的位置固定在上面，x的位置随机
            Vector3 span = new Vector3(Random.Range(-spanValues.x, spanValues.x), spanValues.y, spanValues.z);
            Instantiate(hezardForPlane, span, Quaternion.identity);
            //类似与java里面的休眠，不过线程的调用方式跟java有点不太一样
            yield return new WaitForSeconds(hezardForPlaneTime);
        }

    }


    void updataScore()
    {
        scoreText.text = "Score:" + score;
    }


    public void addScore(int newScore)
    {
        score += newScore;
        updataScore();
    }

    public void GameOver()
    {
        gameover = true;
        gameOverText.text = "GAME OVER";
    }



}
