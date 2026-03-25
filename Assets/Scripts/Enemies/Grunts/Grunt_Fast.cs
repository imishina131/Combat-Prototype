// Combat Prototype
// Cameron Lee Czysz-Mille
// 2026-03-24
using UnityEngine;

public class Grunt_Fast : EnemyBehavior
{
    protected override void Awake()
    {
        // Grabs EnemyBehavior base awake
        base.Awake();

        // Sets custom enemy speed, range, and attackCooldown
        agent.speed = 6f;
        attackRange = 1.2f;
        attackCooldown = 2.7f;
    }

    public override void Attack()
    {
        // triggers attack animation
        base.Attack();
        animator.SetTrigger("attackPunch");
    }
}
