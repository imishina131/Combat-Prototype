using UnityEngine;

public class Grunt_Fast : EnemyBehavior
{
    public int damage = 1;

    protected override void Awake()
    {
        base.Awake();

        agent.speed = 6f;
        attackRange = 1.2f;
        attackCooldown = 0.8f;
    }

    public override void Attack()
    {
        animator.SetTrigger("attackPunch");
        Debug.Log("Fast Enemy Hit Player");
    }
}
