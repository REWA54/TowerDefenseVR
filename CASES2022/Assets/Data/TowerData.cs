using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Tower")]
public class TowerData : ScriptableObject
{
    public float range;
    public float damagesMultiplicator;
    public int price;

    [Header("Canon & Missile Towers Only")]
    public float bulletVelocity;
    public float shootingRate;

    [Header("Rail Tower Only")]
    public LineRenderer lineRenderer;
    public Light railHitLight;
    public ParticleSystem railHitParticles;
}

