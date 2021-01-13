using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Script : MonoBehaviour
{
    public GameObject gridCube;
    int gridX = 12;
    int gridZ = 12;
    GameObject[,] gridArray;
    public GameObject Game_Helper;
    // Start is called before the first frame update
    void Start()
    {
        
        gridArray = new GameObject[gridZ,gridX];
        for (int i = (gridZ/2)*-1; i < gridZ/2; i++) {
            for(int j = (gridX / 2) * -1; j < gridX / 2; j++) {
                GameObject newGridCube = Instantiate(gridCube, new Vector3(i, 2, j), transform.rotation);
                newGridCube.name = "Grid_Cube:" + (i+(gridZ/2)).ToString() + "," + (j+(gridX/2)).ToString();
                gridArray[i+(gridZ/2), j + (gridX/2)] = newGridCube;
                //Debug.Log(gridArray[i + (gridZ / 2), j + (gridX / 2)]);


            }
        }
        //enableGrid();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            Game_Helper.GetComponent<Game_Start>().startGame(gridArray);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            moveShip("left");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            moveShip("right");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            moveShip("up");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            moveShip("down");
        }
    }

    public bool moveShip(string direction) {

        //We are justing goint to temp put this in a try catch if the player tries to move the ship out of the grid

       //This is a dirty way iof doing it. It's long and repetitive
        if (direction == "left") {
            for (int i = 0; i < gridZ; i++) {
                for (int j = 0; j < gridX; j++) {
                    if(gridArray[i, j].GetComponent<Grid_Object>().containsObject()) {
                        if (i == 0) {
                            return false;
                        }
                        //We will now try and move this to the next object to the left
                        gridArray[i, j].GetComponent<Grid_Object>().getObject().transform.position = gridArray[i, j].GetComponent<Grid_Object>().getObject().transform.position + (new Vector3(-1,0,0));
                        gridArray[i-1, j].GetComponent<Grid_Object>().setObject(gridArray[i, j].GetComponent<Grid_Object>().getObject());
                        gridArray[i, j].GetComponent<Grid_Object>().setObject(null);

                    }
                        
                }
            }
        }
        else if (direction == "right") {
            for (int i = gridZ -1; i >= 0; i--) {
                for (int j = 0; j < gridX; j++) {
                    if (gridArray[i, j].GetComponent<Grid_Object>().containsObject()) {
                        if (i == gridZ-1) {
                            return false;
                        }
                        //We will now try and move this to the next object to the left
                        gridArray[i, j].GetComponent<Grid_Object>().getObject().transform.position = gridArray[i, j].GetComponent<Grid_Object>().getObject().transform.position + (new Vector3(1, 0, 0));
                        gridArray[i + 1, j].GetComponent<Grid_Object>().setObject(gridArray[i, j].GetComponent<Grid_Object>().getObject());
                        gridArray[i, j].GetComponent<Grid_Object>().setObject(null);

                    }

                }
            }
        }
        else if (direction == "down") {
            for (int i = 0; i < gridX; i++) {
                for (int j = 0; j < gridZ; j++) {
                    if (gridArray[j, i].GetComponent<Grid_Object>().containsObject()) {
                        if (i == 0) {
                            return false;
                        }
                        //We will now try and move this to the next object to the left
                        gridArray[j, i].GetComponent<Grid_Object>().getObject().transform.position = gridArray[j, i].GetComponent<Grid_Object>().getObject().transform.position + (new Vector3(0, 0, -1));
                        gridArray[j, i-1].GetComponent<Grid_Object>().setObject(gridArray[j, i].GetComponent<Grid_Object>().getObject());
                        gridArray[j, i].GetComponent<Grid_Object>().setObject(null);

                    }

                }
            }
        }
        else if (direction == "up") {
            for (int i = gridX-1; i >= 0; i--) {
                for (int j = 0; j < gridZ; j++) {
                    if (gridArray[j, i].GetComponent<Grid_Object>().containsObject()) {
                        if (i == gridX-1) {
                            return false;
                        }
                        //We will now try and move this to the next object to the left
                        gridArray[j, i].GetComponent<Grid_Object>().getObject().transform.position = gridArray[j, i].GetComponent<Grid_Object>().getObject().transform.position + (new Vector3(0, 0, 1));
                        gridArray[j, i + 1].GetComponent<Grid_Object>().setObject(gridArray[j, i].GetComponent<Grid_Object>().getObject());
                        gridArray[j, i].GetComponent<Grid_Object>().setObject(null);

                    }

                }
            }
        }
        return true;
    }

    public bool disableGrid() {
        /*for(int i = 0; i < gridX; i++) {
            for (int j = 0; j < gridZ; j++) {
                gridArray[i,j].GetComponent<BoxCollider>().enabled = false;
                gridArray[i, j].GetComponent<Renderer>().enabled = false;
                Debug.Log("Disable");
            }
        }*/
        return true;
    }
    public bool enableGrid() {
        for (int i = 0; i < gridZ; i++) {
            for (int j = 0; j < gridX; j++) {
                //gridArray[i,j].GetComponent<BoxCollider>().enabled = true;
                gridArray[i, j].GetComponent<Renderer>().enabled = true;
            }
        }
        return true;
    }
    public GameObject[,] getGridArray() {
        return gridArray;
    }

    public void clearShip() {
        for (int i = 0; i < gridZ; i++) {
            for (int j = 0; j < gridX; j++) {
                Grid_Object currentGridObject = gridArray[i, j].GetComponent<Grid_Object>();
                if (currentGridObject.containsObject()) {
                    currentGridObject.getObject().GetComponent<Pickable_Object>().getSource().GetComponent<Pickable_Object>().changeQuantity(1); 
                    Destroy(currentGridObject.getObject(true));
                }
            }
        }
    }
}
