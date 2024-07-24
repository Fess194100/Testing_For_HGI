using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "CollectedObject/ItemObject")]
public class ItemObject : ScriptableObject
{
    [SerializeField] private string itemName, description;
    [SerializeField] private int count;

    public string ItemName => itemName;
    public string Description => description;
    public int Count => count;
}
