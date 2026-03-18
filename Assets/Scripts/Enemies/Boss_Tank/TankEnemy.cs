using UnityEngine;

public class TankEnemy : EnemyBehavior
{
    protected override void Awake()
    {
        base.Awake();

        agent.speed = 3f;
        attackRange = 1.2f;
        attackCooldown = 4f;
    }

    public override void Attack()
    {
        base.Attack();
        animator.SetTrigger("attackHeadbutt");
        //"slap" the player
    }

    void ThrowProjectile()
    {
        //instantiate projectile thrown at last player position
    }
}
