using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FichePerso : MonoBehaviour
{
    [SerializeField]
    Image im;
    [SerializeField]
    Image bg;
    [SerializeField]
    Image eyes;
    [SerializeField]
    TextMeshProUGUI hp;
    [SerializeField]
    TextMeshProUGUI def;
    [SerializeField]
    TextMeshProUGUI att;
    [SerializeField]
    TextMeshProUGUI attS;
    [SerializeField]
    TextMeshProUGUI rgn;
    [SerializeField]
    TextMeshProUGUI rgnS;
    [SerializeField]
    TextMeshProUGUI crit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateFiche(Unite u)
    {
        im.sprite = u.sp0;
        im.color = Color.HSVToRGB(u.colorHigh, 0.65f, 1.0f);
        if(eyes != null)
            eyes.color = Color.HSVToRGB(u.colorHigh, 0.65f, 1.0f);
        bg.color = Color.HSVToRGB(u.colorHigh, 0.45f, 1.0f); 
        hp.text = u.hpMax.ToString("0.0");
        def.text = u.defense.ToString("0.0");
        att.text = u.attack.ToString("0.0");
        attS.text = u.speedAtt.ToString("0.0");
        rgn.text = u.regen.ToString("0.0");
        rgnS.text = u.speedRegen.ToString("0.0");
        crit.text = u.crit.ToString("0.0");
    }
}
