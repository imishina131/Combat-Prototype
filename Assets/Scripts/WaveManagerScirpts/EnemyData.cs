using UnityEngine;

[System.Serializable]
public class EnemyData 
{
    public string enemyName;
    public GameObject prefab;
    public int pointCost;
    public int weight; // higher = more common
}
