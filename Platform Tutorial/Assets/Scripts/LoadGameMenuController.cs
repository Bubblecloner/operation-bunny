using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameMenuController : MonoBehaviour {

    public GameObject CarryOverInfoPrefab;
    public int backButtonScene;

    private void Start()
    {

    }

    public void LoadSave1()
    {
        LoadSave(1);
    }

    public void LoadSave2()
    {
        LoadSave(2);
    }

    public void Back()
    {
        SceneManager.LoadScene(backButtonScene);
    }

    private void LoadSave(int saveNumbers)
    {
        CarryOverInfo.carryOverInfoInstance.saveNumber = saveNumbers;
        CarryOverInfo.carryOverInfoInstance.Load();

        SceneManager.LoadScene(6);
    }
}
