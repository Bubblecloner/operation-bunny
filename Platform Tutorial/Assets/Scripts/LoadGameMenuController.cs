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

    public void Back()
    {
        SceneManager.LoadScene(backButtonScene);
    }

    public void LoadSave(int saveNumber)
    {
        CarryOverInfo.carryOverInfoInstance.saveNumber = saveNumber;
        CarryOverInfo.carryOverInfoInstance.Load();

        SceneManager.LoadScene(5);
    }
}
