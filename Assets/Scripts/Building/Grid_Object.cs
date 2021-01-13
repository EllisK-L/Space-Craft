using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Object : MonoBehaviour
{
    public GameObject containedObject;
    private int cordX;
    private int cordY;
  
    public void setX(int xCord) {
        cordX = xCord;
    }
    public void setY(int yCord) {
        cordY = yCord;
    }

    public int getX() {
        return (int) transform.position.x;
    }
    public int getY() {
        return (int)transform.position.z;
    }

    public void setObject(GameObject objectToSet) {
        containedObject = objectToSet;
    }
    public GameObject getObject(bool selfDestruct) {
        if (selfDestruct == true) {
            GameObject temp = containedObject;
            setObject(null);
            return temp;
        }
        else {
            return containedObject;
        }
        
    }
    public GameObject getObject() {
        return containedObject;
    }
    public bool containsObject() {
        if(containedObject == null) {
            return false;
        }
        else {
            return true;
        }
    }


}
