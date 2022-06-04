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
            float weight = GetCurrentWeightInventory();
            _weightBar.DOFillAmount(weight / 100, 1f);
            _weightBar.DOColor(_gradientFill.Evaluate(weight / 100), 1f);
            _textWeight.text = weight + " kg";
        }
    }
    private float GetCurrentWeightInventory()
    {
        float currentWeight = 0;
        int maxCells = (int)EventBus.GetMaxCells?.Invoke();
        for (int i = 0; i < maxCells; i++)
        {
            InventoryData.ItemInfo _cellItemID = _itemChanger.GetInfoItemID(i);
            int _cellItemAmount = _itemChanger.GetInfoItemAmount(i);
            float _cellItemWeight = _itemChanger.GetInfoItemWeight(i);

            if (_cellItemID == InventoryData.ItemInfo.None) continue;
            currentWeight += _cellItemWeight * _cellItemAmount;
        }
        return currentWeight;
    }
}
