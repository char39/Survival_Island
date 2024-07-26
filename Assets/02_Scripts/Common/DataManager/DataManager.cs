using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataInfo;

public class DataManager : MonoBehaviour
{
    [SerializeField] string dataPath;
    public void Initialized()
    {
        dataPath = Application.persistentDataPath + "/Data.dat";
    }

    public void Save(GameData gameData)
    {
        BinaryFormatter binaryFormatter = new();
        FileStream fileStream = File.Create(dataPath);

        GameData data = new();
        data.killCounts = gameData.killCounts;
        data.hp = gameData.hp;
        data.speed = gameData.speed;
        data.damage = gameData.damage;
        data.equipItem = gameData.equipItem;

        binaryFormatter.Serialize(fileStream, data);
        fileStream.Close();
    }

    public GameData Load()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter binaryFormatter = new();    
            FileStream fileStream = File.Open(dataPath, FileMode.Open);
            GameData data = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return data;
        }
        else
        {
            GameData data = new();
            return data;
        }
    }
    
}
