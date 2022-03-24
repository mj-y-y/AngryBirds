using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject pauseButton;

    private void Awake() {
        anim = GetComponent<Animator>();
    }
    public void Retry() {
         Time.timeScale = 1;
         SceneManager.LoadScene(2);
    }

/// <summary>
/// 点击了pause按钮
/// </summary>
    public void Pause() {

    //播放动画
        anim.SetBool("isPause", true); 
        pauseButton.SetActive(false);
    }

    public void Menu() {
         Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

/// <summary>
/// 点击了resume 按钮
/// </summary>
    public void Resume() {
       //播放动画
       Time.timeScale = 1;
       anim.SetBool("isPause", false);
       pauseButton.SetActive(true);
    }
    public void PauseAnimEnd() {
        Time.timeScale = 0;
    }
    // public void ResumeAnimEnd() {
         
    // }
}
