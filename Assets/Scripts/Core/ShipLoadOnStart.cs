using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLoadOnStart : MonoBehaviour
{
    public GameObject gameHelper;
    public GameObject inventory;
    void Start()
    {
        gameHelper.GetComponent<Save_Load_Ship>().loadShip("temp");
        this.GetComponent<Game_Start>().startGame(this.gameObject.transform.GetChild(0).GetComponent<Grid_Script>().getGridArray());
        inventory.SetActive(false);
    }
}
