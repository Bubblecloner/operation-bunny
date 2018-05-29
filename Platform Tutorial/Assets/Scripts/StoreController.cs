using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreController : MonoBehaviour {

    public GameObject[] listedItems;
    public GameObject cursor;
    private int selectedItem = 0;
    private int holdingDirection = 0;

	void Start () {
		
	}
	
	void Update () {

        cursor.transform.position = (Vector2)listedItems[selectedItem].transform.position + new Vector2(0,100);

        if (Input.GetAxisRaw("Vertical") == -1 && holdingDirection != -1)
        {
            Move(selectedItem + 1);
            holdingDirection = -1;
        }
        else if (Input.GetAxisRaw("Vertical") == 1 && holdingDirection != 1)
        {
            Move(selectedItem - 1);
            holdingDirection = 1;
        }
        else if(Input.GetAxisRaw("Vertical") == 0)
            holdingDirection = 0;
        else if (Input.GetButtonDown("Submit"))
        {
            PurchaseItem(selectedItem);
        }
        //add exit method
    }

    private void Move(int target)
    {
        if(target < listedItems.Length && target >= 0)
        {
            selectedItem = target;
        }
    }

    private void PurchaseItem(int target)
    {
        if (!listedItems[target].GetComponent<StoreItem>().PurchaseItem())
        {
            //runs if player does not have enough money
        }
    }
}
