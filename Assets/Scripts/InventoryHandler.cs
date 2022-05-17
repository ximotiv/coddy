using UnityEngine;
using UnityEngine.UI;

public class InventoryHandler : MonoBehaviour
{
    private const int MAX_INVENTORY_CELLS = 8;
    [SerializeField] private int[] _cellItemAmount;
    [SerializeField] private float[] _cellItemWeight;

    private Color _selectedCellColor = new Color(0.3113208f, 0.3113208f, 0.3113208f, 1f);
    private Color _neutralCellColor = new Color(0.1132075f, 0.1132075f, 0.1132075f, 1f);

    private float _currentWeight;
    [SerializeField] private WeightIndicator _weightIndicator;
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
    public void DropItem(int id)
    {
        if (id < MAX_INVENTORY_CELLS && id != -1)
        {
            // Spawn item on ground
            GivePlayerItem(id, 0, 0, 0);
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
                    GivePlayerItem(id, _cellItemID[selectedCellID], _cellItemAmount[selectedCellID], _cellItemWeight[selectedCellID]);
                    GivePlayerItem(selectedCellID, 0, 0, 0);
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
    public void GivePlayerItem(int id, ItemInfo item, int amount, float weight)
    {
        _cellItemID[id] = item;
        _cellItemAmount[id] = amount;
        _cellItemWeight[id] = weight;
        //_weightIndicator.UpdateWeightInfo();
    }
    public ItemInfo GetInfoItemID(int id)
    {
        return _cellItemID[id];
    }
    public int GetInfoItemAmount(int id)
    {
        return _cellItemAmount[id];
    }
    public float GetInfoItemWeight(int id)
    {
        return _cellItemWeight[id];
    }
    public int GetInfoMaxCells()
    {
        return MAX_INVENTORY_CELLS;
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