using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private int inventoryCellID;
    public void OnPlayerClickCell()
    {
        EventBus.OnClickedInventoryCellEvent?.Invoke(inventoryCellID);
    }
}
