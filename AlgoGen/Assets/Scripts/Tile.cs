using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum tileState
{
    nothing = 0,
    born = 1,
    death = 2
}

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public bool isFree = true;
    public Unite currentUnite;

    public Color evenColor;
    public Color oddColor;

    SpriteRenderer SR;

    int i, j;

    Grid G;

    public List<Tile> neighbors = new List<Tile>();

    [SerializeField]
    GameObject ContourPrefab;

    Contour cont;

    public tileState status = tileState.nothing;
    GameManager GM;

    public List<Unite> parents = new List<Unite>();

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
 
    }

    public void init(int i, int j)
    {
        if ((i + j) % 2 == 0)
            SR.color = evenColor;
        else
            SR.color = oddColor;

        this.i = i;
        this.j = j;
        isFree = true;
    }

    public void init(int i, int j, Grid grid)
    {
        if ((i+j) % 2 == 0)
            SR.color = evenColor;
        else
            SR.color = oddColor;

        this.i = i;
        this.j = j;
        isFree = true;

        G = grid;
        status = tileState.nothing;
        cont = Instantiate(ContourPrefab, transform).GetComponent<Contour>();
        cont.transform.localPosition = Vector3.zero;
        cont.t = this;
        updateContour();
    }

    public void processBorn()
    {
        if (status == tileState.nothing || status == tileState.death)
            return;

        Unite newUnite = GM.generateNewUniteRandom(parents);
        newUnite.init();
        newUnite.currentTile = this;
        currentUnite = newUnite;
        isFree = false;
        newUnite.transform.position = transform.position;
        newUnite.transform.SetParent(transform);
    }

    internal void removeCont()
    {
        cont.gameObject.SetActive(false);
    }

    public void processDel()
    {
        if (status == tileState.nothing || status == tileState.born)
            return;

        Destroy(currentUnite.gameObject);
        currentUnite = null;
        isFree = true;
    }

    public void initNeighbors()
    {
        if (i - 1 > -1)
            neighbors.Add(G.tiles[i - 1, j]);
        if (j - 1 > -1)
            neighbors.Add(G.tiles[i, j - 1]);
        if (i + 1 < G.width)
            neighbors.Add(G.tiles[i + 1, j]);
        if (j + 1 < G.hight)
            neighbors.Add(G.tiles[i, j + 1]);

        if (i - 1 > -1 && j - 1 > -1)
            neighbors.Add(G.tiles[i - 1, j - 1]);
        if (j - 1 > -1 && i + 1 < G.width)
            neighbors.Add(G.tiles[i + 1, j - 1]);
        if (i + 1 < G.width && j + 1 < G.hight)
            neighbors.Add(G.tiles[i + 1, j + 1]);
        if (j + 1 < G.hight && i - 1 > -1)
            neighbors.Add(G.tiles[i - 1, j + 1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurrentUnite(Unite unite)
    {
        currentUnite = unite;
        isFree = false;

        if(G && GM.isBreeding)
            G.updateTiles();
    }

    public void removeUnite()
    {
        currentUnite = null;
        isFree = true;
        if (G && GM.isBreeding)
            G.updateTiles();
    }

    public void updateStatus()
    {
        parents.Clear();
        int n = 0;
        foreach (Tile t in neighbors)
            if (!t.isFree)
                n++;

        if (isFree)
        {
            if (n == 3)
            {
                status = tileState.born;
                foreach (Tile t in neighbors)
                    if (!t.isFree)
                        parents.Add(t.currentUnite);
            }
            else
                status = tileState.nothing;
        }
        else
        {
            if (n <= 1 || n >= 4)
                status = tileState.death;
            else
                status = tileState.nothing;
        }

        updateContour();
    }

    public void updateContour()
    {
        if (status == tileState.nothing)
        {
            cont.gameObject.SetActive(false);
        }
        else if(status == tileState.born)
        {
            cont.gameObject.SetActive(true);
            cont.showNew();
        }
        else
        {
            cont.gameObject.SetActive(true);
            cont.showDel();
        }
            
    }

    public void startPreviewParent()
    {
        GM.startPreviewParent(parents);
    }

    public void endPreviewParent()
    {
        GM.endPreviewParent();
    }

    //private void OnMouseEnter()
    //{
    //    if(status == tileState.born)
    //    {
    //        GM.startPreviewParent(parents);
    //    }
    //}
    //
    //private void OnMouseExit()
    //{
    //    if (status == tileState.born)
    //        GM.endPreviewParent();
    //}
}
