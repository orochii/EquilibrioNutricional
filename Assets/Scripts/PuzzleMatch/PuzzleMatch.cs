using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMatch : MonoBehaviour
{
    public int width;
    public int spaceW;
    public int height;
    public int spaceH;

    public int distanceX;
    public int distanceY;
    public GameObject tilePrefab;
    private GameObject[,] allTiles;
    private GameObject[,] allFood;
    public GameObject[] foods;

      // Start is called before the first frame update
      void Start()
    {
        allTiles = new GameObject[width, height];
        allFood = new GameObject[width, height];
        GenerateBoard();
    }

      private void GenerateBoard(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i*spaceW + distanceX, j*spaceH + distanceY);
                GameObject newObject = Instantiate(tilePrefab, transform);
                RectTransform tileTransform = (RectTransform) newObject.transform;
                tileTransform.anchoredPosition = tempPosition;
                newObject.name = "Tile(" + i + ", " + j + ")";
                int foodToUse = Random.Range(0, foods.Length);
                GameObject food = Instantiate(foods[foodToUse], transform);
                //food.transform.parent = transform;
                RectTransform foodTransform = (RectTransform)food.transform;
                foodTransform.anchoredPosition = tempPosition;
                food.name = "Food(" + i + ", " + j + ")";
                allFood[i,j] = food;
            }
        }
    }

}