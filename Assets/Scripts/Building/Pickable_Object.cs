using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Object : MonoBehaviour
{
    public bool source = true;
    public int quantitiy;
    public GameObject originalSource;
    bool destroyed = false;
    private string orientation = "up";
    public bool rotatable;

    public string type;
    private void Start() {
        
    }

    public void setSource(bool amISource) {
        source = amISource;
    }
    public bool isSource() {
        return source;
    }
    public bool rotateObject(bool right) {
        //if the object can't be rotated
        if(rotatable == false) {
            return false;
        }
        if (right == true) {
            this.transform.Rotate(new Vector3(0, -90, 0));
            if(orientation == "up") {
                orientation = "right";
            }
            else if (orientation == "right") {
                orientation = "down";
            }
            else if(orientation == "down") {
                orientation = "left";
            }
            else {
                orientation = "up";
            }
        }
        else {
            this.transform.Rotate(new Vector3(0, 90, 0));
            if (orientation == "up") {
                orientation = "left";
            }
            else if (orientation == "left") {
                orientation = "down";
            }
            else if (orientation == "down") {
                orientation = "right";
            }
            else {
                orientation = "up";
            }
        }
        //Debug.Log(orientation);
        return true;
    }
    public string getOrientation() {
        return orientation;
    }
    public bool available() {
        if(quantitiy > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void changeQuantity(int changeInQuant) {
        quantitiy += changeInQuant;
    }
    public void setOriginalSource(GameObject setSource) {
        source = false;
        originalSource = setSource;
    }
    public GameObject getSource() {
        return originalSource;
    }
    public bool isDestroyed() {
        if(destroyed == false) {
            return false;
        }
        else {
            return true;
        }
    }
    public string getType() {
        return type;
    }
}
