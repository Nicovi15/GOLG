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

    public float colorHigh;
    public Sprite sp0;
    public Sprite sp1;
    public float delayAnim0;
    public float delayAnim1;


    public Tile currentTile;
    protected SpriteRenderer SR;

    Mouse M;
    GameManager GM;

    public Unite(Unite a)
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

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        M = GameObject.Find("Mouse").GetComponent<Mouse>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(animation());
    }

    public void init()
    {
        SR.color = Color.HSVToRGB(colorHigh, 0.65f, 1.0f);
        SR.sprite = sp0;
        hp = hpMax;
    }

    public void gotGrabbed()
    {
        SR.sortingOrder = 10;
    }

    public void gotDropped()
    {
        SR.sortingOrder = 5;
    }

    IEnumerator animation()
    {
        float t = 0;
        bool a = true;
        while (true)
        {
            t += Time.deltaTime;
            if (a)
            {
                if (t > delayAnim0)
                {
                    t = 0;
                    a = !a;
                    if (a)
                        SR.sprite = sp0;
                    else
                        SR.sprite = sp1;
                }
            }
            else
            {
                if (t > delayAnim1)
                {
                    t = 0;
                    a = !a;
                    if (a)
                        SR.sprite = sp0;
                    else
                        SR.sprite = sp1;
                }
            }

            yield return null;
        }
    }

    private void OnMouseEnter()
    {
        if (M.currentGrab != null && M.currentGrab != this)
            return;
        GM.startPreview(this);
    }

    private void OnMouseOver()
    {
        if (M.currentGrab != null && M.currentGrab != this)
            return;
        GM.startPreview(this);
    }

    private void OnMouseExit()
    {
        if (M.currentGrab != null)// && M.currentGrab != this)
            return;
        GM.endPreview();
    }

}
