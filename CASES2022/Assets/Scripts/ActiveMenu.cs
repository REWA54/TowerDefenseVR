using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] InputActionProperty showButton;
    [SerializeField] Transform head;
    [SerializeField] float spawnDistance = 0.5f;

    void Update()
    {
        if (showButton.action.WasPressedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);
        }
        menu.transform.position = head.position + new Vector3(head.forward.x, 0, head.forward.z).normalized * spawnDistance;
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        menu.transform.forward *= -1;
    }
}
