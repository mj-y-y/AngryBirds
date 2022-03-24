using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   public List<Bird> birds;
   public List<Pig> pigs;
   public static GameManager _instance;
   private Vector3 originPos;//小鸟初始化的位置
   public GameObject win;
   public GameObject lose;
   public GameObject[] starts;

   public int starsNum = 0; //星星
   private void Awake() {
      _instance = this;
      if (birds.Count > 0)
      {
         originPos = birds[0].transform.position;
      }
     
   }
   private void Start() {
      Intiialized();
   }
/// <summary>
/// 初始化小鸟
/// </summary>

   private void Intiialized() {
       for (int i = 0; i < birds.Count; i++) {
          if(i == 0) { // 第一只小鸟可以飞出，其他不行
          birds[i].transform.position = originPos;
             birds[i].enabled = true;
             birds[i].sp.enabled = true;
          } else {  
             birds[i].enabled = false;
             birds[i].sp.enabled = false;
          }
       }
   }

/// <summary>
/// 判定游戏逻辑：如果当前猪已经被全部打完，小鸟便不用再被弹出；
/// </summary>
   public void NextBird() {
      if (pigs.Count > 0) {
         if (birds.Count > 0) {
            //下只小鸟飞出
            Intiialized();
         } else {   
             //输了
            lose.SetActive(true); 
         }
      } else {//赢了
             win.SetActive(true);
         }
      }
      public void ShowStars() {
         StartCoroutine("show");
      }
      IEnumerator show() {
         for (; starsNum < birds.Count + 1; starsNum++) {
            if (starsNum >= starts.Length)
            {
               break;
            }
            yield return new WaitForSeconds(0.15f);  //让星星一颗一颗的显示出来
            starts[starsNum].SetActive(true);
         }
      }


      //鼠标注册事件：
      public void Replay() {
         SaveStarNum();
         Time.timeScale = 1;
         SceneManager.LoadScene(2);
      }
      public void Menu() {
         Time.timeScale = 1;
         SaveStarNum();
         SceneManager.LoadScene(1);
      }

      /// <summary>
      /// 存储每个关卡过关后的星星数量
      /// </summary>
      public void SaveStarNum() {
         PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
      }
}
