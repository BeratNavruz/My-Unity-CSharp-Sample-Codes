using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField tMP_InputField;

    public float distance = .5f;
    public float verticalOffset = -.5f;

    public Transform positionSource;

    void Start()
    {
        tMP_InputField = GetComponent<TMP_InputField>();
        tMP_InputField.onSelect.AddListener(x => OpenKeyboard());
    }

    public void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = tMP_InputField;
        NonNativeKeyboard.Instance.PresentKeyboard(tMP_InputField.text);

        Vector3 direction = positionSource.forward;
        direction.y = 0;
        direction.Normalize();

        Vector3 targetPosition = positionSource.position + direction * distance + Vector3.up * verticalOffset;

        NonNativeKeyboard.Instance.RepositionKeyboard(targetPosition);

        SetCaretColorAlpha(1);

        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;
    }

    private void Instance_OnClosed(object sender, System.EventArgs e)
    {
        SetCaretColorAlpha(0);
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }

    public void SetCaretColorAlpha(float value)
    {
        tMP_InputField.customCaretColor = true;
        Color caretColor = tMP_InputField.caretColor;
        caretColor.a = value;
        tMP_InputField.caretColor = caretColor;
    }
}
