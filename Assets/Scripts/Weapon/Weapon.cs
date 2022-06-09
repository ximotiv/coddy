using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected ParticleSystem PrefabAttackFX;
    [SerializeField] protected Sprite Icon;
    [SerializeField] private Vector2 SpritePosition;

    protected ParticleSystem AttackFX;

    protected string Label;
    protected int Damage;
    protected float Cooldown;
    
    protected PlayerData Player;

    public virtual void WeaponShot()
    {
        if (Player.CurrentCooldown <= 0)
        {
            EventBus.OnPlayerShot?.Invoke();

            if (Player.CurrentEnemyClosed != null)
            {
                Player.CurrentEnemyClosed.TakeDamage(Damage);
            }
            Player.CurrentCooldown = Cooldown;
            PlayAttackFX();
        }
    }

    public virtual void Init(PlayerData player)
    {
        Player = player;
        AttackFX = Instantiate(PrefabAttackFX, Vector2.zero, Quaternion.identity);
        AttackFX.transform.parent = player.transform;
        AttackFX.transform.localPosition = player.GetAttackPoint;
        Player.RightHand.sprite = Icon;
        Player.RightHand.transform.localPosition = SpritePosition;
    }

    public void PlayAttackFX()
    {
        AttackFX.Play();
    }
}
