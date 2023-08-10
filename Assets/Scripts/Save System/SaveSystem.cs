using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    static string path = Application.persistentDataPath + "/savefile.save";
   public static void SaveGame(GameManager gameManager) {
    BinaryFormatter formatter = new BinaryFormatter();
    FileStream stream = new FileStream(path,FileMode.Create);

    SaveData saveData = new SaveData(gameManager);

    formatter.Serialize(stream, saveData);
    stream.Close();
   }

   public static SaveData LoadGame() {
    if(File.Exists(path)) 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path,FileMode.Open);
        SaveData saveData = formatter.Deserialize(stream) as SaveData;
        stream.Close();

        return saveData;
    } else {
        Debug.Log("No Save File");
        return null;
    }
   }
}
