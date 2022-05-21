using UnityEngine;

public class ButtonDropItems : MonoBehaviour
{
    [SerializeField] private InventorySelector _selector;
    public void OnPlayerClickButton()
    {
        int _selectedCell = _selector.GetSelectedCellID();
        EventBus.OnClickedButtonDropItemEvent?.Invoke(_selectedCell);
    }
}
