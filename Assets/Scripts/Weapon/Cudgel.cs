public class Cudgel : Weapon
{
    public override void Init(PlayerData player)
    {
        base.Init(player);
        Damage = 100;
        Label = "Cudgel";
        Cooldown = 0.5f;
    }
}
