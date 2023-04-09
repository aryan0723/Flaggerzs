using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public Transform[] spawnTransform;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        spawnTransform = GetComponentsInChildren<Transform>();
    }



    public Transform GetSpawnPoint()
    {
        return spawnTransform[Random.RandomRange(0, spawnTransform.Length)];
    }
}
