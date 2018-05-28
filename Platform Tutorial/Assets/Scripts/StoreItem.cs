using System.Collections;
using System.Collections.Generic;
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
            CarryOverInfo.carryOverInfoInstance.upgrades[upgradeId] = upgradeLevel + 1;
            CarryOverInfo.carryOverInfoInstance.money -= price[upgradeLevel + 1];
            CarryOverInfo.carryOverInfoInstance.Save();

            return true;
        }
        else
            return false;
    }
}
