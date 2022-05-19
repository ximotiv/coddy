using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    private const int MAX_INVENTORY_CELLS = 8;
    [SerializeField] private int[] _cellItemAmount;
    [SerializeField] private float[] _cellItemWeight;

    public ItemInfo GetInfoItemID(int id) => _cellItemID[id];
    public int GetInfoItemAmount(int id) => _cellItemAmount[id];
    public float GetInfoItemWeight(int id) => _cellItemWeight[id];
    public int GetInfoMaxCells() => MAX_INVENTORY_CELLS;

    private Color _selectedCellColor = new Color(0.3113208f, 0.3113208f, 0.3113208f, 1f);
    private Color _neutralCellColor = new Color(0.1132075f, 0.1132075f, 0.1132075f, 1f);

    private float _currentWeight;
    [SerializeField] private WeightIndicator _weightIndicator;
    [SerializeField] private ShowHideInventory _showHideInventory;
    public enum ItemInfo
    {
        None,
        BasebalBat,
        Knife,
        Sword,
        Katana,
        Scrap
    }
    private ItemInfo[] _cellItemID;

    public int selectedCellID = -1;
    [SerializeField] private GameObject[] cellObjects;

    private void Start()
    {
        _cellItemID = new ItemInfo[MAX_INVENTORY_CELLS];
        _cellItemAmount = new int[MAX_INVENTORY_CELLS];
        _cellItemWeight = new float[MAX_INVENTORY_CELLS];
        for (int i = 0; i < MAX_INVENTORY_CELLS; i++)
        {
            _cellItemID[i] = (ItemInfo)Random.Range(0, 3);
            _cellItemAmount[i] = 0;
            _cellItemWeight[i] = 0f;
        }
    }
    private void OnEnable()
    {
        EventBus.OnClickedInventoryCell += SelectInventoryCell;
        EventBus.OnClickedButtonDropItem += DropItem;
    }
    private void OnDisable()
    {
        EventBus.OnClickedInventoryCell -= SelectInventoryCell;
        EventBus.OnClickedButtonDropItem -= DropItem;
    }
    public void DropItem()
    {
        if (selectedCellID < MAX_INVENTORY_CELLS && selectedCellID != -1)
        {
            // Spawn item on ground
            GiveItemForCellID(selectedCellID, 0, 0, 0);
            ResetInventorySelection();
            _weightIndicator.UpdateWeightInfo();
        }
        else Debug.Log("Overflow stack MAX_INVENTORY_CELL");
    }
    public void ResetInventorySelection()
    {
        if (selectedCellID != -1) cellObjects[selectedCellID].GetComponent<Image>().color = _neutralCellColor;
        selectedCellID = -1;
    }
    public void SelectInventoryCell(int id)
    {
        if (selectedCellID == id)
        {
            ResetInventorySelection();
            cellObjects[id].GetComponent<Image>().color = _neutralCellColor;
        }
        else
        {
            if (selectedCellID != -1)
            {
                if (_cellItemID[selectedCellID] != ItemInfo.None && _cellItemID[id] == ItemInfo.None)
                {
                    GiveItemForCellID(id, _cellItemID[selectedCellID], _cellItemAmount[selectedCellID], _cellItemWeight[selectedCellID]);
                    GiveItemForCellID(selectedCellID, 0, 0, 0);
                    ResetInventorySelection();
                    Debug.Log("Поменялись");
                }
                else
                {
                    if (_cellItemID[id] != ItemInfo.None)
                    {
                        SelectCell(id, true);
                    }
                }
            }
            else if(_cellItemID[id] != ItemInfo.None)
            {
                SelectCell(id);
            }
        }
    }
    private void SelectCell(int id, bool resetColor = false)
    {
        if(resetColor) cellObjects[selectedCellID].GetComponent<Image>().color = _neutralCellColor;
        selectedCellID = id;
        cellObjects[id].GetComponent<Image>().color = _selectedCellColor;
    }
    public void GiveItemInFreeCell(ItemInfo item, int amount, float weight)
    {
        bool full = false;
        for(int i = 0; i < MAX_INVENTORY_CELLS; i++)
        {
            if (_cellItemID[i] != ItemInfo.None) continue;
            GiveItemForCellID(i, item, amount, weight);
            full = true;
            break;
        }
        if (!full) Debug.Log("Нет свободного места!");
    }
    public void GiveItemForCellID(int id, ItemInfo item, int amount, float weight)
    {
        _cellItemID[id] = item;
        _cellItemAmount[id] = amount;
        _cellItemWeight[id] = weight;
        if(_showHideInventory.IsInventoryShow) _weightIndicator.UpdateWeightInfo();
    }
    public float GetCurrentWeightInventory()
    {
        _currentWeight = 0;
        for (int i = 0; i < GetInfoMaxCells(); i++)
        {
            if (GetInfoItemID(i) == ItemInfo.None) continue;
            _currentWeight += GetInfoItemWeight(i) * GetInfoItemAmount(i);
        }
        return _currentWeight;
    }
}