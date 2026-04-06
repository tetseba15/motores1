using UnityEngine;

public class TestDoor : MonoBehaviour, IInteractable
{
    private bool _isOpen = false;

    public string GetInteractPrompt()
    {
        return _isOpen ? "Cerrar puerta" : "Abrir puerta";
    }

    public void Interact()
    {
        _isOpen = !_isOpen;
        Debug.Log("Estado de la puerta modificado: " + _isOpen);

        // Animation or rotation logic...

    }
}