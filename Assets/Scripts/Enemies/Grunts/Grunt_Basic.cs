using UnityEngine;

public class Grunt_Basic : EnemyBehavior
{
    public int damage = 3;

    protected override void Awake()
    {
        base.Awake();

        agent.speed = 4f;
        attackRange = 1.8f;
        attackCooldown = 1.9f;

    }

    public override void Attack()
    {
        //reference to player health (take away damage)
    }
}
