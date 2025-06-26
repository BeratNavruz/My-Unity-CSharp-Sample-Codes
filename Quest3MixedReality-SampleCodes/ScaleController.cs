using System.Collections;
using UnityEngine;

public class ScaleController : MonoBehaviour
{
    [Header("Ayarlar")]
    [Tooltip("Ölçeği değiştirilecek hedef obje")]
    private GameObject targetObject;

    [Tooltip("Ölçek değişim hızı (birim/saniye)")]
    public float scaleSpeed = 1f;

    // [Tooltip("Artış mı yoksa azalış mı yapacak (true = artış, false = azalış)")]
    // public bool isIncreasing = true;

    [Tooltip("Ölçeği değiştirilecek objelerin bulunduğu panel")]
    public GameObject objectsPanel;
    [Tooltip("Seçilen objenin ölçeğinin değiştirileceği panel")]
    public GameObject scalePanel;

    private Coroutine scalingCoroutine;

    public void PanelButton(GameObject go)
    {
        targetObject = go;
        objectsPanel.SetActive(false);
        scalePanel.SetActive(true);
    }

    public void PanelBackButton()
    {
        scalePanel.SetActive(false);
        objectsPanel.SetActive(true);
        targetObject = null;
    }

    public void OnButtonDown(bool isIncreasing)
    {
        if (scalingCoroutine == null)
            scalingCoroutine = StartCoroutine(ScaleWhileHeld(isIncreasing));
    }

    public void OnButtonUp()
    {
        if (scalingCoroutine != null)
        {
            StopCoroutine(scalingCoroutine);
            scalingCoroutine = null;
        }
    }

    private IEnumerator ScaleWhileHeld(bool isIncreasing)
    {
        while (true)
        {
            if (targetObject != null)
            {
                Vector3 scaleChange = Vector3.one * scaleSpeed * Time.deltaTime;
                if (isIncreasing)
                    targetObject.transform.localScale += scaleChange;
                else
                    targetObject.transform.localScale -= scaleChange;
            }
            yield return null;
        }
    }
}
