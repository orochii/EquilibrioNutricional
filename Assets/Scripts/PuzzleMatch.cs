using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMatch : MonoBehaviour
{
    public int width;
    public int spaceW;
    public int height;
    public int spaceH;
    public GameObject tilePrefab;
    private GameObject[,] allTiles;
    // Start is called before the first frame update
    void Start()
    {
        allTiles = new GameObject[width, height];
        GenerateBoard();
    }

    private void GenerateBoard(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i*spaceW, j*spaceH);
                GameObject newObject = Instantiate(tilePrefab, tempPosition, Quaternion.identity);
                newObject.transform.parent = transform;
            }
        }
    }

}