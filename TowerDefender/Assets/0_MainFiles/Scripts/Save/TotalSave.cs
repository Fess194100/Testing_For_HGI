using UnityEngine;

public class TotalSave : MonoBehaviour
{
    private static TotalSave instance;

    public DataSave saveData = new();

    public static TotalSave Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TotalSave>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        saveData.GetData();
    }

    public void SaveData() => saveData.SetData();

    public void UdateCountResources (int id, int count)
    {
        if (id <= saveData.countResources.Length - 1)
        {
            saveData.countResources[id] += count;
            SaveData();
        }        
    }
}