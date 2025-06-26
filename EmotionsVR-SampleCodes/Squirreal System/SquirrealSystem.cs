using System.Collections;
using UnityEngine;

public class SquirrealSystem : MonoBehaviour
{
    public static SquirrealSystem Instance { get; private set; }

    [SerializeField] private SquirrealControl _squirrealControl;
    [SerializeField] private GameObject _squirrealTrigger;

    private void Awake()
    {
        Instance = this;
    }

    public void OnTrigger()
    {
        _squirrealTrigger.SetActive(false);
        _squirrealControl.TargetSwapper(_squirrealControl._playerTarget);
        _squirrealControl.StartControl(true);
    }

    public IEnumerator IEWait()
    {
        StageManager2.Instance.PlayerMovementEnabled(false);
        SoundControl.Instance.NextSound();
        yield return new WaitForSeconds(SoundControl.Instance.AudioClip[SoundControl.Instance.SoundIndex - 1].length + 10);
        SoundControl.Instance.NextSound();
        yield return new WaitForSeconds(SoundControl.Instance.AudioClip[SoundControl.Instance.SoundIndex - 1].length + 10);
        _squirrealControl.TargetSwapper(_squirrealControl._finishTarget);
        _squirrealControl.StartControl(true);
        StageManager2.Instance.PlayerMovementEnabled(true);
    }
}
