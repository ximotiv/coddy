using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected ParticleSystem PrefabAttackFX;
    [SerializeField] protected Sprite Icon;
    [SerializeField] private Vector2 SpritePosition;

    public List<Enemy> ClosedEnemys = new List<Enemy>();

    protected string Label;
    protected int Damage;
    protected float Cooldown;
    
    protected PlayerData Player;

    public virtual void WeaponShot()
    {
        if (Player.CurrentCooldown <= 0)
        {
            EventBus.OnPlayerShot?.Invoke();

            if (ClosedEnemys.Count > 0)
            {
                foreach (Enemy enemy in ClosedEnemys)
                {
                    enemy.TakeDamage(Damage);
                }
            }

            Player.CurrentCooldown = Cooldown;
            PlayAttackFX();
        }
    }

    public virtual void Init(PlayerData player)
    {
        Player = player;
        //AttackFX = Instantiate(PrefabAttackFX, player.transform);
        //AttackFX.transform.localPosition = player.GetAttackPoint;
        Player.RightHand.sprite = Icon;
        Player.RightHand.transform.localPosition = SpritePosition;
    }

    public void PlayAttackFX()
    {
        PrefabAttackFX.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (!enemy.IsEnemyDead)
            {
                bool full = false;
                foreach (Enemy enemyF in ClosedEnemys)
                {
                    if (enemyF == enemy)
                    {
                        full = true;
                        break;
                    }
                }
                if (!full)
                {
                    ClosedEnemys.Add(enemy);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if(_currentEnemyClosed != null)
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (ClosedEnemys.Count > 0)
            {
                foreach (Enemy enemyF in ClosedEnemys)
                {
                    if (enemyF == enemy)
                    {
                        ClosedEnemys.Remove(enemyF);
                        break;
                    }
                }
            }
        }
    }
}
