using UnityEngine;

public class FixingObjectWall : MonoBehaviour
{
    public Transform rayStartPoint;
    public float rayLength;
    public GameObject prefab;
    public bool isFixing = false;

    void Update()
    {
        Ray ray = new Ray(rayStartPoint.position, rayStartPoint.forward);

        if (!isFixing && Physics.Raycast(ray, out RaycastHit hit))
        {
            prefab.transform.position = hit.point;
            prefab.transform.rotation = Quaternion.LookRotation(-hit.normal);
        }
    }
}
