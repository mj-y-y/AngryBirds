using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{
    public override void ShowSkill()
    {
        base.ShowSkill();
        Vector3 speed = rg.velocity;
        speed.x *= -1;//换向
        rg.velocity = speed;
    }
}
