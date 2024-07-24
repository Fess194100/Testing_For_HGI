using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "CollectedObject/ResourceObject")]
public class ResourceObject : ScriptableObject
{
    [SerializeField] private string resourceName, description;
    [SerializeField] private int id, count;
    [SerializeField] private Color color;
    [SerializeField] private Mesh mesh;

    public string ResourcesName => resourceName;
    public string Description => description;
    public int Count => count;

    public int ID => id;
    public Color Color => color;
    public Mesh Mesh => mesh;

}
