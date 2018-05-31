using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreItem : MonoBehaviour {

    public int upgradeId;
    public int[] price;
    private int upgradeLevel;

	void Start ()
    {
        upgradeLevel = CarryOverInfo.carryOverInfoInstance.upgrades[upgradeId];
	}

    public bool PurchaseItem()
    {
        if (price.Length > upgradeLevel + 1 && CarryOverInfo.carryOverInfoInstance.money > price[upgradeLevel + 1])
        {
            upgradeLevel++;
            CarryOverInfo.carryOverInfoInstance.upgrades[upgradeId] = upgradeLevel;
            CarryOverInfo.carryOverInfoInstance.money -= price[upgradeLevel];
            CarryOverInfo.carryOverInfoInstance.Save();

            return true;
        }
        else
            return false;
    }

    public int Price
    {
        get
        {
            return price[upgradeLevel + 1];
        }
    }
}
