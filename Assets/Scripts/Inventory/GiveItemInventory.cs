using UnityEngine;

public class GiveItemInventory : MonoBehaviour
{
    private InventoryHandler _inventoryHandler;
    private void Start()
    {
        _inventoryHandler = GetComponent<InventoryHandler>();
    }
}
