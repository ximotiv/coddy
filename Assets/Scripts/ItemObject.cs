using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private InventoryHandler.ItemInfo _itemType;
    [SerializeField] private int _itemAmount;
    [SerializeField] private float _itemWeight;
    private void DestroyItemObject()
    {
        Destroy(this);
    }
}
