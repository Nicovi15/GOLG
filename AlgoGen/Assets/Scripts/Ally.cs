using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Ally : Unite
{
    SpriteRenderer SR;


    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        StartCoroutine(animation());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool isEnnemy()
    {
        return false;
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
}
