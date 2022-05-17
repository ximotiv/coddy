using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private InventoryHandler _inventoryHandler;
    public int inventoryCellID;

    public void OnPlayerClickCell()
    {
        _inventoryHandler.SelectInventoryCell(inventoryCellID);
    }
}
