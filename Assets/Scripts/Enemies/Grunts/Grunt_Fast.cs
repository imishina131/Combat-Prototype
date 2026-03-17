using UnityEngine;

public class Grunt_Fast : EnemyBehavior
{
    protected override void Awake()
    {
        base.Awake();

        agent.speed = 6f;
        attackRange = 1.2f;
        attackCooldown = 2.7f;
    }

    public override void Attack()
    {
        base.Attack();
        animator.SetTrigger("attackPunch");
        // grab player method to take damage
        Debug.Log("Fast Enemy Hit Player");
    }
}
