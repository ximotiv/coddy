public class Sword : Weapon
{
    public override void Init(PlayerData player)
    {
        base.Init(player);
        Damage = 50;
        Label = "Sword";
        Cooldown = 0.15f;
    }
}
