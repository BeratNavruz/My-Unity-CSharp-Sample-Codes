using System.Collections;
using UnityEngine;

public class PassthroughController : MonoBehaviour
{
    private OVRPassthroughLayer oVRPassthroughLayer;
    public Camera centerCamera;
    private bool waitNumerator = false;

    void Start()
    {
        oVRPassthroughLayer = GetComponent<OVRPassthroughLayer>();
    }

    void Update()
    {
        if (!waitNumerator && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            waitNumerator = true;
            FadePassthrough(0, 1);
        }

        if (!waitNumerator && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            waitNumerator = true;
            FadePassthrough(1, 1);
        }
    }

    public void FadePassthrough(float targetOpacity, float duration)
    {
        StartCoroutine(FadeEffect(targetOpacity, duration));
    }

    private IEnumerator FadeEffect(float targetOpacity, float duration)
    {
        float startOpacity = oVRPassthroughLayer.textureOpacity;
        float elapsedTime = 0f;

        if (targetOpacity == 1)
        {
            centerCamera.clearFlags = CameraClearFlags.SolidColor;
            centerCamera.backgroundColor = new Color(0, 0, 0, 0);
        }

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            oVRPassthroughLayer.textureOpacity = Mathf.Lerp(startOpacity, targetOpacity, elapsedTime / duration);
            yield return null;
        }

        oVRPassthroughLayer.textureOpacity = targetOpacity;

        if (targetOpacity == 0)
        {
            centerCamera.clearFlags = CameraClearFlags.Skybox;
        }
        waitNumerator = false;
    }
}
