using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contour : MonoBehaviour
{

    public Color delColor;
    public Color newColor;
    //public Color lockColor;
    public float delay;
    public float delayDel;
    public float delayNew;

    public float normalSize, lowSize;

    public Tile t;
    SpriteRenderer SR;
    BoxCollider2D col;

    Mouse M;

    private void Awake()
    {
        M = GameObject.Find("Mouse").GetComponent<Mouse>();
        SR = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void showNew()
    {
        SR.color = newColor;
        delay = delayNew;
        col.enabled = true;
    }

    public void showDel()
    {
        SR.color = delColor;
        delay = delayDel;
        col.enabled = false;
    }

    private void OnEnable()
    {
        StartCoroutine(animation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator animation()
    {
        float t = 0;
        bool a = true;
        while (true)
        {
            t += Time.deltaTime;
                if (t > delay)
                {
                    t = 0;
                    a = !a;
                    if (a)
                        transform.localScale = new Vector3(normalSize, normalSize, 1);
                    else
                    transform.localScale = new Vector3(lowSize, lowSize, 1);
            }

            yield return null;
        }
     
    }

    private void OnMouseEnter()
    {
        if (t.status == tileState.born && M.currentGrab == null)
        {
            t.startPreviewParent();
        }
    }

    private void OnMouseExit()
    {
        if (t.status == tileState.born)
            t.endPreviewParent();
    }
}
