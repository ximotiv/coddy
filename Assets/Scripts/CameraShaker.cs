using UnityEngine;
using DG.Tweening;
public class CameraShaker : MonoBehaviour
{
    private Camera _camera;
    private Tween tween;

    private void Start()
    {
        _camera = Camera.main;
    }
    private void OnEnable()
    {
        EventBus.OnPlayerShot += CameraShake;
    }
    private void OnDisable()
    {
        EventBus.OnPlayerShot -= CameraShake;
    }

    private void CameraShake()
    {
        if (tween != null) _camera.DOKill();
        tween = _camera.DOShakeRotation(0.3f, 2).OnComplete(() =>
        {
            transform.DORotate(new Vector3(0, 0, 0), 0.2f);
        });
    }
}
