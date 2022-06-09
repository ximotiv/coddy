using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Animator))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected Animator EnemyAnimator;

    public Transform TargetChase;
    private Vector3 _targetLastPosition;

    private Tweener _tween;

    public bool IsEnemyDead { get; private set; }

    public virtual void Init()
    {
        IsEnemyDead = false;
        EnemyAnimator = GetComponent<Animator>();
        _tween = transform.DOMove(TargetChase.position, 2).SetAutoKill(false);
        _targetLastPosition = TargetChase.position;
    }

    protected void FixedUpdate()
    {
        if(_targetLastPosition != TargetChase.position && !IsEnemyDead)
        {
            _tween.ChangeEndValue(TargetChase.position, true).Restart();
            _targetLastPosition = TargetChase.position;
        }
    }

    public virtual void TakeDamage(int damage)
    {
        if(!IsEnemyDead)
        {
            EventBus.OnPlayerShotOnEnemy?.Invoke();
            Health -= damage;
            if (Health <= 0)
            {
                OnEnemyDead();
            }
            else
            {
                PlayAnimTakeDamage();
            }
        }
    }

    protected virtual void OnEnemyDead()
    {
        IsEnemyDead = true;
        Invoke(nameof(DisableObject), 1f);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    protected abstract void PlayAnimTakeDamage();
}