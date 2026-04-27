using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventory.ItemType _itemType;
    [SerializeField] private string _promptText = "Recoger objeto";

    public string GetInteractPrompt() => _promptText;

    public void Interact()
    {
        PlayerInventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddItem(_itemType);

            // Turn on flashlight on pickup?
            // if (_itemType == PlayerInventory.ItemType.Flashlight) { ... }

            Destroy(gameObject); 
        }
    }
}
