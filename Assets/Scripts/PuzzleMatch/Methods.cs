using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Methods : MonoBehaviour
{
    public int unit;
    public int column;
    public int row;
    private Vector2 positions;
    public int foodType;
    private int targetX;
    private int targetY;
    private GameObject otherFood;
    private PuzzleMatch board;
    private Vector2 firstPosition;
    private Vector2 finalPosition;
    private Vector2 tempPosition;
    private bool moveNow;
    private bool findMatch;
    public float swipeAngule = 0;
    private RectTransform foodTransform;

    private void Start() {
        foodTransform = (RectTransform)transform;
        moveNow = false;
        findMatch = false;
        board = FindObjectOfType<PuzzleMatch>();
        string[] sPos = name.Split(',');
        positions = new Vector2(int.Parse(sPos[0]),int.Parse(sPos[1]));
        tempPosition = foodTransform.anchoredPosition;
        foodType = int.Parse(sPos[2]);
        targetX = (int) positions.x;
        targetY = (int) positions.y;
        column = targetX;
        row = targetY;
        board.printL();
    }

    private void Update() {
        targetX = column;
        targetY = row;
        positions.x = column;
        positions.y = row;
        if(moveNow){
            if(swipeAngule == 180 || swipeAngule == 0){
                //Directly set the position
                tempPosition = new Vector2(targetX * 64, positions.y * 64);
            }
            if (swipeAngule == 90 || swipeAngule == -90)
            {
                //Directly set the position
                tempPosition = new Vector2(positions.x * 64, targetY * 64);
            }
            //foodTransform.anchoredPosition = tempPosition;
            board.allFood[column, row] = this.gameObject;
            board.foodToUse[column, row] = this.foodType;
            moveNow = false;
        }
        foodTransform.anchoredPosition = Vector2.Lerp(foodTransform.anchoredPosition, tempPosition, 0.5f);
        if (findMatch){
            findMatch = false;
            GameObject foodRef = findMatches();
            if (foodRef != null) {
                Methods matchedFood = foodRef.GetComponent<Methods>();
                // Call method for food effect.
                if (matchedFood != null) StatsManager.Instance.CalcEffect(matchedFood.foodType);
            }
        }
        // Check if they exist on the logical table.
        if (board.allFood[column, row].GetInstanceID() != gameObject.GetInstanceID()) {
            Debug.Log(board.allFood[column, row].name);
            Destroy(gameObject);
            return;
        }
    }

    public void OnMouseDown() {
        firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnMouseUp() {
        finalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculeAngle();
    }

    private void CalculeAngle(){
        swipeAngule = Mathf.Atan2(finalPosition.y - firstPosition.y, finalPosition.x - firstPosition.x)* 180/ Mathf.PI;
        movePieces();
    }

    private void movePieces(){
        if (swipeAngule > -45 && swipeAngule <= 45 && column < board.width * 64)
        {
            //Right Swipe
            otherFood = board.allFood[column + unit, row];
            otherFood.GetComponent<Methods>().column -= unit;
            column += unit;
            findMatch = true;
            moveNow = true;
            swipeAngule = 180;
            otherFood.GetComponent<Methods>().moveNow = moveNow;
            otherFood.GetComponent<Methods>().swipeAngule = swipeAngule;
            otherFood.GetComponent<Methods>().findMatch = findMatch;

        } else if (swipeAngule > -45 && swipeAngule <= 135 && row < board.height * 64)
        {
            //Up Swipe
            otherFood = board.allFood[column, row + unit];
            otherFood.GetComponent<Methods>().row -= unit;
            row += unit;
            findMatch = true;
            moveNow = true;
            swipeAngule = -90;
            otherFood.GetComponent<Methods>().moveNow = moveNow;
            otherFood.GetComponent<Methods>().findMatch = findMatch;
            otherFood.GetComponent<Methods>().swipeAngule = swipeAngule;
            otherFood.GetComponent<Methods>().findMatch = findMatch;
            }else if ((swipeAngule > 135 || swipeAngule <= -135) && column > 0)
        {
            //Left Swipe
            otherFood = board.allFood[column - unit, row];
            otherFood.GetComponent<Methods>().column += unit;
            column -= unit;
            moveNow = true;
            findMatch = true;
            swipeAngule = 0;
            otherFood.GetComponent<Methods>().moveNow = moveNow;
            otherFood.GetComponent<Methods>().swipeAngule = swipeAngule;
                  otherFood.GetComponent<Methods>().findMatch = findMatch;
            }else if (swipeAngule < -45 && swipeAngule >= -135 && row > 0)
        {
            //Down Swipe
            otherFood = board.allFood[column, row - unit];
            otherFood.GetComponent<Methods>().row += unit;
            row -= unit;
            findMatch = true;
            moveNow = true;
            swipeAngule = 90;
            otherFood.GetComponent<Methods>().moveNow = moveNow;
            otherFood.GetComponent<Methods>().swipeAngule = swipeAngule;
            otherFood.GetComponent<Methods>().findMatch = findMatch;
        }
    }

      private void returnPieces()
      {
        if (swipeAngule > -45 && swipeAngule <= 45 && column < board.width * 64)
        {
                //Right Swipe
                otherFood = board.allFood[column + unit, row];
                otherFood.GetComponent<Methods>().column -= unit;
                column += unit;
                moveNow = true;
                otherFood.GetComponent<Methods>().moveNow = moveNow;
        }
        else if (swipeAngule > -45 && swipeAngule <= 135 && row < board.height * 64)
        {
                //Up Swipe
                otherFood = board.allFood[column, row + unit];
                otherFood.GetComponent<Methods>().row -= unit;
                row += unit;
                moveNow = true;
                otherFood.GetComponent<Methods>().moveNow = moveNow;
        }
        else if ((swipeAngule > 135 || swipeAngule <= -135) && column > 0)
        {
                //Left Swipe
                otherFood = board.allFood[column - unit, row];
                otherFood.GetComponent<Methods>().column += unit;
                column -= unit;
                moveNow = true;
                swipeAngule = 0;
                otherFood.GetComponent<Methods>().moveNow = moveNow;
        }
        else if (swipeAngule < -45 && swipeAngule >= -135 && row > 0)
        {
                //Down Swipe
                otherFood = board.allFood[column, row - unit];
                otherFood.GetComponent<Methods>().row += unit;
                row -= unit;
                moveNow = true;
                otherFood.GetComponent<Methods>().moveNow = moveNow;
        }
      }
    private GameObject findMatches(){
        int[,] mtx = board.foodToUse;
        bool value = false;
        GameObject referenceObject = null;

        for (int i = 0; i < board.width-2; i++)
        {
            int j = 0;
            while (j < board.height-2)
            {
                int[] explotePostions = new int[board.width + 1];
                for (int spc = 0; spc < explotePostions.Length; spc++)
                {
                    explotePostions[spc] = -1;
                }
                string where = "";
                //asdasdasd
                int c = 0;

                if(mtx[i,j] == mtx[i,j+1] && mtx[i, j] == mtx[i, j + 2]){
                    value = true;
                    where = "row";
                    explotePostions[0] = i;
                    explotePostions[1] = j;
                    explotePostions[2] = j+1;
                    explotePostions[3] = j+2;
                    bool more = false;
                    //asdasdasd
                    c = 3; 
                    for (int k = 4; k < explotePostions.Length; k++)
                    {
                        for (int l = j+3; l < board.height; l++)
                        {
                            if(mtx[i,j] == mtx[i,l]){
                                explotePostions[k] = l;
                                c++;
                            }else{
                                referenceObject = board.allFood[i, j];
                                j = l;
                                exploteFood(explotePostions,where);

                                more = true;
                                Debug.Log("Match find at "+c);
                            }
                        }
                        if(more){
                            break;
                        }
                    }
                    exploteFood(explotePostions,where);
                    //Debug.Log("Match find at " + c);
                }
                else if (mtx[i, j] == mtx[i + 1, j] && mtx[i, j] == mtx[i + 2, j])
                {
                    value = true;
                    where = "column";
                    explotePostions[0] = j;
                    explotePostions[1] = i;
                    explotePostions[2] = i + 1;
                    explotePostions[3] = i + 2;
                    bool more = false;
                    c = 3;
                    for (int k = 4; k < explotePostions.Length; k++)
                    {
                        for (int l = i + 3; l < board.height; l++)
                        {
                            if (mtx[i, j] == mtx[l, j])
                            {
                                explotePostions[k] = l;
                                c++;
                            }
                            else
                            {
                                referenceObject = board.allFood[i, j];
                                i = l;
                                exploteFood(explotePostions, where);
                                Debug.Log("Match find at "+c);
                                more = true;
                            }
                            break;
                        }
                        if (more)
                        {
                            break;
                        }
                    }
                    
                    //Debug.Log("Match find at " + c);
                }
                j++;
            }
        }
        if (value){
            board.foodRestore();
        }
        return referenceObject;
    }
    private void exploteFood(int[] exploteFood, string where){
        if(where == "row"){
            for (int i = 1; i < exploteFood.Length; i++)
            {
                if(exploteFood[i] != -1){
                    board.foodToUse[exploteFood[0], exploteFood[i]] = -1;
                    GameObject matchedFood = board.allFood[exploteFood[0], exploteFood[i]];
                    board.InstantiateExplodeAt(matchedFood.transform);
                }
            }
        }else{
            for (int i = 1; exploteFood[i] != -1; i++)
            {
                board.foodToUse[exploteFood[i], exploteFood[0]] = -1;
                GameObject matchedFood = board.allFood[exploteFood[i], exploteFood[0]];
                board.InstantiateExplodeAt(matchedFood.transform);
            }
        }
    }


}
