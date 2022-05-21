using UnityEngine;
using UnityEngine.UI;

public class InventorySelector : MonoBehaviour
{
    private InventoryItemChanger _itemChanger;
    [SerializeField] private GameObject[] cellObjects;

    private int _selectedCellID = -1;
    private Color _selectedCellColor = new Color(0.3113208f, 0.3113208f, 0.3113208f, 1f);
    private Color _neutralCellColor = new Color(0.1132075f, 0.1132075f, 0.1132075f, 1f);
    public int GetSelectedCellID() => _selectedCellID;

    private void Start()
    {
        _itemChanger = GetComponent<InventoryItemChanger>();
    }

    private void OnEnable()
    {
        EventBus.OnClickedInventoryCellEvent += SelectInventoryCell;
        EventBus.OnPlayerDropItemEvent += ResetInventorySelection;
    }
    private void OnDisable()
    {
        EventBus.OnClickedInventoryCellEvent -= SelectInventoryCell;
        EventBus.OnPlayerDropItemEvent -= ResetInventorySelection;
    }
    private void ResetInventorySelection()
    {
        ChangeCellColor(_selectedCellID, false);
        _selectedCellID = -1;
    }
    private void SelectInventoryCell(int id)
    {
        if (_selectedCellID == id)
        {
            ResetInventorySelection();
            ChangeCellColor(id, false);
        }
        else
        {
            InventoryData.ItemInfo _emptySlot = InventoryData.ItemInfo.None;
            InventoryData.ItemInfo _newSelectedCell = _itemChanger.GetInfoItemID(id);
            if (_selectedCellID != -1)
            {
                InventoryData.ItemInfo _selectedCellItem = _itemChanger.GetInfoItemID(_selectedCellID);
                if (_selectedCellItem != _emptySlot && _newSelectedCell == _emptySlot)
                {
                    _itemChanger.MoveItemToCell(_selectedCellID, id);
                    ResetInventorySelection();
                }
                else
                {
                    if (_newSelectedCell != _emptySlot)
                    {
                        SelectCell(id, true);
                    }
                }
            }
            else if (_newSelectedCell != _emptySlot)
            {
                SelectCell(id);
            }
        }
    }
    private void SelectCell(int id, bool resetColor = false)
    {
        if (resetColor) ChangeCellColor(_selectedCellID, false);
        _selectedCellID = id;
        ChangeCellColor(id, true);
    }

    private void ChangeCellColor(int id, bool isSelected)
    {
        if (id != -1)
        {
            cellObjects[id].GetComponent<Image>().color = isSelected ? _selectedCellColor : _neutralCellColor;
        }
    }
}
