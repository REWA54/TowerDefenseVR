using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class TutorialManagement : MonoBehaviour
{
    // The tutorial is separated in 4 states : learn to pick up the gun & use it; learn to upgrade the gun; learn to purshase towers and place them; learn to upgrade towers.

    public int tutorialState;
    private int highestTutorialState;

    public TMP_Text TextTables;
    public GameObject ButtonTables;
    public TMP_Text TextButtonTables;

    public TMP_Text TextShooting;
    public GameObject ButtonShooting;
    public TMP_Text TextButtonShooting;

    public GameObject tableGun;
    public GameObject tableUpgrade;
    public GameObject tableShop;

    public GameObject[] Tanks ;
    public GameObject[] PlacePoints;
    public GameObject[] ShopButtons;
    public GameObject[] Enemys;

    private List<bool> ThingsUpgraded;
    int ButtonPressedState;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in PlacePoints)
        {
            item.SetActive(false);
        }
        foreach (var item in ShopButtons)
        {
            item.SetActive(false);
        }
        highestTutorialState = PlayerPrefs.GetInt("highestTutorialState", 0);
        tutorialState = highestTutorialState;
        
        LoadState(tutorialState);
    }
    public void ButtonPressed()
    {
        switch (ButtonPressedState)
        {
            default:
                return;
            case 0:
                tableGun.SetActive(true);
                TextTables.text = "First, grab the gun and kill the Tank behind you";
                ButtonTables.gameObject.SetActive(false);
                break;
            case 1:
                Tanks[2].SetActive(true);
                break;
        }
    }
    public void SaveTutorialState()
    {
        if (tutorialState > highestTutorialState)
        {
            PlayerPrefs.SetInt("highestTutorialState", tutorialState);
        }
    }

    public void LoadState (int state)
    {
        tutorialState = state;
       // SaveTutorialState();
        tableShop.gameObject.SetActive(false);
        tableUpgrade.gameObject.SetActive(false);

        switch (state)
        {
            case 0:
                Tanks[0].SetActive(true);
                break;
            case 1:
                tableUpgrade.SetActive(true);
                break;
            case 2:
                tableShop.SetActive(true);
                ShopButtons[0].SetActive(true);
                break;
            case 3:
                tableShop.SetActive(true);
                tableUpgrade.SetActive(true);
                break;
            case 4:
                SceneManager.LoadScene(1);
                break;
        }            
    }
    
    public void ThingUpgraded(string tag)
    {
        switch(tag)
        {
            case "Weapon":
                ThingsUpgraded[0] = true;
                TutorialStep("Gun Upgraded");
                break;
            case "Tower":
                ThingsUpgraded[1] = true;
                TutorialStep("Tower Upgraded");
                break;
        }
        
    }
    public void TutorialStep(string Event)
    {
        Debug.Log(Event);
        switch (Event)
        {
            case "First Tank Killed":
                Tanks[1].SetActive(true);
                break;
            case "First Moving Tank Killed":
                TextShooting.text = "A real sniper ! Now look at the new table behind you";
                LoadState(1);
                break;
            case "Gun Upgraded":
                TextTables.text = "Your gun is upgraded ! try it on this Tank";
                break;
            case "Second Idle Tank Killed":
                TextTables.text = "Your gun is now really powerfull. Now go buy your first tower";
                LoadState(2);
                break;            
            case "Tower Bought 0":
                PlacePoints[0].SetActive(true);
                break;
            case "Tower placed Canon":
                Enemys[0].SetActive(true);
                Enemys[0].GetComponent<EnemyManager>().alive = true;
                break;
            case "Tower Bought 1":
                ShopButtons[1].SetActive(true);
                break;
            case "Tower placed Missile":
                Enemys[1].SetActive(true);
                Enemys[1].GetComponent<EnemyManager>().alive = true;
                break;
            case "Tower Bought 2":
                ShopButtons[2].SetActive(true);
                break;
            case "Tower placed Rail":
                Enemys[2].SetActive(true);
                Enemys[2].GetComponent<EnemyManager>().alive = true;
                break;
            case "Fourth Moving Tank Killed":
                TextShooting.text = "Well done ! Now try to upgrade your towers for Power increase";
                LoadState(3);
                break;
            case "Tower Upgraded":
                // Set active the button to finish tutorial
                break;
        }
    }
}
