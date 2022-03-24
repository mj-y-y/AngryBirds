using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer  render;
    public  Sprite hurt; 
    public GameObject boom;
    public GameObject pigScore;
    public bool isPig = false;
    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision; 

    private void Awake() {
        render = GetComponent<SpriteRenderer>();

    }

    //触发检测 ： 保证其中一个物体挂载Rigidbody 2D 组件，并且勾选 Is Trigger
    // private void OnTriggerEnter2D(Collider2D other) {    }
    //检测碰撞 ：两个物体需要同时挂载Rigidbody 2D 和 Circle Collider 2D 组件
    private void OnCollisionEnter2D(Collision2D collision) {
         
        if (collision.gameObject.tag == "Player")
        {
            AudioPlay(birdCollision);
            collision.transform.GetComponent<Bird>().Hurt();
        }
        //当游戏物体的相对速度达到某值，便受伤
        if (collision.relativeVelocity.magnitude > maxSpeed) { //直接死亡
            Dead();
        } 
        else if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed) { //受伤换图
            render.sprite = hurt;
            AudioPlay(hurtClip);
        }      
    }

    //猪死后
    public void Dead() {
        if (isPig) 
        {
            GameManager._instance.pigs.Remove(this);
        }
        Destroy(gameObject);
        //猪死后生成的特效：
        Instantiate(boom, transform.position, Quaternion.identity);
        //加分特效：
        GameObject score =  Instantiate(pigScore, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        Destroy(score, 1.5f); //1.5秒后销毁
        AudioPlay(dead);
    }

    public void AudioPlay(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, transform.position); // 避免挂载Audio组件，pig在销毁之后，音乐却未播放完的情况
    }

}
