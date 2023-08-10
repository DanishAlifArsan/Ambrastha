using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isTutorial = true;
    public int level;

    public void SaveGame() {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame() {
        SaveData saveData = SaveSystem.LoadGame();
        if (saveData != null)
        {
            isTutorial = saveData.isTutorial;
            level = saveData.level;
        }   
    }
}
