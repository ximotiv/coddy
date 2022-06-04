using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected ParticleSystem PrefabAttackFX;
    [SerializeField] protected Sprite Icon;

    protected ParticleSystem AttackFX;

    protected string Label;
    protected int Damage;
    protected float Cooldown;
    
    protected PlayerData Player;

    public abstract void WeaponShot();
    public virtual void Init(PlayerData player)
    {
        Player = player;
        AttackFX = Instantiate(PrefabAttackFX, Vector2.zero, Quaternion.identity);
        AttackFX.transform.parent = player.transform;
        AttackFX.transform.localPosition = player.GetAttackPoint;
        Player.RightHand.sprite = Icon;
    }

    public void PlayAttackFX()
    {
        AttackFX.Play();
    }
}
