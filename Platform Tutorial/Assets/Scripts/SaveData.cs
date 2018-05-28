using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData{

    public int saveNumber = -1;
    public int money;
    public float choosenLevelX;
    public float choosenLevelY;
    public int[] pots;
    public int[] upgrades = new int[3];
    public List<string> unlockedLevels;

}
