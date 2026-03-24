using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    // used by the wave manager for enemy data
    public string enemyName;
    public GameObject prefab;
    public int pointCost;
    public int weight; // higher = more common
}
