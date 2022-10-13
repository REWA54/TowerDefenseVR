using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
public class EnemyData : ScriptableObject
{
    public float Damage;
    public float Life;
    public float Speed;
    public float lootMoney;
}
