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
        ThrowProjectile();
        //animator.SetTrigger("attackHeadbutt");
        //"slap" the player
    }

    void ThrowProjectile()
    {
        Instantiate(projectile, startLocation.position, Quaternion.identity);

    }
}
