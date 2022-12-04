using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{
    public float BaseDegat = 15;

    public Ally cloneC;
    public Ally cloneE;

    [SerializeField]
    public Unite Champion;

    [SerializeField]
    public Unite Ennemy;

    [SerializeField]
    Image hpBarChampion;

    [SerializeField]
    Image hpBarEnnemy;

    [SerializeField]
    SpriteRenderer SRc;
    [SerializeField]
    SpriteRenderer SRe;

    [SerializeField]
    SpriteRenderer bg;

    [SerializeField]
    Color winCol;

    [SerializeField]
    Color looseCol;

    [SerializeField]
    Eyes eyes;

    public float cdChamp;
    public float cdEnnemy;
    public float cdChampH;
    public float cdEnnemyH;

    public bool finished;

    public bool start = false;
    public float speedBattle = 1.0f;

    public float score;
    public bool win = false;
    public float winScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            start = false;
            StartCoroutine(battle());
        }
    }

    public void init()
    {
        SRc.sprite = Champion.sp0;
        SRc.color = Color.HSVToRGB(Champion.colorHigh, 0.65f, 1.0f);
        cloneC.sp0 = Champion.sp0;
        cloneC.sp1 = Champion.sp1;
        SRe.sprite = Ennemy.sp0;
        SRe.color = Color.HSVToRGB(Ennemy.colorHigh, 0.65f, 1.0f);
        cloneE.sp0 = Ennemy.sp0;
        cloneE.sp1 = Ennemy.sp1;

        Champion.hp = Champion.hpMax;
        Ennemy.hp = Ennemy.hpMax;

        eyes.init(Ennemy.colorHigh);
        updateHB();
    }

    IEnumerator battle()
    {
        cdChamp = Mathf.Clamp(1.0f - (Champion.speedAtt / 100.0f), 0.2f, 1.0f);
        cdEnnemy = Mathf.Clamp(1.0f - (Ennemy.speedAtt / 100.0f), 0.2f, 1.0f);

        cdChampH = Mathf.Clamp(2.0f - (Champion.speedRegen / 100.0f), 0.2f, 2.0f);
        cdEnnemyH = Mathf.Clamp(2.0f - (Ennemy.speedRegen / 100.0f), 0.2f, 2.0f);

        Champion.hp = Champion.hpMax;
        Ennemy.hp = Ennemy.hpMax;

        updateHB();

        finished = false;

        float tC = 0.0f;
        float tE = 0.0f;

        float hC = 0.0f;
        float hE = 0.0f;

        while (!finished)
        {
            tC += Time.deltaTime * speedBattle;
            tE += Time.deltaTime * speedBattle;
            hC += Time.deltaTime * speedBattle;
            hE += Time.deltaTime * speedBattle;

            if (tC > cdChamp)
            {
                tC = 0.0f;
                Ennemy.hp -= attack(Champion, Ennemy) * (UnityEngine.Random.Range(0.1f, 100.0f) <= Champion.crit ? 1.5f : 1.0f);
                updateHB();

                if (Ennemy.hp <= 0)
                {
                    finished = true;
                    break;
                }
                    
            }

            if(tE > cdEnnemy)
            {
                tE = 0.0f;
                Champion.hp -= attack(Ennemy, Champion) * (UnityEngine.Random.Range(0.1f, 100.0f) <= Ennemy.crit ? 1.5f : 1.0f);
                updateHB();

                if (Champion.hp <= 0)
                {
                    finished = true;
                    break;
                }
            }

            if(hC > cdChampH)
            {
                hC = 0.0f;
                Champion.hp += ((Champion.regen / 100.0f) * Champion.hpMax) * (UnityEngine.Random.Range(0.0f, 100.0f) <= Champion.crit ? 1.5f : 1.0f);
                updateHB();
            }

            if (hE > cdChampH)
            {
                hE = 0.0f;
                Ennemy.hp += ((Ennemy.regen / 100.0f) * Ennemy.hpMax) * (UnityEngine.Random.Range(0.0f, 100.0f) <= Ennemy.crit ? 1.5f : 1.0f);
                updateHB();
            }
            yield return null;
        }

        if(Champion.hp <= 0)
        {
            score = -1;
            win = true;
            winScore = (Ennemy.hp / Ennemy.hpMax) * 100.0f;
            bg.color = looseCol;
        }
        else
        {
            score = (Champion.hp / Champion.hpMax) * 100.0f;
            bg.color = winCol;
        }

        yield return null;
    }

    internal void startBattle()
    {
        StartCoroutine(battle());
    }

    public float attack(Unite attaquant, Unite defenseur)
    {
        //Debug.Log(((BaseDegat * (100.0f * 0.4f + 2) * attaquant.attack) / (defenseur.defense * 50)) + 2);
        return ((BaseDegat * (100.0f * 0.4f + 2) * attaquant.attack) / (defenseur.defense * 50)) + 2;
    }

    public void updateHB()
    {
        hpBarChampion.fillAmount = Mathf.Clamp(Champion.hp / Champion.hpMax, 0.0f, 1.0f);
        hpBarEnnemy.fillAmount = Mathf.Clamp(Ennemy.hp / Ennemy.hpMax, 0.0f, 1.0f);
    }
}
