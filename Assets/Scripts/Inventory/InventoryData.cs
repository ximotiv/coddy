using UnityEngine;

public class InventoryData : MonoBehaviour
{
    private const int MAX_INVENTORY_CELLS = 8;

    public enum ItemInfo
    {
        None,
        BasebalBat,
        Knife,
        Sword,
        Katana,
        Scrap
    }

    public int GetInfoMaxCells() => MAX_INVENTORY_CELLS;

    private void OnEnable()
    {
        EventBus.GetMaxCells += GetInfoMaxCells;
    }
    private void OnDisable()
    {
        EventBus.GetMaxCells -= GetInfoMaxCells;
    }
}