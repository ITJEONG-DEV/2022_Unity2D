using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapEvent : MonoBehaviour
{
    public MainUI mainUI;

    public Tilemap farmGround;
    public Tilemap crops;
    // Start is called before the first frame update
    void Start()
    {
        mainUI = GameObject.Find("MainUI").GetComponent<MainUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var tpos = farmGround.WorldToCell(worldPoint);
            var tile = farmGround.GetTile(tpos);

            if (tile)
            {
                var pos = farmGround.GetCellCenterWorld(tpos);
                mainUI.SendMessage("OnClickFarmGround", pos);
                Debug.Log($"world: ({worldPoint.x}, {worldPoint.y}), tpos: ({tpos.x},{tpos.y}), wpos: ({pos.x}, {pos.y})");
            }

        }
    }

    private void OnMouseOver()
    {
        try
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction*10, Color.blue, 3.5f);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, Vector3.zero);

            if (this.farmGround = hit.transform.GetComponent<Tilemap>())
            {
                this.farmGround.RefreshAllTiles();
                int x, y;
                x = this.farmGround.WorldToCell(ray.origin).x;
                y = this.farmGround.WorldToCell(ray.origin).y;

                Debug.Log(x + ", " + y);
            }
        }
        catch(Exception ex)
        {
            Debug.LogException(ex);
        }
    }
}
