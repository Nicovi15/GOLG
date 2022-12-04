using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    GameManager GM;

    [SerializeField]
    GameObject combatPrefab;

    public List<Unite> currentGen = new List<Unite>();

    public List<Combat> combats = new List<Combat>();

    public List<Vector3> combPos = new List<Vector3> { new Vector3(-6, 1.5f, 0), new Vector3(-3, 1.5f, 0), new Vector3(0, 1.5f, 0), new Vector3(3, 1.5f, 0), new Vector3(6, 1.5f, 0),
                                                        new Vector3(-6, -2, 0), new Vector3(-3, -2, 0), new Vector3(0, -2, 0), new Vector3(3, -2, 0), new Vector3(6, -2, 0)};

    public List<Unite> bests = new List<Unite>();

    public float speedBattle = 1.0f;

    public bool test = false;

    public bool enFight = false;

    public bool finished = false;

    public Unite winner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enFight)
        {
            foreach(Combat c in combats)
                if (!c.finished)
                    return;

            enFight = false;

            foreach (Combat c in combats)
                if (c.win)
                {
                    finished = true;
                    break;
                }

            float max = -1.0f;
            int maxJ = -1;
            for (int j = 0; j < combats.Count; j++)
            {
                if (combats[j].winScore > max)
                {
                    max = combats[j].winScore;
                    maxJ = j;
                }
            }
            winner = combats[maxJ].Ennemy;

        }
    }

    public void startRound()
    {
        // generation current gen

        if (GM.currentRound != 1)
        {
            // selection des 3 meilleurs
            foreach (Unite u in bests)
            {
                if (!currentGen.Contains(u))
                    Destroy(u.gameObject);
            }
            bests.Clear();
            for (int i = 0; i < 3; i++)
            {
                float min = 101.0f;
                int minJ = -1;
                for (int j = 0; j < combats.Count; j++)
                {
                    if (combats[j].score < min)
                    {
                        min = combats[j].score;
                        minJ = j;
                    }
                }

                bests.Add(combats[minJ].Ennemy);
                Destroy(combats[minJ].Champion.gameObject);
                Destroy(combats[minJ].gameObject);
                combats.Remove(combats[minJ]);
            }

            foreach (Unite u in currentGen)
            {
                if (!bests.Contains(u))
                    Destroy(u.gameObject);
            }
            currentGen.Clear();
            currentGen.AddRange(bests);
            while(currentGen.Count < 10)
            {
                currentGen.Add(GM.generateNewUniteRandom(bests, 20));
            }
        }
        else
        {
            foreach (Unite u in GM.baseUnites)
            {
                currentGen.Add(Instantiate(u));
            }
        }

        // generation des combats
        foreach(Combat c in combats)
        {
            Destroy(c.Champion.gameObject);
            Destroy(c.gameObject);
        }
        combats.Clear();

        for(int i = 0; i<10; i++)
        {
            Combat newCombat = Instantiate(combatPrefab).GetComponent<Combat>();
            newCombat.transform.position = combPos[i];
            newCombat.Champion = Instantiate(GM.Champion);
            newCombat.Champion.gameObject.SetActive(false);
            newCombat.Ennemy = currentGen[i];
            newCombat.Ennemy.gameObject.SetActive(false);
            newCombat.init();
            newCombat.speedBattle = speedBattle;
            combats.Add(newCombat);
        }

        foreach(Combat c in combats)
        {
            c.startBattle();
        }

        enFight = true;
    }

    internal void removeCombats()
    {
        foreach (Combat c in combats)
            Destroy(c.gameObject);
    }
}
