using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{

    public List<Pig> blocks = new List<Pig>();

/// <summary>
/// 进入触发区域则被销毁【进入则存进list，然逐个销毁】
/// </summary>
/// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            blocks.Add(other.gameObject.GetComponent<Pig>());
        }
    }

    /// <summary>
/// 离开触发区域
/// </summary>
/// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            blocks.Remove(other.gameObject.GetComponent<Pig>());
        }
    }
    public override void ShowSkill()
    {
        base.ShowSkill();
        if (blocks.Count > 0 && blocks != null)
        {
            for (int i = 0; i < blocks.Count; i++) {
                blocks[i].Dead();
            }
        }
        OnClear();
    }

    public void OnClear()  {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        render.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        myTrail.TrailClear(); 
    }

    protected override void Next() {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        GameManager._instance.NextBird();
    }
}
