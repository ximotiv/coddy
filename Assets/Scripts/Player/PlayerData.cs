using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<Weapon> _weapon;
    [SerializeField] private Vector2 _attackPoint;
    public SpriteRenderer RightHand;
    public float CurrentCooldown = 0;

    public Vector2 GetAttackPoint => _attackPoint;
    private Enemy _currentEnemyClosed;
    public Enemy CurrentEnemyClosed => _currentEnemyClosed;

    private Weapon _currentWeapon;
    private Animator _animator;
    private AnimHash _animhash;

    private void Start()
    {
        _currentWeapon = _weapon[0];
        _currentWeapon.Init(this);
        _health = 100;
        _animator = GetComponent<Animator>();
        _animhash = new AnimHash();
    }

    private void Update()
    {
        if (CurrentCooldown > 0f) CurrentCooldown -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.A) && CurrentCooldown <= 0)
        {
            _currentWeapon.WeaponShot();
            _animator.CrossFadeInFixedTime(_animhash.HeroAttack, 0.1f);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.CrossFadeInFixedTime(_animhash.HeroIdle, 0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _currentEnemyClosed = enemy;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(_currentEnemyClosed != null) _currentEnemyClosed = null;
    }
}
