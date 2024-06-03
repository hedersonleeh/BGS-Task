using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public bool canInteract { get; private set; }
    [SerializeField] private GameObject _interactionBubble;
    private IInterable _currentInteractable;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInterable>(out var interable))
        {
            canInteract = true;
            _currentInteractable = interable;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInterable>(out var interable))
        {
            canInteract = false;
            _currentInteractable = null;
        }
    }
    private void Update()
    {
        if (canInteract && !GlobalVariables.PlayerIsBusy && _currentInteractable != null && Input.GetButtonDown("Jump"))
        {
            _currentInteractable.Interact();
        }
    }
    private void LateUpdate()
    {
        _interactionBubble.gameObject.SetActive(canInteract);
    }
}
