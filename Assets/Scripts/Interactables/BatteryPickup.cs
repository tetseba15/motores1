using UnityEngine;

public class BatteryPickup : MonoBehaviour, IInteractable
{
    [Header("Configuración")]
    [SerializeField] private float _rechargeAmount = 25f; 
    [SerializeField] private string _promptText = "[E] Tomar pilas";

    public string GetInteractPrompt() => _promptText;

    public void Interact()
    {
        // Buscamos al jugador por Tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            PlayerFlashlight flashlight = player.GetComponent<PlayerFlashlight>();

            if (flashlight != null)
            {
                flashlight.RechargeBattery(_rechargeAmount);

                // FX HERE

                Debug.Log($"Linterna recargada con {_rechargeAmount}%");

                Destroy(gameObject);
            }
        }
    }
}