using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //引入UI的命名空间；
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public bool isSelect = false;
    public Sprite levelBg;
    private Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }
    private void Start() {
        //找到父物体的第一个子物体
        if (transform.parent.GetChild(0).name == gameObject.name)
        {
            isSelect = true;
        }
        if (isSelect)
        {
            image.overrideSprite = levelBg;
            transform.Find("num").gameObject.SetActive(true);
        }
    }

    public void Selected() {
        if(isSelect) {
            //直接进入game场景，开始玩游戏
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
        }
    }
}
