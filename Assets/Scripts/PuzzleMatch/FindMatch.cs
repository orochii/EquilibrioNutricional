using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatch : MonoBehaviour
{
    private PuzzleMatch board;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<PuzzleMatch>();

    }

    // Update is called once per frame
    void Update()
    {
        GameObject foodRef = findMatches();
        if (foodRef != null)
        {
            Methods matchedFood = foodRef.GetComponent<Methods>();
            // Call method for food effect.
            if (matchedFood != null) StatsManager.Instance.CalcEffect(matchedFood);
        }
            board.foodRestore();

      }


      private GameObject findMatches()
      {
            GameObject[,] mtx = board.allFood;
            bool value = false;
            GameObject referenceObject = null;

            for (int i = 0; i < board.width; i++)
            {
                  int j = 0;
                  while (j < board.height)
                  {
                        int[] explotePostions = new int[board.width + 1];
                        for (int spc = 0; spc < explotePostions.Length; spc++)
                        {
                              explotePostions[spc] = -1;
                        }
                        string where = "";
                        //asdasdasd
                        int c = 0;
                        if (j + 2 < board.height)
                        {

                              if (mtx[i, j].GetComponent<Methods>().foodType == mtx[i, j + 1].GetComponent<Methods>().foodType &&
                              mtx[i, j].GetComponent<Methods>().foodType == mtx[i, j + 2].GetComponent<Methods>().foodType)
                              {
                                    value = true;
                                    where = "row";
                                    explotePostions[0] = i;
                                    explotePostions[1] = j;
                                    explotePostions[2] = j + 1;
                                    explotePostions[3] = j + 2;
                                    c = 3;
                                    for (int k = 4; k < explotePostions.Length; k++)
                                    {
                                          for (int l = j + 3; l < board.height; l++)
                                          {
                                                if (mtx[i, j].GetComponent<Methods>().foodType == mtx[i, l].GetComponent<Methods>().foodType)
                                                {
                                                      explotePostions[k] = l;
                                                      c++;
                                                }
                                                else
                                                {
                                                      j = l;
                                                }
                                          }
                                          break;
                                    }
                                    Debug.Log("Match find at row" + c);
                                    referenceObject = Instantiate(board.allFood[i, j]);
                                    exploteFood(explotePostions, where);
                              }
                        }
                        if (i + 2 < board.width)
                        {
                              if (mtx[i, j].GetComponent<Methods>().foodType == mtx[i + 1, j].GetComponent<Methods>().foodType &&
                              mtx[i, j].GetComponent<Methods>().foodType == mtx[i + 2, j].GetComponent<Methods>().foodType)
                              {
                                    value = true;
                                    where = "column";
                                    explotePostions[0] = j;
                                    explotePostions[1] = i;
                                    explotePostions[2] = i + 1;
                                    explotePostions[3] = i + 2;
                                    c = 3;
                                    for (int k = 4; k < explotePostions.Length; k++)
                                    {
                                          for (int l = i + 3; l < board.height; l++)
                                          {
                                                if (mtx[i, j].GetComponent<Methods>().foodType == mtx[l, j].GetComponent<Methods>().foodType)
                                                {
                                                      explotePostions[k] = l;
                                                      c++;
                                                }
                                                else
                                                {
                                                      i = l;
                                                      break;
                                                }
                                          }
                                          break;
                                    }
                                    Debug.Log("Match find at column" + c);
                                    referenceObject = Instantiate(board.allFood[i, j]);
                                    exploteFood(explotePostions, where);
                              }
                        }
                        j++;
                  }
                  if (value)
                  {
                        break;
                  }
            }
            //findMatch = value;
            return referenceObject;
      }

      private void exploteFood(int[] exploteFood, string where)
      {
            if (where == "row")
            {
                  for (int i = 1; i < exploteFood.Length; i++)
                  {
                        if (exploteFood[i] != -1)
                        {
                            board.allFood[exploteFood[0], exploteFood[i]].GetComponent<Methods>().foodType = -1;
                            GameObject matchedFood = board.allFood[exploteFood[0], exploteFood[i]];
                            board.InstantiateExplodeAt(matchedFood.transform);
                        }
                  }
            }
            else
            {
                  for (int i = 1; exploteFood[i] != -1; i++)
                  {
                        board.allFood[exploteFood[i], exploteFood[0]].GetComponent<Methods>().foodType = -1;
                        GameObject matchedFood = board.allFood[exploteFood[i], exploteFood[0]];
                        board.InstantiateExplodeAt(matchedFood.transform);
                  }
            }
      }


}
