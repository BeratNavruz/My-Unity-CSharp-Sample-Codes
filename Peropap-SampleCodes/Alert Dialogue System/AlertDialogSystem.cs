using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class AlertDialogSystem : MonoBehaviour
{
    private Button buttonAlert;
    private TextMeshProUGUI textAlert;
    public string alertButtonGoToSceneName;

    private void Awake()
    {
        textAlert = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        buttonAlert = transform.GetChild(0).GetChild(1).gameObject.GetComponent<Button>();
        buttonAlert.onClick.AddListener(delegate { ButtonAlertFunc(alertButtonGoToSceneName); });
    }

    private void Start()
    {
        transform.GetChild(0).DOScale(Vector3.zero, .25f).From().SetEase(Ease.OutElastic);
    }

    private void ButtonAlertFunc(string sceneName)
    {
        if (string.IsNullOrWhiteSpace(sceneName))
            Destroy(transform.parent.gameObject);
        else
            SceneManager.LoadSceneAsync(sceneName);
    }

    public void TextAlertFunc(string _textAlert)
    {
        textAlert.text = _textAlert;
    }
}
