public class ThrowingKnife : Weapon
{
    public override void Init(PlayerData player)
    {
        base.Init(player);
        Damage = 35;
        Label = "Throwing Knife";
        Cooldown = 0.4f;
    }
}
