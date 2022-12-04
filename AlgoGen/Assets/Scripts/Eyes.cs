using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    protected SpriteRenderer SR;
    public Sprite sp0;
    public Sprite sp1;
    public float delayAnim0;
    public float delayAnim1;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        StartCoroutine(animation());
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(float colorHigh)
    {
        SR.color = Color.HSVToRGB(colorHigh, 0.65f, 1.0f);
    }
}
