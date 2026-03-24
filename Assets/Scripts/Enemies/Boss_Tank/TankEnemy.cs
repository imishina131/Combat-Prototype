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

    public override void Attack()
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

    void ThrowProjectile()
    {
        Instantiate(projectile, startLocation.position, Quaternion.identity);

    }
}
