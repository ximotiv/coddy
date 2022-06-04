using UnityEngine;

public class InventoryItemChanger : MonoBehaviour
{
    private int[] _cellItemAmount;
    private float[] _cellItemWeight;
    private InventoryData.ItemInfo[] _cellItemID;
    public InventoryData.ItemInfo GetInfoItemID(int id) => _cellItemID[id];
    public int GetInfoItemAmount(int id) => _cellItemAmount[id];
    public float GetInfoItemWeight(int id) => _cellItemWeight[id];

    private int _maxCells;

    private void Start()
    {
        _maxCells = (int)EventBus.GetMaxCells?.Invoke();
        // For tests
        _cellItemID = new InventoryData.ItemInfo[_maxCells];
        _cellItemAmount = new int[_maxCells];
        _cellItemWeight = new float[_maxCells];
        for (int i = 0; i < _maxCells; i++)
        {
            _cellItemID[i] = (InventoryData.ItemInfo)Random.Range(0, 3);
            _cellItemAmount[i] = _cellItemID[i] == InventoryData.ItemInfo.None ? 0 : Random.Range(1, 3);
            _cellItemWeight[i] = _cellItemID[i] == InventoryData.ItemInfo.None ? 0 : Random.Range(1, 15);
        }
    }
    private void OnEnable()
    {
        EventBus.OnClickedButtonDropItemEvent += DropItem;
    }
    private void OnDisable()
    {
        EventBus.OnClickedButtonDropItemEvent -= DropItem;
    }
    private void GiveItemToEmptyCell(InventoryData.ItemInfo item, int amount, float weight)
    {
        bool full = false;
        for (int i = 0; i < _maxCells; i++)
        {
            if (_cellItemID[i] != InventoryData.ItemInfo.None) continue;
            GiveItemToCellID(i, item, amount, weight);
            full = true;
            break;
        }
        if (!full) Debug.Log("I don't have free cell!");
    }
    private void GiveItemToCellID(int id, InventoryData.ItemInfo item, int amount, float weight, bool updateweight = true)
    {
        _cellItemID[id] = item;
        _cellItemAmount[id] = amount;
        _cellItemWeight[id] = weight;
        if (updateweight) EventBus.OnPlayerGetItemEvent?.Invoke();
    }
    public void MoveItemToCell(int oldCellID, int newCellID)
    {
        GiveItemToCellID(newCellID, _cellItemID[oldCellID], _cellItemAmount[oldCellID], _cellItemWeight[oldCellID], false);
        GiveItemToCellID(oldCellID, InventoryData.ItemInfo.None, 0, 0, false);
    }
    private void DropItem(int id)
    {
        if (id < _maxCells && id != -1)
        {
            // Spawn item on ground
            GiveItemToCellID(id, 0, 0, 0);
            EventBus.OnPlayerDropItemEvent?.Invoke();
        }
        else Debug.Log("Overflow stack MAX_INVENTORY_CELL");
    }
}