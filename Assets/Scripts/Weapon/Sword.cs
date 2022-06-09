public class Sword : Weapon
{
    public override void Init(PlayerData player)
    {
        base.Init(player);
        Damage = 30;
        Label = "Sword";
        Cooldown = 0.3f;
    }
}
