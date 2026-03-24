using UnityEngine;

public class Grunt_Tank : EnemyBehavior
{
    protected override void Awake()
    {
        base.Awake();

        agent.speed = 2f;
        attackRange = 2.5f;
        attackCooldown = 5f;
    }

    public override void Attack()
    {
        base.Attack();
        animator.SetTrigger("attackHeadbutt");
    }
}
