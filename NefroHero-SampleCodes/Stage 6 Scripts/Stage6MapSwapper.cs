using UnityEngine;

public class Stage6MapSwapper : MonoBehaviour
{
    [SerializeField] GameObject swapper_prefab;
    public GameObject destroyed_object;
    GameObject newMap;
    [SerializeField] Vector3 plus_position;
    Vector3 next_position;

    public void Swapper()
    {
        next_position += plus_position;
        newMap = Instantiate(swapper_prefab, next_position, Quaternion.identity);
    }

    public void Destroyed()
    {
        Invoke(nameof(InvokeDestroyed), 1);
    }

    void InvokeDestroyed()
    {
        destroyed_object.SetActive(false);
        destroyed_object = newMap;
    }
}
