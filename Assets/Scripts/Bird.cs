using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private bool isClick = false;  //用于检测点击

    public float maxDistance = 3;//3m

    [HideInInspector] //使得下面这个公有变量在Inspector面板中看不到
    public SpringJoint2D sp; //让小鸟飞出

    protected Rigidbody2D rg; //用于控制小鸟飞出后的滑行下落
    public Transform rightPos;  //用于控制拖拽小鸟的最大距离、划线操作
    public LineRenderer right;
    public Transform leftPos;
    public LineRenderer left;
    public GameObject boom; //消失动画
    protected TestMyTrail myTrail; //拖尾
    private bool canFly = true; 
    public float smooth = 3; // 相机跟随
    public AudioClip select; 
    public AudioClip fly;
    private bool isFly = false;
    public Sprite hurt;
    protected SpriteRenderer render;

    private void Awake() {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    //鼠标按下
    private void OnMouseDown() { 
        if (canFly)
        {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }   
    }

    //鼠标抬起
    private void OnMouseUp() {
        isClick = false;
        canFly = false;
        rg.isKinematic = false;
        Invoke("Fly", 0.1f); //让SpringJoint2D 组件失活，但同时在将小鸟从 Dynamic 转成 kinematic 的时候，给其一定的时间进行动力学计算，避免运行时转换过快，小鸟飞不出去
        //禁用划线组件
        right.enabled = false;
        left.enabled = false;
    }

    private void Update() {
        if (isClick) //鼠标一直按下时，进行位置的跟随
        {
            //注： 小鸟是以世界坐标，而鼠标是屏幕坐标（其原点是屏幕的左下角点）
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //遇到一个问题便是，小鸟的位置转换后与摄像头在同一个z轴，因此摄像头拍摄不到。
            // 解决方法： 
            // （1）转换时获取到小鸟的z轴，然后设为固定值 :transform.position += new Vector3(0, 0, 10);
            // （2）转换后减去z轴的值 
           transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            //小鸟活动的最大距离限制
            if (Vector3.Distance(transform.position, rightPos.position) > maxDistance) { //进行位置限定
                Vector3 pos = (transform.position - rightPos.position ).normalized; //单位/规范化向量 ：获得该向量的方向
                pos *= maxDistance; // 最大长度的向量 ：获得其长度
                transform.position = pos + rightPos.position; 
            }

            Line();
        }
         //相机跟随
        float posx = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, new Vector3(Mathf.Clamp(posx, 0, 16), Camera.main.transform.position.y, Camera.main.transform.position.z), smooth * Time.deltaTime );

        if (isFly) 
        {
            if(Input.GetMouseButtonDown(0)) {
                ShowSkill();
            }
        }
    }
    void Fly() {
        isFly = true;
        AudioPlay(fly);
        myTrail.TrailStart();
        sp.enabled = false;
        Invoke("Next", 4f);
    }

    //划线函数
    void Line() {
        //当划线启用时，激活划线组件
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    /// <summary>
    /// 下只小鸟的飞出 
    /// </summary>

    protected virtual void Next() {
         GameManager._instance.birds.Remove(this);
         Destroy(gameObject);   
         Instantiate(boom, transform.position, Quaternion.identity);
         GameManager._instance.NextBird();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        isFly = false;
        myTrail.TrailClear();
        
    }

    public void AudioPlay(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

/// <summary>
/// 小鸟的强化功能 炫技
/// </summary>
   public virtual void ShowSkill() { 
        isFly = false;
   }
   /// <summary>
   /// 小鸟受伤
   /// </summary>
   public void Hurt() {
       render.sprite = hurt;
   }
}
