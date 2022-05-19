using UnityEngine;

public class ShowHideInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    private bool isShow;
    public bool IsInventoryShow => isShow;

    private void ShowHide()
    {
        isShow = !isShow;
        _inventory.SetActive(isShow);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y)) ShowHide();
    }
}
