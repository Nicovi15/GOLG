using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "StatsSet", order = 1)]
public class StatsSet : ScriptableObject
{
    public float defaultHpMax;
    public float offSetHpMax;

    public float defaultDefense;
    public float offSetDefense;

    public float defaultAttack;
    public float offSetAttack;

    public float defaultSpeedAtt;
    public float offSetSpeedAtt;

    public float defaultCrit;
    public float offSetCrit;

    public float defaultSpeedRegen;
    public float offSetSpeedRegen;

    public float defaultRegen;
    public float offSetRegen;

    public List<Sprite> face;
    public List<Sprite> faceb;
}
