using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public bool isFree;
    public Unite currentUnite;

    public Color evenColor;
    public Color oddColor;

    SpriteRenderer SR;

    int i, j;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(int i, int j)
    {
        if ((i+j) % 2 == 0)
            SR.color = evenColor;
        else
            SR.color = oddColor;

        this.i = i;
        this.j = j;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log(i + ' ' + j);
    }

}
