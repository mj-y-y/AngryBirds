using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour
{
    public int starsNeed;  //每个关卡需要的星星数量
    public bool isSelect = false;
    public GameObject checkpointLock;
    public GameObject stars;
    public GameObject panel;
    public GameObject map;


/// <summary>
/// 统计通关之后的星星数量
/// </summary>
    private void Start() {
        if (PlayerPrefs.GetInt("totalNum", 0) >= starsNeed) {
            isSelect = true;
        }
        if (isSelect)
        {
            checkpointLock.SetActive(false);
            stars.SetActive(true);
            
            //todo:TEXT显示

        }
        // 可以进入不同地图时的Text显示
    }

    public void Selected() {
        if(isSelect) {
            panel.SetActive(true);
            map.SetActive(false);
        }    
    }
}
