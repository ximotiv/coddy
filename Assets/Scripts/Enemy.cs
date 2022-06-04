using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;

    private void Start()
    {
        _health = 100;
    }

    public void TakeDamage(int damage)
    {
        EventBus.OnPlayerShotOnEnemy?.Invoke();
        _health -= damage;
        if(_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
