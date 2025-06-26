using System.Collections;
using UnityEngine;

public class SpawnCube : MonoBehaviour
{
    public GameObject prefab;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            SpawnCubeFunc();
        }
    }

    void SpawnCubeFunc()
    {
        GameObject spawnCube = Instantiate(prefab, transform.position, Quaternion.identity);
        StartCoroutine(KinematicWait(spawnCube.GetComponent<Rigidbody>()));
    }

    IEnumerator KinematicWait(Rigidbody rb)
    {
        yield return new WaitForSeconds(2);
        rb.isKinematic = false;
    }
}
