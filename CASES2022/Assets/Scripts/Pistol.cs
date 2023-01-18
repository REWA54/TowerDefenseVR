using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pistol : MonoBehaviour
{
    [SerializeField] GameObject BulletGO;
    [SerializeField] Transform BarrelEnd;
    [SerializeField] float FireSpeed;

    public XRDirectInteractor xrController;
    public bool isGrabbed;
    public float upgradePrice;
    public float damagesMultiplier;
    public int Level;
    public XRInteractorLineVisual xRInteractorLineVisual;

    [SerializeField] TMP_Text LevelUI;
    [SerializeField] ParticleSystem shootParticles;

    private void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(FireBullet);
    }

    void FireBullet(ActivateEventArgs args)
    {
        shootParticles.Play();
        xrController.SendHapticImpulse(0.3f, 0.1f);
        GameObject Bullet = Instantiate(BulletGO);
        Bullet.GetComponent<Bullet>().Fire(BarrelEnd, BarrelEnd.forward, FireSpeed, damagesMultiplier, Vector3.zero);
    }

    public void ChangeGrabbedState(bool state)
    {
        isGrabbed = state;
        xRInteractorLineVisual.enabled = !isGrabbed;
    }

    public void Upgrade()
    {
        Level++;
        damagesMultiplier *= 1.2f;
        upgradePrice *= 1.2f;
        UpdateUI();
    }

    private void UpdateUI()
    {
        LevelUI.text = Level.ToString();
    }

}
