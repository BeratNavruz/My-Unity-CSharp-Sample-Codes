using System.Collections;
using UnityEngine;

public class AnimalSoundControl : MonoBehaviour
{
    private AudioSource _audioSource;
    private Coroutine _fadeCoroutine;
    private bool _isInsideTrigger = false;

    [SerializeField] private float fadeDuration = 1.0f;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.volume = 0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CameraRayCollider") && !_isInsideTrigger)
        {
            _isInsideTrigger = true;

            if (_fadeCoroutine != null)
                StopCoroutine(_fadeCoroutine);

            _fadeCoroutine = StartCoroutine(FadeAudio(0f, 1f, fadeDuration, playSound: true));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CameraRayCollider") && _isInsideTrigger)
        {
            _isInsideTrigger = false;

            if (_fadeCoroutine != null)
                StopCoroutine(_fadeCoroutine);

            _fadeCoroutine = StartCoroutine(FadeAudio(1f, 0f, fadeDuration, playSound: false));
        }
    }

    IEnumerator FadeAudio(float startVolume, float targetVolume, float duration, bool playSound)
    {
        if (playSound && !_audioSource.isPlaying)
            _audioSource.Play();

        float currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }

        _audioSource.volume = targetVolume;

        if (targetVolume <= 0f)
            _audioSource.Stop();
    }
}
