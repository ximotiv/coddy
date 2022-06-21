using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected Animator EnemyAnimator;

    public Transform TargetChase;
    private Vector2 _targetLastPosition;
    private Rigidbody2D _rigidbody;

    private float _speed = 2f;

    public bool IsEnemyDead { get; private set; }

    public virtual void Init()
    {
        IsEnemyDead = false;
        EnemyAnimator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        _targetLastPosition = TargetChase.position;
    }

    protected void FixedUpdate()
    {
        if (!IsEnemyDead && Vector2.Distance(TargetChase.position, transform.position) > 2f)
        {
            _rigidbody.velocity = new Vector2(TargetChase.position.x > transform.position.x ? _speed : -_speed, 0);
            _targetLastPosition = TargetChase.position;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
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
        GetComponent<CapsuleCollider2D>().isTrigger = true;
        IsEnemyDead = true;
        Invoke(nameof(DisableObject), 1f);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    protected abstract void PlayAnimTakeDamage();
}