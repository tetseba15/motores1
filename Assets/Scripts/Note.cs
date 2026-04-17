using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [Header("Note Content")]

    [SerializeField, TextArea(3, 10)]
    private string _noteContent;

    public string GetInteractPrompt()
    {
        return "Leer nota de mi mentor";
    }

    public void Interact()
    {
        if (!UIManager.Instance.IsReadingNote)
        {
            UIManager.Instance.ShowNote(_noteContent);
        }
    }
}
