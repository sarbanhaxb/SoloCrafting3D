using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class UIBuildQueueButton : MonoBehaviour, IUIElement<UnitSO, UnityAction>
{
    [SerializeField] private Image icon;
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        Disable();
    }

    public void EnableFor(UnitSO item, UnityAction callback)
    {
        button.onClick.RemoveAllListeners();
        button.interactable = true;
        button.onClick.AddListener(callback);
        icon.gameObject.SetActive(true);
        icon.sprite = item.Icon;
    }

    public void Disable()
    {
        button.interactable = false;
        button.onClick.RemoveAllListeners();
        icon.gameObject.SetActive(false);
    }
}