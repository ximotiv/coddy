using UnityEngine;

public class ButtonDropItems : MonoBehaviour
{
    [SerializeField] private InventoryHandler _inventoryHandler;
    public void OnPlayerClickButton()
    {
        if(_inventoryHandler.selectedCellID != -1)
        {
            _inventoryHandler.DropItem(_inventoryHandler.selectedCellID);
        }
    }
}
