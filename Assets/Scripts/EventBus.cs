using System;
using UnityEngine.Events;

public static class EventBus
{
    public static Action OnPlayerGetItemEvent;
    public static Action OnPlayerDropItemEvent;
    public static Action<int> OnClickedInventoryCellEvent;
    public static Action<int> OnClickedButtonDropItemEvent;

    public static Func<int> GetMaxCells;

    public static UnityAction OnPlayerShot;
    public static UnityAction OnPlayerShotOnEnemy;
}
