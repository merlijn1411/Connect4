using UnityEngine;
using UnityEngine.UI;

public class ButtonActivator : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    private void Start()
    {
        _buttons = GetComponentsInChildren<Button>();
    }

    public void DeactivateButtons()
    {
        foreach (var button in _buttons)
        {
            button.enabled = false;
        }
    }
}
