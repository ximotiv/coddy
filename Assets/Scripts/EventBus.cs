using System;

public static class EventBus
{
    public static Action<int> OnClickedInventoryCell;
    public static Action OnClickedButtonDropItem;
    public static Action<InventoryHandler.ItemInfo, int, float> OnPlayerPickupedItem;
}
