using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private PlayerInventory.ItemType _requiredKey = PlayerInventory.ItemType.MansionKey;
    [SerializeField] private string _lockedMessage = "Está cerrada con llave.";
    [SerializeField] private string _unlockedMessage = "Abrir puerta";

    private bool _isOpened = false;
    private PlayerInventory _playerInventory;

    private void Start()
    {
        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }

    public string GetInteractPrompt()
    {
        if (_isOpened) return "";

        // show block status
        return _playerInventory.HasItem(_requiredKey) ? _unlockedMessage : _lockedMessage;
    }

    public void Interact()
    {
        if (_isOpened) return;

        if (_playerInventory.HasItem(_requiredKey))
        {
            OpenDoor();
        }
        else
        {
            // SFX
            Debug.Log("La puerta no cede...");
        }
    }

    private void OpenDoor()
    {
        _isOpened = true;
        Debug.Log("Puerta abierta. Bienvenido a la mansión.");

        // Animation?
        transform.Rotate(0, -90, 0); 
    }
}