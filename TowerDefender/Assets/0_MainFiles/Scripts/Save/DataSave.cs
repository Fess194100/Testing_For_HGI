using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class DataSave
{
    [SerializeField]
    public int[] countResources = new int[2];
    [SerializeField]
    public string playerName;

    public void GetData()
    {
        if (File.Exists(Application.persistentDataPath + "/saveData.dat"))
        {
            BinaryFormatter bf = new();
            FileStream file = File.Open(Application.persistentDataPath + "/saveData.dat", FileMode.Open);
            DataSave save = (DataSave)bf.Deserialize(file);
            file.Close();

            countResources = save.countResources;
            playerName = save.playerName;

        }
        else
        {
            ResetData();
        }
    }

    public void SetData()
    {
        BinaryFormatter bf = new();
        FileStream file = File.Create(Application.persistentDataPath + "/saveData.dat");

        bf.Serialize(file, this);
        file.Close();
    }

    public void ResetData()
    {
        countResources = new int [2];
        playerName = "Player1";

        SetData();
    }
}