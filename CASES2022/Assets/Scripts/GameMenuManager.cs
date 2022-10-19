using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] InputActionProperty showButton;
    [SerializeField] Transform head;
    float spawnDistance = 3f;

    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            // Spawn the menu at the head position, but offset by the spawn distance.
            menu.SetActive(!menu.activeSelf);
        }
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }

  

}
