using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unite : MonoBehaviour
{
    public abstract bool isEnnemy();

    public float hp;

    public float hpMax;
    public float defense;
    public float attack;
    public float speedAtt;
    public float crit;
    public float speedRegen;
    public float regen;

    public Sprite sp0;
    public Sprite sp1;
    public float delayAnim0;
    public float delayAnim1;


}
