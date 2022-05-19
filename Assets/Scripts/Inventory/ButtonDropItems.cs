using UnityEngine;

public class ButtonDropItems : MonoBehaviour
{
    public void OnPlayerClickButton()
    {
        EventBus.OnClickedButtonDropItem?.Invoke();
    }
}
