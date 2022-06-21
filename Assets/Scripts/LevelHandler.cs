using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class LevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject _road;
    [SerializeField] private Image _background;

    private AudioSource _music;
    public bool IsLevelStarted { get; private set; }


    private void Start()
    {
        _music = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventBus.OnPlayerShotOnEnemy += StartMusic;
    }
    private void OnDisable()
    {
        EventBus.OnPlayerShotOnEnemy -= StartMusic;
    }

    private void StartMusic()
    {
        if(!IsLevelStarted)
        {
            _road.transform.DOMoveX(3.87093f, 0.5f);
            _background.DOFade(1, 0.5f);
            _music.Play();
            IsLevelStarted = true;
        }
    }
}
