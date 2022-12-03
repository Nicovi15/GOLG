using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField]
    LayerMask UniteLayer;

    [SerializeField]
    LayerMask TileLayer;

    public Unite currentGrab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100.0f, UniteLayer);

            if (hit.collider != null)
            {
                currentGrab = hit.collider.GetComponent<Unite>();
                currentGrab.gotGrabbed();
            }
        }

        if (Input.GetMouseButtonUp(0) && currentGrab != null)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 100.0f, TileLayer);

            if (hit.collider != null)
            {
                Tile t = hit.collider.GetComponent<Tile>();
                if (t.isFree)
                {
                    currentGrab.currentTile.removeUnite();
                    currentGrab.currentTile = t;
                    currentGrab.currentTile.setCurrentUnite(currentGrab);
                    currentGrab.transform.position = t.transform.position;
                    currentGrab.transform.SetParent(t.transform);
                    currentGrab.gotDropped();
                    currentGrab = null;
                }
                else
                {
                    currentGrab.transform.position = currentGrab.currentTile.transform.position;
                    currentGrab.gotDropped();
                    currentGrab = null;
                }
            }
            else
            {
                currentGrab.transform.position = currentGrab.currentTile.transform.position;
                currentGrab.gotDropped();
                currentGrab = null;
            }
        }

        if (currentGrab != null)
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            currentGrab.transform.position = newPos;
        }
        
    }
}
