using UnityEngine;

public class AnimHash
{
    public readonly int HeroIdle = Animator.StringToHash("Hero_Idle");
    public readonly int HeroRun = Animator.StringToHash("Hero_Run");
    public readonly int HeroAttack = Animator.StringToHash("Hero_Attack");

    public readonly int MaskManDead = Animator.StringToHash("Maskman_Dead");
    public readonly int MaskManTakeDamage = Animator.StringToHash("Maskman_TakeDamage");
}
