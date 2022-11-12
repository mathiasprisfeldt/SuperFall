using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject _trailPool;
    private float _modifiedVerticalSpeed;

    [field: SerializeField] public GameObject TrailPrefab { get; set; }

    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float VerticalSpeed { get; set; }

    public bool Raising => _modifiedVerticalSpeed < 0;

    void Start()
    {
        _modifiedVerticalSpeed = VerticalSpeed;

        _trailPool = new GameObject("PLAYER TRAIL POOL");
    }

    void Update()
    {
        var position = transform.position;
        var speed = Time.deltaTime * Speed;
        var verticalSpeed = _modifiedVerticalSpeed * Time.deltaTime;
        position = new Vector3(position.x + speed, position.y - verticalSpeed, position.z);
        transform.position = position;

        var lookDirection = Quaternion.LookRotation(new Vector3(Speed, _modifiedVerticalSpeed).normalized).eulerAngles;
        transform.rotation = Quaternion.Euler(0, 0, lookDirection.x);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartRaise(-15);
        }

        SpawnTrail();
    }

    public void StartRaise(float amount)
    {
        _modifiedVerticalSpeed = -amount;

        Task.Run(async () =>
        {
            await Task.Delay(2000);
            _modifiedVerticalSpeed = VerticalSpeed;
        });
    }

    public void SpawnTrail()
    {
        var newTrailChunk = Instantiate(TrailPrefab, _trailPool.transform);
        newTrailChunk.transform.position = transform.position;
        newTrailChunk.transform.rotation = transform.rotation;
        newTrailChunk.GetComponent<SpriteRenderer>().color = Raising ? Color.green : Color.red;
    }
}
