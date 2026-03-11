using UnityEngine;

public class Grunt_Tank : EnemyBehavior
{
    public int damage = 5;

    protected override void Awake()
    {
        base.Awake();

        agent.speed = 2f;
        attackRange = 2.5f;
        attackCooldown = 3f;
    }

    public override void Attack()
    {
        Debug.Log("Tank hits player");
    }
}
