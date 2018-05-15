using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OverworldLevel : MonoBehaviour {

    public Transform[] connections = new Transform[4];
    public int levelScene = 1;
    public bool unlocked;

	void Start () {
        unlocked = false;
        for (int i = 0; i < CarryOverInfo.carryOverInfoInstance.unlockedLevels.Count; i++)
        {
            if (CarryOverInfo.carryOverInfoInstance.unlockedLevels[i] == transform.name)
            {
                unlocked = true;
                break;
            }
        }


        if (unlocked)
        {
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);
        }
	}
	
	void Update () {
		
	}

    public void Fade()
    {

        transform.GetComponentInChildren<SpriteRenderer>().DOFade(0, 1);
    }
}
