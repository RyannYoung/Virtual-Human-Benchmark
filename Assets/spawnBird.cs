using UnityEngine;
using Random = UnityEngine.Random;

public class spawnBird : MonoBehaviour
{

    public GameObject bird;
    public Vector3 spawnZone;
    public float birdSpeed;
    public float birdScale;

    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBird();
    }

    void SpawnBird()
    {
        // var spawn = Instantiate(bird, _transform);
        // var spawnedBird = spawn.GetComponent<BirdMovement>();
        // spawnedBird.speed = birdSpeed;
        //
        // var randx = Random.Range(-spawnZone.x, spawnZone.x);
        // var randy = Random.Range(-spawnZone.y, spawnZone.y);
        // var randz = Random.Range(-spawnZone.z, spawnZone.z);
        //
        // spawnedBird.transform.position = new Vector3(randx, randy, randz);
    }
}
