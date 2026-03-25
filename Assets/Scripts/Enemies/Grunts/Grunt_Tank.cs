// Combat Prototype
// Cameron Lee Czysz-Mille
// 2026-03-24
using UnityEngine;

public class Grunt_Tank : EnemyBehavior
{
    protected override void Awake()
    {
        // Grabs EnemyBehavior base awake
        base.Awake();

        // Sets custom enemy speed, range, and attackCooldown
        agent.speed = 1.5f;
        attackRange = 1.5f;
        attackCooldown = 5f;
    }

    public override void Attack()
    {
        // triggers attack animation
        base.Attack();
        animator.SetTrigger("attackHeadbutt");
    }
}
