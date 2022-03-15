using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : TileBase
{
    
    [SerializeField]
    private Sprite[] waterSprites;
    public Tilemap tileMap;

    //Test 
    private GameObject player;

    //A preview of the tile
    [SerializeField]
    private Sprite preview;

    
    private void Awake()
    {
        
    }

    private void Test()
    {
        
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        return base.StartUp(position, tilemap, go);
    }

    /// <summary>
    /// Refreshes this tile when something changes
    /// </summary>
    /// <param name="position">The tiles position in the grid</param>
    /// <param name="tilemap">A reference to the tilemap that this tile belongs to.</param>
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {

        for (int y = -1; y <= 1; y++) //Runs through all the tile's neighbours 
        {
            for (int x = -1; x <= 1; x++)
            {
                //We store the position of the neighbour 
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);
            }
        }
    }


    /// <summary>
    /// Changes the tiles sprite to the correct sprites based on the situation
    /// </summary>
    /// <param name="location">The location of this sprite</param>
    /// <param name="tilemap">A reference to the tilemap, that this tile belongs to</param>
    /// <param name="tileData">A reference to the actual object, that this tile belongs to</param>
    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {

        base.GetTileData(location, tilemap, ref tileData);

        string composition = string.Empty;//Makes an empty string as compostion, we need this so that we change the sprite

        for (int x = -1; x <= 1; x++)//Runs through all neighbours 
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x != 0 || y != 0) //Makes sure that we aren't checking our self
                {
                    //If the value is a watertile
                    if (HasWater(tilemap, new Vector3Int(location.x + x, location.y + y, location.z)))
                    {
                        composition += 'W';
                    }
                    else
                    {
                        composition += 'E';
                    }


                }
            }
        }

        ///Selects a random tile for the water
        int randomVal = Random.Range(0, 100);

        if (randomVal < 15)
        {
            tileData.sprite = waterSprites[46];
        }
        else if (randomVal >= 15 && randomVal < 35)
        {

            tileData.sprite = waterSprites[48];
        }
        else
        {
            tileData.sprite = waterSprites[47];
        }
    }


    private bool HasWater(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }


}