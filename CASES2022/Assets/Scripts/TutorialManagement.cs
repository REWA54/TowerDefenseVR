using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManagement : MonoBehaviour
{
    // The tutorial is separated in 4 states : learn to pick up the gun & use it; learn to upgrade the gun; learn to purshase towers and place them; learn to upgrade towers.

    public int tutorialState;
    private int highestTutorialState;
    public GameObject UpgradeButton;

    public TMP_Text TextTables;
    public GameObject ButtonTables;
    public TMP_Text TextButtonTables;

    public TMP_Text TextShooting;
    public GameObject ButtonShooting;
    public TMP_Text TextButtonShooting;

    public GameObject tableGun;
    public GameObject tableUpgrade;
    public GameObject tableShop;

    public GameObject[] Tanks;
    public GameObject[] PlacePoints;
    public GameObject[] ShopButtons;
    public GameObject[] Enemys;

    private List<bool> ThingsUpgraded;
    int ButtonPressedState;

    void Start()
    {
        TextShooting.text = "Welcome to the TowerDefense VR Tutorial";
        TextTables.text = "Welcome to the TowerDefense VR Tutorial";

        tableGun.SetActive(false);
        tableShop.SetActive(false);
        tableUpgrade.SetActive(false);

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

    [ContextMenu("Button Table")]
    void ButtonsWithInspector0()
    {
        ButtonPressed(0);
    }
    [ContextMenu("Button Shoot")]
    void ButtonsWithInspector1()
    {
        ButtonPressed(1);
    }
    public void ButtonPressed(int index)
    {
        switch (index)
        {
            case 0:
                tableGun.SetActive(true);
                TextTables.text = "First, grab the gun and kill the Tank behind you";
                ButtonTables.gameObject.SetActive(false);
                break;
            case 1:
                TutorialStep("Finish Tutorial");
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

    public void LoadState(int state)
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
                tableUpgrade.SetActive(false);
                ShopButtons[0].SetActive(true);
                break;
            case 3:
                tableShop.SetActive(false);
                tableUpgrade.SetActive(true);
                UpgradeButton.SetActive(true);
                break;
            case 4:
                SceneManager.LoadScene(1);
                break;
        }
    }

    public void ThingUpgraded(string tag)
    {
        switch (tag)
        {
            case "Weapon":
                //ThingsUpgraded[0] = true;
                TutorialStep("Gun Upgraded");
                break;
            case "Tower":
                //ThingsUpgraded[1] = true;
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
                TextShooting.text = "A real sniper ! Now look at the new table behind you";
                TextTables.text = "Here is the Table for upgrade your Weapon. Place your weapon on the table and press the Upgrade button";
                LoadState(1);
                break;
            case "Gun Upgraded":
                TextTables.text = "Your gun is upgraded ! try it on this Tank";
                TextShooting.text = "Kill the Tank with your upraded weapon";
                Tanks[1].SetActive(true);
                UpgradeButton.SetActive(false);
                break;
            case "Second Idle Tank Killed":
                TextShooting.text = "Your gun is now really powerfull. Now kill the moving one";
                TextTables.text = "Your gun is now really powerfull. Now kill the moving one";
                Tanks[2].SetActive(true);
                break;
            case "First Moving Tank Killed":
                TextShooting.text = "Nice kill, now let's buy your first Tower";
                TextTables.text = "Press the button to buy a Tower";
                LoadState(2);
                break;
            case "Tower Bought 0":
                TextTables.text = "You bought your first tower, now place it on the circle behind you";
                TextShooting.text = "Place the tower on the circle";
                PlacePoints[0].SetActive(true);
                ShopButtons[0].SetActive(false);
                break;
            case "Tower placed Canon":
                TextShooting.text = "Tower placed, now let see if it can kill the enemy";
                Enemys[0].SetActive(true);
                Enemys[0].GetComponent<EnemyManager>().alive = true;
                break;
            case "Enemy 0 killed":
                TextShooting.text = "Enemy killed, let's buy another tower";
                TextTables.text = "Press the button to buy a Tower";
                ShopButtons[1].SetActive(true);
                break;
            case "Tower Bought 1":
                TextShooting.text = "Place the tower on the available circle or move the first tower by grabbing it";
                TextTables.text = "You bought a Missile tower, high damages but slow reloading";
                PlacePoints[1].SetActive(true);
                ShopButtons[1].SetActive(false);
                break;
            case "Tower placed Missile":
                TextShooting.text = "Tower placed, now let see if it can kill the enemy";
                Enemys[1].SetActive(true);
                Enemys[1].GetComponent<EnemyManager>().alive = true;
                break;
            case "Enemy 1 killed":
                TextShooting.text = "Enemy killed, let's buy another tower";
                TextTables.text = "Press the button to buy a Tower";
                ShopButtons[2].SetActive(true);
                break;
            case "Tower Bought 2":
                TextShooting.text = "Place the tower on the available circle or move the first tower by grabbing it";
                TextTables.text = "You bought a Rail tower, Low damages but continuous";
                PlacePoints[2].SetActive(true);
                ShopButtons[2].SetActive(false);
                break;
            case "Tower placed Rail":
                TextShooting.text = "Tower placed, now let see if it can kill the enemy";
                Enemys[2].SetActive(true);
                Enemys[2].GetComponent<EnemyManager>().alive = true;
                break;
            case "Enemy 3 killed":
                TextShooting.text = "Well done ! Now try to upgrade your towers for Power increase";
                TextTables.text = "Here is the Table for upgrade your Tower. Place one Tower on the table and press the Upgrade button";                
                LoadState(3);
                break;
            case "Tower Upgraded":
                TextTables.text = "Tower upgraded ! now put it back to a circle and it will kill enemy faster";
                TextShooting.text = "Place the tower on the available circle or move the first tower by grabbing it"; 
                UpgradeButton.SetActive(false);
                break;
            case "Tower upgraded placed":
                TextShooting.text = "Tower placed, now let see if it can kill the enemy";
                Enemys[3].SetActive(true);
                Enemys[3].GetComponent<EnemyManager>().alive = true;
                break;
            case "Enemy 4 killed":
                TextShooting.text = "Tutorial finished. Press the button for start a new Game";
                ButtonShooting.SetActive(true);
                break;
            case "Finish Tutorial":
                LoadState(4);
                break;
        }
    }
}
