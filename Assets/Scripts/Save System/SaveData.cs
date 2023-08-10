using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public bool isTutorial;
    public int level;

    public SaveData(GameManager gameManager)
    {
        this.isTutorial = gameManager.isTutorial;
        this.level = gameManager.level;
    }
}
