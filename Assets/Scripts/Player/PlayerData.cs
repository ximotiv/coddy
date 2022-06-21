using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private List<GameObject> _weaponPrefabs;
    [SerializeField] private Vector2 _attackPoint;
    public SpriteRenderer RightHand;
    public float CurrentCooldown = 0;

    public Vector2 GetAttackPoint => _attackPoint;

    private Weapon _currentWeapon;

    private Animator _animator;
    private AnimHash _animhash;

    private void Start()
    {
        SetPlayerWeapon(_weaponPrefabs[0]);
        _health = 100;
        _animator = GetComponent<Animator>();
        _animhash = new AnimHash();
    }

    public void SetPlayerWeapon(GameObject weapon)
    {
        if(_currentWeapon != null) Destroy(_currentWeapon.gameObject);
        _currentWeapon = Instantiate(weapon, RightHand.transform).GetComponent<Weapon>();
        _currentWeapon.Init(this);
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
}