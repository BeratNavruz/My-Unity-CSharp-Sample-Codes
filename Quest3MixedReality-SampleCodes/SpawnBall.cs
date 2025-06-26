using UnityEngine;

public enum ShootingStyle { SerialShot, SingleShot }

public class SpawnBall : MonoBehaviour
{
    ShootingStyle shootingStyle = ShootingStyle.SingleShot;
    public bool InterchangeableShot;

    public GameObject prefab;
    public float spawnSpeed = 5;
    public float serialShotInterval = 0.2f;
    private float nextSerialShotTime = 0f;

    void Update()
    {
        if (InterchangeableShot && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (shootingStyle == ShootingStyle.SerialShot)
                shootingStyle = ShootingStyle.SingleShot;

            else if (shootingStyle == ShootingStyle.SingleShot)
                shootingStyle = ShootingStyle.SerialShot;
        }

        if (shootingStyle == ShootingStyle.SingleShot && OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            SpawnSingleBall();
        }

        if (shootingStyle == ShootingStyle.SerialShot && OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            if (Time.time >= nextSerialShotTime)
            {
                SpawnSingleBall();
                nextSerialShotTime = Time.time + serialShotInterval;
            }
        }
    }

    void SpawnSingleBall()
    {
        GameObject spawnBall = Instantiate(prefab, transform.position, Quaternion.identity);
        Rigidbody spawnBallRB = spawnBall.GetComponent<Rigidbody>();
        spawnBallRB.velocity = transform.forward * spawnSpeed;
    }
}
