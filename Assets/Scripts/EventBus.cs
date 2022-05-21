using System;

public static class EventBus
{
    public static Action OnPlayerGetItemEvent;
    public static Action OnPlayerDropItemEvent;
    public static Action<int> OnClickedInventoryCellEvent;
    public static Action<int> OnClickedButtonDropItemEvent;

    public static Func<int> GetMaxCells;
}
