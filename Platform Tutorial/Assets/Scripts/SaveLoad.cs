using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad{
    

    public static void Save(SaveData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/save" + data.saveNumber + ".gd");
        bf.Serialize(file, data);
        file.Close();
    }

    public static SaveData Load(int saveNumber)
    {
        if (File.Exists(Application.persistentDataPath + "/save" + saveNumber + ".gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save" + saveNumber + ".gd", FileMode.Open);
            return ((SaveData)bf.Deserialize(file));
        }
        else
            return null;
    }

}
