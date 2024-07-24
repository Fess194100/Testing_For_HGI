using UnityEngine;

public class DataResource : MonoBehaviour
{
    [SerializeField] private ResourceObject dataObject;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshFilter meshFilter;

    TotalSave totalSaveInstance;
    void Start()
    {
        totalSaveInstance = TotalSave.Instance;
        meshRenderer.material.SetColor("_BaseColor", dataObject.Color);
        meshFilter.mesh = dataObject.Mesh;
    }

    public void ObjectCollected()
    {
        if (dataObject != null)
        {
            Debug.Log("Собран ресурс: " + dataObject.ResourcesName + "Кол-во = " + dataObject.Count);
            totalSaveInstance.UdateCountResources(dataObject.ID, dataObject.Count);
        }
    }
}
