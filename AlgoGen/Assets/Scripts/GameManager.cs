using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentRound;
    public int maxBreedRound;

    [Header("Init Settings")]
    public int widthInit;
    public int hightInit;

    [SerializeField]
    GameObject basePrefab;

    [SerializeField]
    StatsSet S;

    [SerializeField]
    GameObject grilleInitPrefab;

    public GridInit GI;

    public List<Unite> baseUnites;

    [SerializeField]
    FichePerso previewUnite;

    [SerializeField]
    FichePerso previewParent0;
    [SerializeField]
    FichePerso previewParent1;
    [SerializeField]
    FichePerso previewParent2;

    [SerializeField]
    GameObject contourGrid2;

    [SerializeField]
    Grid G;

    [SerializeField]
    Tile ChampionTile;

    [SerializeField]
    GameObject ChampionSetup;

    [SerializeField]
    TextMeshProUGUI roundPhase;
    [SerializeField]
    TextMeshProUGUI TitlePhase;

    public bool isBreeding = true;
    public bool isFighting = false;
    public Unite Champion;

    // Start is called before the first frame update
    void Start()
    {
        startNewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startNewGame()
    {
        GI = Instantiate(grilleInitPrefab).GetComponent<GridInit>();
        GI.transform.position = new Vector3((-widthInit + 1) * 0.32f, -3.0f, 0);
        GI.CreateGrille(widthInit, hightInit);
        generateBaseUnites();

        roundPhase.text = "Round: " + currentRound + "/" + maxBreedRound;
    }

    public void generateBaseUnites()
    {
        for(int i = 0; i < widthInit; i++)
            for(int j = 0; j < hightInit; j++)
            {
                Unite newUnite = Instantiate(basePrefab).GetComponent<Ally>();
                newUnite.hpMax = Mathf.Clamp(Random.Range(S.defaultHpMax - S.offSetHpMax, S.defaultHpMax + S.offSetHpMax), 0, 100);
                newUnite.defense = Mathf.Clamp(Random.Range(S.defaultDefense - S.offSetDefense, S.defaultDefense + S.offSetDefense), 0, 100);
                newUnite.attack = Mathf.Clamp(Random.Range(S.defaultAttack - S.offSetAttack, S.defaultAttack + S.offSetAttack), 0, 100);
                newUnite.speedAtt = Mathf.Clamp(Random.Range(S.defaultSpeedAtt - S.offSetSpeedAtt, S.defaultSpeedAtt + S.offSetSpeedAtt), 0, 100);
                newUnite.crit = Mathf.Clamp(Random.Range(S.defaultCrit - S.offSetCrit, S.defaultCrit + S.offSetCrit), 0, 100);
                newUnite.speedRegen = Mathf.Clamp(Random.Range(S.defaultSpeedRegen - S.offSetSpeedRegen, S.defaultSpeedRegen + S.offSetSpeedRegen), 0, 100);
                newUnite.regen = Mathf.Clamp(Random.Range(S.defaultRegen - S.offSetRegen, S.defaultRegen + S.offSetRegen), 0, 100);
                newUnite.colorHigh = Random.Range(0.0f, 1.0f);
                int x = Random.Range((int)0, S.face.Count);
                newUnite.sp0 = S.face[x];
                newUnite.sp1 = S.faceb[x];

                //GameObject copy = Instantiate(newUnite.gameObject);
                baseUnites.Add(Instantiate(newUnite));
                baseUnites[baseUnites.Count - 1].gameObject.SetActive(false);

                newUnite.init();

                newUnite.currentTile = GI.tiles[i, j];
                newUnite.currentTile.setCurrentUnite(newUnite);
                newUnite.transform.position = newUnite.currentTile.transform.position;
                newUnite.transform.SetParent(newUnite.currentTile.transform);
            }

    }

    public void startPreview(Unite u)
    {
        previewUnite.gameObject.SetActive(true);
        previewUnite.updateFiche(u);
    }
    public void endPreview()
    {
        previewUnite.gameObject.SetActive(false);
    }

    public void startPreviewParent(List<Unite> u)
    {
        previewParent0.gameObject.SetActive(true);
        previewParent0.updateFiche(u[0]);
        previewParent1.gameObject.SetActive(true);
        previewParent1.updateFiche(u[1]);
        previewParent2.gameObject.SetActive(true);
        previewParent2.updateFiche(u[2]);
    }
    public void endPreviewParent()
    {
        previewParent0.gameObject.SetActive(false);
        previewParent1.gameObject.SetActive(false);
        previewParent2.gameObject.SetActive(false);
    }

    public void nextRound()
    {
        if (isBreeding && currentRound <= maxBreedRound)
        {
            G.processNextRound();
            if (currentRound == 0)
            {
                contourGrid2.SetActive(false);
                Destroy(GI.gameObject);
            }

            currentRound += 1;
            roundPhase.text = "Round: " + currentRound + "/" + maxBreedRound;

            if (currentRound > maxBreedRound)
            {
                ChampionSetup.SetActive(true);
                isBreeding = false;
                roundPhase.text = "Choose your champion !";
                G.removeCont();
            }
        }
        else
        {
            if (ChampionTile.currentUnite == null)
                return;
            Debug.Log(ChampionTile.currentUnite);
            Champion = ChampionTile.currentUnite;
            isFighting = true;
            currentRound = 0;
            TitlePhase.text = "Fighting Phase";
            roundPhase.text = "Round: " + currentRound;


        }
            
    }

    public Unite generateNewUnite(List<Unite> parents)
    {
        Unite parent0, parent1, parent2;
        int i = Random.Range((int)0, (int)3);
        parent0 = parents[i];
        i += 1;
        i %= 3;
        parent1 = parents[i];
        i += 1;
        i %= 3;
        parent2 = parents[i];
        i += 1;
        i %= 3;

        Unite newUnite = Instantiate(basePrefab).GetComponent<Ally>();
        newUnite.hpMax = parent0.hpMax;
        newUnite.defense = parent0.defense;
        newUnite.speedRegen = parent2.speedRegen;
        newUnite.attack = parent1.attack;
        newUnite.speedAtt = parent1.speedAtt;
        newUnite.regen = parent2.regen;
        newUnite.crit = parent2.crit;
        newUnite.colorHigh = parent0.colorHigh;
        newUnite.sp0 = parent1.sp0;
        newUnite.sp1 = parent1.sp1;

        newUnite.init();

        return newUnite;
    }

    public Unite generateNewUniteRandom(List<Unite> parents)
    {
        int probaMut = 10;

        Unite newUnite = Instantiate(basePrefab).GetComponent<Ally>();
        newUnite.hpMax = parents[Random.Range((int)0, (int)3)].hpMax + (Random.Range((int)0, (int)probaMut) == 0 ? Random.Range(1.0f, 10.0f) : 0);
        newUnite.defense = parents[Random.Range((int)0, (int)3)].defense + (Random.Range((int)0, (int)probaMut) == 0 ? Random.Range(1.0f, 10.0f) : 0);
        newUnite.speedRegen = parents[Random.Range((int)0, (int)3)].speedRegen + (Random.Range((int)0, (int)probaMut) == 0 ? Random.Range(1.0f, 10.0f) : 0);
        newUnite.attack = parents[Random.Range((int)0, (int)3)].attack + (Random.Range((int)0, (int)probaMut) == 0 ? Random.Range(1.0f, 10.0f) : 0);
        newUnite.speedAtt = parents[Random.Range((int)0, (int)3)].speedAtt + (Random.Range((int)0, (int)probaMut) == 0 ? Random.Range(1.0f, 10.0f) : 0);
        newUnite.regen = parents[Random.Range((int)0, (int)3)].regen + (Random.Range((int)0, (int)probaMut) == 0 ? Random.Range(1.0f, 10.0f) : 0);
        newUnite.crit = parents[Random.Range((int)0, (int)3)].crit + (Random.Range((int)0, (int)probaMut) == 0 ? Random.Range(1.0f, 10.0f) : 0);
        newUnite.colorHigh = parents[Random.Range((int)0, (int)3)].colorHigh;
        int i = Random.Range((int)0, (int)3);
        newUnite.sp0 = parents[i].sp0;
        newUnite.sp1 = parents[i].sp1;

        newUnite.init();

        return newUnite;
    }
}
