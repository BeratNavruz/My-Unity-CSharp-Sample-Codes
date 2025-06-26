using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class AlertDialogueSO : ScriptableObject
{
    public GameObject AlertPrefab;

    public void AlertDialogueFunc(string _textAlert,  string alertButtonGoToSceneName)
    {
        GameObject newAlert = Instantiate(AlertPrefab, CanvasTransform());
        newAlert.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        newAlert.GetComponent<AlertDialogSystem>().TextAlertFunc(_textAlert);
        newAlert.GetComponent<AlertDialogSystem>().alertButtonGoToSceneName = alertButtonGoToSceneName;
    }

    Transform CanvasTransform()
    {
        Canvas canvas = new GameObject("Canvas").AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasScaler = canvas.gameObject.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
        canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        canvasScaler.referencePixelsPerUnit = 100;
        canvas.gameObject.AddComponent<GraphicRaycaster>();

        return canvas.transform;
    }
}
