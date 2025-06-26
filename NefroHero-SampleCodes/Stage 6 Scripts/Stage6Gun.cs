using UnityEngine;

public class Stage6Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    private GameObject bullet;
    public float bulletSpeed = 10;
    AudioSource audioSource;
    public Stage6CharacterController stage6CharacterController;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bulletSpeed += stage6CharacterController.running_speed;
    }

    public void SpawnBullet()
    {
        if (stage6CharacterController.isRun)
        {
            bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.name = "Bullet";
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            audioSource.Play();
        }
    }
}
