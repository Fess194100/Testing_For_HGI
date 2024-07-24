using UnityEngine;

namespace UltimateCC
{    public class CollectingObjects : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Resources"))
            {
                DataResource resource = other.GetComponent<DataResource>();
                if (resource != null)
                {
                    resource.ObjectCollected();
                }
            }
            else if (other.CompareTag("Items"))
            {
                ItemObject item = other.GetComponent<ItemObject>();
                if (item != null)
                {                    
                    Debug.Log("������ �������: " + item.ItemName + "���-�� = " + item.Count);
                    Destroy(other.gameObject);
                }
            }
        }
    }
}

