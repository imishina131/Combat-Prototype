// Combat Prototype
// Irina Mishina
// 2026-03-24
using UnityEngine;

public class TankEnemy : EnemyBehavior
{

    public GameObject projectile;
    public Transform startLocation;

    protected override void Awake()
    {
        base.Awake();

        agent.speed = 3f;
        attackRange = 5f;
        attackCooldown = 4f;
    }

    public override void Attack()//chooses between 2 attacks : projectile or close range
    {
        base.Attack();

        int chosenAttack = Random.Range(1, 2);

        if (chosenAttack == 1)
        {
            animator.SetTrigger("attackHeadbutt");
        }
        else if(chosenAttack == 2)
        {
            ThrowProjectile();
        }
    }

    void ThrowProjectile()//instantiates projectile
    {
        Instantiate(projectile, startLocation.position, Quaternion.identity);
    }
}
