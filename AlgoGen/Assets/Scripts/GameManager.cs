using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int nbBaseUnites;

    [SerializeField]
    GameObject basePrefab;

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

    public List<Ally> baseUnites;


    // Start is called before the first frame update
    void Start()
    {
        generateBaseUnites();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateBaseUnites()
    {
        for(int i = 0; i < nbBaseUnites; i++)
        {
            baseUnites.Add(Instantiate(basePrefab, transform).GetComponent<Ally>());
            baseUnites[i].hpMax = Random.Range(defaultHpMax-offSetHpMax, defaultHpMax + offSetHpMax);
            baseUnites[i].defense = Random.Range(defaultDefense - offSetDefense, defaultDefense + offSetDefense);
            baseUnites[i].attack = Random.Range(defaultAttack - offSetAttack, defaultAttack + offSetAttack);
            baseUnites[i].speedAtt = Random.Range(defaultSpeedAtt - offSetSpeedAtt, defaultSpeedAtt + offSetSpeedAtt);
            baseUnites[i].crit = Random.Range(defaultCrit - offSetCrit, defaultCrit + offSetCrit);
            baseUnites[i].speedRegen = Random.Range(defaultSpeedRegen - offSetSpeedRegen, defaultSpeedRegen + offSetSpeedRegen);
            baseUnites[i].regen = Random.Range(defaultRegen - offSetRegen, defaultRegen + offSetRegen);
        }
    }


}
