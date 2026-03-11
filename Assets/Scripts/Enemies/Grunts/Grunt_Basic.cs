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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        //reference to player health (take away damage)
    }
}
