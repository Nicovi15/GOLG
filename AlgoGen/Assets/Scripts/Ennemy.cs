using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : Unite
{
    public Ennemy(Unite a) : base(a)
    {
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
        return true;
    }
}
