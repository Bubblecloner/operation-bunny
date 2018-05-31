using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreController : MonoBehaviour {

    public Text price;
    public GameObject[] description;
    public GameObject[] listedItems;
    public GameObject cursor;
    public Text coinText;
    private int selectedItem = 0;
    private int holdingDirection = 0;
    private int money;

	void Start () {
        Move(0);
	}
	
	void Update ()
    {
        coinText.text = money.ToString();

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
        else if (Input.GetButtonDown("Cancel"))
        {

        }
        //add exit method
        
    }

    private void Move(int target)
    {
        if(target < listedItems.Length && target >= 0)
        {
            description[selectedItem].SetActive(false);
            description[target].SetActive(true);

            selectedItem = target;

            price.text = listedItems[selectedItem].GetComponent<StoreItem>().Price.ToString();
        }
    }

    private void PurchaseItem(int target)
    {
        if (!listedItems[target].GetComponent<StoreItem>().PurchaseItem())
        {
            //runs if player does not have enough money
        }
    }

    private void Exit()
    {
        SceneManager.LoadScene(6);
    }
}
