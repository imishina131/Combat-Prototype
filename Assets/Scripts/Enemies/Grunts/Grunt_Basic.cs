using UnityEngine;

public class Grunt_Basic : EnemyBehavior
{
    protected override void Awake()
    {
        base.Awake();

        agent.speed = 4f;
        attackRange = 1.8f;
        attackCooldown = 3f;

    }

    public override void Attack()
    {
        base.Attack();
        animator.SetTrigger("hookPunch");
        //reference to player health (take away damage)
    }
}
