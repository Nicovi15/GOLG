using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    int width, hight;

    [SerializeField]
    GameObject prefabTile;

    Tile[,] tiles;

    // Start is called before the first frame update
    void Start()
    {
        tiles = new Tile[width, hight];
        for (int i = 0; i < width; i++)
            for (int j = 0; j < hight; j++)
            {
                tiles[i, j] = Instantiate(prefabTile, transform).GetComponent<Tile>();
                tiles[i, j].transform.localPosition = new Vector2(i * 0.64f, j * 0.64f);
                tiles[i, j].init(i,j);
            } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
