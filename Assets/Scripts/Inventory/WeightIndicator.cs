using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeightIndicator : MonoBehaviour
{
    [SerializeField] private Gradient _gradientFill;
    [SerializeField] private Text _textWeight;
    [SerializeField] private ShowHideInventory _showHideInventory;
    [SerializeField] private InventoryItemChanger _itemChanger;
    private Image _weightBar;

    private void Start()
    {
        _weightBar = GetComponent<Image>();
    }
    private void OnEnable()
    {
        EventBus.OnPlayerGetItemEvent += UpdateWeightInfo;
        EventBus.OnPlayerDropItemEvent += UpdateWeightInfo;
    }
    private void OnDisable()
    {
        EventBus.OnPlayerGetItemEvent -= UpdateWeightInfo;
        EventBus.OnPlayerDropItemEvent -= UpdateWeightInfo;
    }
    public void UpdateWeightInfo()
    {
        if(_showHideInventory.IsInventoryShow)
        {
            float _weight = GetCurrentWeightInventory();
            _weightBar.DOFillAmount(_weight / 100, 1f);
            _weightBar.DOColor(_gradientFill.Evaluate(_weight / 100), 1f);
            _textWeight.text = _weight + " kg";
        }
    }
    private float GetCurrentWeightInventory()
    {
        float _currentWeight = 0;
        int _maxCells = (int)EventBus.GetMaxCells?.Invoke();
        for (int i = 0; i < _maxCells; i++)
        {
            InventoryData.ItemInfo _cellItemID = _itemChanger.GetInfoItemID(i);
            int _cellItemAmount = _itemChanger.GetInfoItemAmount(i);
            float _cellItemWeight = _itemChanger.GetInfoItemWeight(i);

            if (_cellItemID == InventoryData.ItemInfo.None) continue;
            _currentWeight += _cellItemWeight * _cellItemAmount;
        }
        return _currentWeight;
    }
}
