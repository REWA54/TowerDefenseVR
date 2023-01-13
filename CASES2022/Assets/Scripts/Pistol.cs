using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

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
        xrController.SendHapticImpulse(1f, 0.2f);
        GameObject Bullet = Instantiate(BulletGO);
        Bullet.GetComponent<Bullet>().Fire(BarrelEnd, BarrelEnd.forward, FireSpeed,damagesMultiplier,Vector3.zero);
    }

    public void ChangeGrabbedState(bool state)
    {
        isGrabbed = state;
        Debug.Log("Weapon state is now : " + isGrabbed);
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
