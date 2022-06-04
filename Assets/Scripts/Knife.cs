using UnityEngine;

public class Knife : Weapon
{
    public override void Init(PlayerData player)
    {
        base.Init(player);
        Damage = 25;
        Label = "Кинжал";
        Cooldown = 0.2f;
    }

    public override void WeaponShot()
    {
        if(Player.CurrentCooldown <= 0)
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
}
