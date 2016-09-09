using UnityEngine;
public class DestroyByContact : MonoBehaviour
{

    //被攻击的次数
    private int shotCount;
    //需要爆炸的次数
    public int needToBoom;

    //音乐加载到爆炸对象里面，当实例化的时候就可以产生爆炸的音效
    public GameObject explosion;
    //音乐加载到爆炸对象里面，当实例化的时候就可以产生爆炸的音效
    public GameObject player;
    private GameControl mGameControl;
    public int score;

    void Start()
    {
        GameObject mGameObject = GameObject.FindGameObjectWithTag("GameController");
        if (mGameObject != null)
        {
            mGameControl = mGameObject.GetComponent<GameControl>();

        }
    }

    void OnTriggerEnter(Collider other)
    {


        //敌方物体碰撞不会产生爆炸，有敌机，敌机子弹，小行星，敌机子弹不会自行爆炸
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Asteroid" || other.tag == "EnemyShot")
        {
            return;
        }
        shotCount++;
        Destroy(other.gameObject);

        if (needToBoom == shotCount)
        {
            Destroy(gameObject);
            //实例化一个爆炸的对象
            Instantiate(explosion, transform.position, transform.rotation);
            mGameControl.addScore(score);
        }
        if (other.tag == "player")
        {
            Instantiate(player, other.transform.position, other.transform.rotation);
            mGameControl.GameOver();
        }

    }
}
