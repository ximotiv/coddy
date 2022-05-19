using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] private int inventoryCellID;
    public void OnPlayerClickCell()
    {
        EventBus.OnClickedInventoryCell?.Invoke(inventoryCellID);
    }
}
