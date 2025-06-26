using UnityEngine;

public class Stage6Bullet : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name != "Character Parent")
            Destroy(gameObject);
    }
}