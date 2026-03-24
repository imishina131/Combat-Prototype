using UnityEngine;
using UnityEngine.AI;

public class ArcherEnemy : EnemyBehavior
{
    public GameObject arrowPrefab;
    public Transform firePoint;

    public float teleportRadius = 8f;
    public float archerAttackRange = 15f;
    public float safeDistance = 4f;
    public float teleportDelay = 2f;

    private float closeTimer = 0f;

    protected override void Update()
    {
        // if dead return
        if (enemy.isDead) return;

        float distance = Vector3.Distance(transform.position, player.position);

        agent.ResetPath();

        // if player is too close, start timer
        if (distance <= safeDistance)
        {
            closeTimer += Time.deltaTime;

            // if timer exceeds limit, teleport away
            if (closeTimer >= teleportDelay)
            {
                Teleport();
                closeTimer = 0f;
            }
        }
        else
        {
            closeTimer = 0f;
        }

        // if player is outside of close range, attempt to attack
        if (distance <= archerAttackRange)
        {
            TryAttack();
        }
    }

    public override void Attack()
    {
        ShootArrow();
    }

    void ShootArrow()
    {
        // spawns arrow and fires to players last POS
        if (arrowPrefab != null && firePoint != null)
        {
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

            Vector3 direction = (player.position - firePoint.position).normalized;
            arrow.GetComponent<Rigidbody>().linearVelocity = direction * 6f;

            Collider arrowCollider = arrow.GetComponent<Collider>();
            Collider enemyCollider = GetComponent<Collider>();

            Physics.IgnoreCollision(arrowCollider, enemyCollider);
        }
    }

    // chooses random pos on nav mesh and teleport to that pos
    void Teleport()
    {
        Vector3 randomDirection = Random.insideUnitSphere * teleportRadius;
        randomDirection += transform.position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, teleportRadius, 1))
        {
            agent.Warp(hit.position);
        }
    }
}
