using UnityEngine;
public class Note : MonoBehaviour, IInteractable
{
    [Header("Interact prompt")]
    [SerializeField] private string _promptText = "[E] Recoger objeto";
    [Header("Note Content")]
    [SerializeField, TextArea(3, 10)]
    private string _noteContent;
    [Header("Mannequin")]
    [SerializeField] private GameObject _mannequin;

    public string GetInteractPrompt()
    {
        return _promptText;
    }
    public void Interact()
    {
        if (!UIManager.Instance.IsReadingNote)
        {
            UIManager.Instance.ShowNote(_noteContent);
            UIManager.Instance.HideInteractPrompt();
            if (_mannequin != null)
            {
                _mannequin.SetActive(true);
            }
        }
    }
}