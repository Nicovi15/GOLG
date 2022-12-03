using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Ally : Unite
{
    public Ally(Unite a) : base(a)
    {
    }

    /*
public Ally(Unite a)
{
   hp = a.hp;
   hpMax = a.hpMax;
   defense = a.defense;
   attack = a.attack;
   speedAtt = a.speedAtt;
   crit = a.crit;
   speedRegen = a.speedRegen;
   regen = a.regen;
   colorHigh = a.colorHigh;
   sp0 = a.sp0;
   sp1 = a.sp1;
   delayAnim0 = a.delayAnim0;
   delayAnim1 = a.delayAnim1;
}
*/


    public override bool isEnnemy()
    {
        return false;
    }

    
}
