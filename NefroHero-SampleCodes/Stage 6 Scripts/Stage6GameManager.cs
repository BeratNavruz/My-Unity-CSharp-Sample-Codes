using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage6GameManager : MonoBehaviour
{
    [System.Serializable]
    public class Enemy
    {
        public int total_enemy;
        public int dead_enemy;
        public TextMeshProUGUI deadText;
        public AudioClip deadSound;
    }

    [System.Serializable]
    public class GameProgress
    {
        public AnimationClip[] educationAnim;
        public GameObject canvasObj;
        private float animTotalLength;

        public float AnimationLengthFunc()
        {
            animTotalLength = 0;
            foreach (var anim in educationAnim)
            {
                animTotalLength += anim.length;
            }
            return animTotalLength;
            //return .1f;
        }
    }

    AudioSource audioSource;
    public AudioClip errorSound;
    public AudioClip moneySound;
    public AudioClip happySound;

    public Enemy enemy;
    public GameProgress[] gameProgress;
    public Stage6CharacterController stage6CharacterController;
    public AlertDialogueSO alertDialogueSO;
    PostApiManager postApiManager;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 1.0f / 60.0f;
        //postApiManager = PostApiManager.Instance;
        //postApiManager.alertDialogueSO = alertDialogueSO;
        audioSource = GetComponent<AudioSource>();
        StartIEGameProgressFunc(1);
    }

    public void EnemyControl()
    {
        enemy.dead_enemy++;
        enemy.deadText.text = enemy.dead_enemy.ToString();
        SoundFunc(enemy.deadSound);
        if (enemy.dead_enemy >= enemy.total_enemy)
        {
            StartCoroutine(FinishStageFunc());
        }
    }

    public void SoundFunc(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    public void StartIEGameProgressFunc(int index)
    {
        StartCoroutine(IEGameProgressFunc(index));
    }

    IEnumerator IEGameProgressFunc(int index)
    {
        gameProgress[index].canvasObj.SetActive(true);
        gameProgress[0].canvasObj.SetActive(false);
        yield return new WaitForSeconds(gameProgress[index].AnimationLengthFunc());
        gameProgress[0].canvasObj.SetActive(true);
        gameProgress[index].canvasObj.SetActive(false);
    }

    public IEnumerator ReLoadScene()
    {
        // postApiManager.isPostAPIWait = true;
        // postApiManager.SendScoreData("6", "", "");
        // while (postApiManager.isPostAPIWait)
        //     yield return null;

        yield return null;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator FinishStageFunc()
    {
        stage6CharacterController.isRun = false;
        stage6CharacterController.CharacterAnimChange("Idle");
        yield return new WaitForSeconds(1.5f);
        gameProgress[0].canvasObj.SetActive(false);
        SoundFunc(happySound);
        stage6CharacterController.CharacterAnimChange("BeHappy");
        // postApiManager.isPostAPIWait = true;
        // postApiManager.SendScoreData("6", "", "6");
        // while (postApiManager.isPostAPIWait)
        //     yield return null;

        Debug.Log("6. Aşama Bitti");
        yield return new WaitForSeconds(happySound.length);
        LevelManager.Instance.UnlockLevel(6);
        yield return new WaitForSeconds(1);
        SceneLoader.Instance.LoadMapScene();
    }
}
