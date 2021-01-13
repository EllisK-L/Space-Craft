using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour
{
    //The reactor is left out because it's delt with seperatly
    private string[] blockTypes = { "Thruster","Cube","Reactor","Gun" };
    private GameObject[,] gridArray;
    private Dictionary<string,ArrayList> shipComponents;
    public int thrust;

    public ArrayList GetLocationsOf(GameObject[,] gridArray,string objectName) {
        ArrayList foundObjects = new ArrayList();

        //add all objects with the name to the arraylist
        for (int i = 0; i < gridArray.GetLength(0); i++) {
            for (int j = 0; j < gridArray.GetLength(1); j++) {
                if (gridArray[i, j].GetComponent<Grid_Object>().containsObject()) {
                   if(gridArray[i,j].GetComponent<Grid_Object>().getObject(false).name == objectName) {
                        foundObjects.Add(gridArray[i, j].GetComponent<Grid_Object>().getObject(false));
                    }
                }
            }
        }
        return foundObjects;

    }
    public int getThrust() {
        return thrust;
    }

    public string[] getBlockTypes() {
        return blockTypes;
    }
    public void setGridArray(GameObject[,] gridarray) {
        gridArray = gridarray;
    }
    public GameObject[,] getGridArray() {
        return gridArray;
    }
    public void setShipComponents(Dictionary<string, ArrayList> shipcomponents) {
        shipComponents = shipcomponents;
    }
    public Dictionary<string,ArrayList> getShipComponents() {
        return shipComponents;
    }
    public GameObject[] getAllOfAComponent(string compToLookFor) {
        GameObject[] listOfComps = { };
        foreach (KeyValuePair<string,ArrayList> comp in getShipComponents()) {
            if(comp.Key == compToLookFor) {
                listOfComps = new GameObject[comp.Value.Count];
                for(int i=0; i < comp.Value.Count; i++) {
                    listOfComps[i] = (GameObject)comp.Value[i];
                }
            }
        }
        return listOfComps;
    }





}
