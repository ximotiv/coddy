using UnityEngine;

public class ShowHideInventory : MonoBehaviour
{
    [SerializeField] private GameObject _inventory;
    [SerializeField] private WeightIndicator _weightUpdate;
    private bool _isShow = false;
    public bool IsInventoryShow => _isShow;

    private void ShowHide()
    {
        _isShow = !_isShow;
        _inventory.SetActive(_isShow);
        _weightUpdate.Invoke("UpdateWeightInfo", 0.2f);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y)) ShowHide();
    }
}
