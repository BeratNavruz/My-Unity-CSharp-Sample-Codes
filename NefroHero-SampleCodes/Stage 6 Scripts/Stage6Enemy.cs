using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage6Enemy : MonoBehaviour
{
    private Stage6GameManager stage6GameManager;

    private void Start()
    {
        stage6GameManager = FindObjectOfType<Stage6GameManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Character Parent")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (other.gameObject.name == "Bullet")
        {
            gameObject.SetActive(false);
            stage6GameManager.EnemyControl();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Character Parent")
        {
            gameObject.SetActive(false);
            stage6GameManager.EnemyControl();
        }
    }
}
