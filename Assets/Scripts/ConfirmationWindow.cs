using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationWindow : MonoBehaviour
{
    [SerializeField] private Image _itemIconConfirmationWindow;
    [SerializeField] private TextMeshProUGUI _itemDescriptionDisplay;
    [SerializeField] private TextMeshProUGUI _itemNameDisplay;
    [SerializeField] private Button _confirmationButton;
    [SerializeField] private TextMeshProUGUI _confirmationDisplay;
    [SerializeField] private Button _cancelButton;

    public void Updateinfo(string confirmMessage,ItemData data)
    {
        _itemIconConfirmationWindow.sprite = data.icon;
        _itemIconConfirmationWindow.color = data.tint;
        _itemIconConfirmationWindow.enabled = data.icon != null;
        _itemNameDisplay.text = data.displayName;
        _itemDescriptionDisplay.text = data.description;
        _confirmationDisplay.text = confirmMessage;
    }
    public void AssingCallbacks(System.Action onAccept, System.Action onCancel)
    {
        _confirmationButton.onClick.RemoveAllListeners();
        _cancelButton.onClick.RemoveAllListeners();
        _confirmationButton.onClick.AddListener(()=>onAccept.Invoke());
        _cancelButton.onClick.AddListener(()=> onCancel.Invoke());
    }
}
