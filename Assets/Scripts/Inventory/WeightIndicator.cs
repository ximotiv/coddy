using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeightIndicator : MonoBehaviour
{
    [SerializeField] private Gradient _gradientFill;
    [SerializeField] private InventoryHandler _inventoryHandler;
    [SerializeField] private Text _textWeight;
    private Image _weightBar;

    private void Start()
    {
        _weightBar = GetComponent<Image>();
    }
    public void UpdateWeightInfo()
    {
        DOTween.Complete(_weightBar);
        float _weight = _inventoryHandler.GetCurrentWeightInventory();
        _weightBar.DOFillAmount(_weight / 100, 1f);
        _weightBar.DOColor(_gradientFill.Evaluate(_weight / 100), 1f);
        _textWeight.text = _weight + " kg";
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R)) UpdateWeightInfo();
    }
}
