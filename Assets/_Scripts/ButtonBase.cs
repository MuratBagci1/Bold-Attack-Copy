using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
    protected Button button;

    private void Awake()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("No Button component found on " + gameObject.name);
            return;
        }

        button.onClick.AddListener(ButtonPressed);
    }

    protected virtual void ButtonPressed()
    {

        Debug.Log("Hello World!");
    }
}