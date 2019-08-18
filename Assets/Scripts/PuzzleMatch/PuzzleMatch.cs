using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleMatch : MonoBehaviour
{
    public int width;
    public int spaceW;
    public int height;
    public int spaceH;
    public GameObject[,] allFood;
    public int[,] foodToUse;
    public GameObject[] foods;

      // Start is called before the first frame update
      void Start()
    {
        allFood = new GameObject[width, height];
        foodToUse = new int[width,height];
        createLogicBoard();
        GenerateBoard();
    }

    private void createLogicBoard(){
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                foodToUse[i,j] = Random.Range(0, foods.Length);
        initLogicBoard();
    }
    private void initLogicBoard(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i+2 <= width-1)
                {
                    if (foodToUse[i,j] == foodToUse[i+1, j] && foodToUse[i, j] == foodToUse[i+2, j])
                    {
                        foodToUse[i,j] = Random.Range(0, foods.Length);    
                        foodToUse[i+1,j] = Random.Range(0, foods.Length);    
                        foodToUse[i+2,j] = Random.Range(0, foods.Length);
                        initLogicBoard();   
                    }
                }
                if (j+2 <= height-1)
                {
                    if (foodToUse[i, j] == foodToUse[i, j+1] && foodToUse[i, j] == foodToUse[i, j+2])
                    {
                        foodToUse[i, j] = Random.Range(0, foods.Length);
                        foodToUse[i, j + 1] = Random.Range(0, foods.Length);
                        foodToUse[i, j + 2] = Random.Range(0, foods.Length);
                        initLogicBoard();
                    }
                }
            }
        }
    }

    private void GenerateBoard(){
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i*spaceW, j*spaceH);
                GameObject food = Instantiate(foods[foodToUse[i,j]], transform);
                RectTransform foodTransform = (RectTransform)food.transform;
                foodTransform.anchoredPosition = tempPosition;
                food.name = i + "," + j+","+foodToUse[i,j];
                allFood[i,j] = food;
            }
        }
    }


      public void printL()
      {
            Debug.Log("fooods");
            for (int i = 0; i < width; i++)
          {
                string aS = "";
                for (int j = 0; j < height; j++)
                {
                    aS += foodToUse[i,j].ToString() + " ";
                }
                Debug.Log(aS);
            }
      }

}