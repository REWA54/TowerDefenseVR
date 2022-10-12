using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Tower")]
public class TowerData : ScriptableObject
{
    public float range;
    public float shootingRate;
    public float damagesMultiplicator;
    public float bulletVelocity;
    public int price;
}

