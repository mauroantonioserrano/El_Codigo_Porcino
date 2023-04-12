using UnityEngine;

public class InstanciarLobo : MonoBehaviour
{
    public GameObject objectToSpawn;
    public AudioClip spawnSound;
    public float spawnInterval = 180.0f; // 3 minutes in seconds
    private float timeSinceLastSpawn = 0.0f;

    public AudioSource audioSource; // New AudioSource variable

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnObject();
            timeSinceLastSpawn = 0.0f;
        }
    }

    private void SpawnObject()
    {
        Vector3 spawnPosition = transform.position;
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        if (spawnSound != null && audioSource != null) // Check if spawnSound and audioSource are not null
        {
            audioSource.PlayOneShot(spawnSound); // Use PlayOneShot instead of PlayClipAtPoint
        }
    }
}
