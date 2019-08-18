using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedFood : MonoBehaviour
{
    public int size;
    public int spaceH;
    public int distanceX;
    public int distanceY;
    public GameObject tilePrefab;
    private GameObject[] selectionsTiles;
    private GameObject[] selectionsFoods;

      // Start is called before the first frame update
      void Start()
    {
        selectionsTiles = new GameObject[size];
        selectionsFoods = new GameObject[size];
        GenerateBoard();
    }

    private void Update() {
        
    }

    private void GenerateBoard(){
        for (int i = 0; i < size; i++)
        {
            Vector2 tempPosition = new Vector2(distanceX, i*spaceH + distanceY);
            GameObject newObject = Instantiate(tilePrefab, tempPosition, Quaternion.identity);
            newObject.transform.parent = transform;
            newObject.name = "Selection " + i ;
        }
    }

}