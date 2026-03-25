// Combat Prototype
// Irina Mishina
// 2026-03-24
using UnityEngine;

public class Grunt_Basic : EnemyBehavior
{
    protected override void Awake()
    {
        base.Awake();

        agent.speed = 2f;
        attackRange = 1.8f;
        attackCooldown = 3f;

    }

    public override void Attack()//attacks
    {
        base.Attack();
        animator.SetTrigger("hookPunch");
    }
}
