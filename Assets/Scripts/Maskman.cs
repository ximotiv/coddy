public class Maskman : Enemy
{
    private AnimHash _animhash;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Health = 100;
        _animhash = new AnimHash();
    }

    protected override void PlayAnimTakeDamage()
    {
        EnemyAnimator.CrossFadeInFixedTime(_animhash.MaskManTakeDamage, 0.1f);
    }

    protected override void OnEnemyDead()
    {
        base.OnEnemyDead();
        EnemyAnimator.CrossFadeInFixedTime(_animhash.MaskManDead, 0.1f);
    }
}