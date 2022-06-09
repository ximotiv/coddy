public class Knife : Weapon
{
    public override void Init(PlayerData player)
    {
        base.Init(player);
        Damage = 25;
        Label = "Knife";
        Cooldown = 0.2f;
    }
}
