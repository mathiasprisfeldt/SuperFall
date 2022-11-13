using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private float _raiseTimer;
    private GameObject _trailPool;
    private CancellationTokenSource _resetSpeedCancellationTokenSource;

    public float Speed { get; private set; }
    public float VerticalSpeed { get; private set; }

    [field: SerializeField] public GameObject TrailPrefab { get; set; }

    [field: SerializeField] public float DefaultSpeed { get; set; }
    [field: SerializeField] public float DefaultVerticalSpeed { get; set; }
    [field: SerializeField] public float RaiseTimerIntervalInMilliseconds { get; set; }

    [field: SerializeField] public float offsetInterval { get; set; }
    private float offsetTimer;
    private float offset;

    public GameObject Logo;
    public List<Sprite> Logos;
    public SpriteRenderer LogoSprite;
    private int logogIdx = 0;

    public bool Raising => VerticalSpeed < 0;

    void Start()
    {
        VerticalSpeed = DefaultVerticalSpeed;

        _trailPool = new GameObject("PLAYER TRAIL POOL");
    }

    void Update()
    {
        var position = transform.position;
        var speed = Time.deltaTime * Speed;
        var verticalSpeed = (VerticalSpeed + offset) * Time.deltaTime;
        position = new Vector3(position.x + speed, position.y - verticalSpeed, position.z);
        transform.position = position;

        var lookDirection = Quaternion.LookRotation(new Vector3(speed, verticalSpeed).normalized).eulerAngles;
        transform.rotation = Quaternion.Euler(0, 0, lookDirection.x);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            StartRaise(15);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            StartRaise(-15);

        _raiseTimer -= Time.deltaTime * 1000;
        if (_raiseTimer <= 0)
        {
            VerticalSpeed = DefaultVerticalSpeed;
        }

        Speed = Mathf.Max(0, Mathf.Abs(VerticalSpeed) - DefaultVerticalSpeed) + DefaultSpeed;

        SpawnTrail();

        offsetTimer -= Time.deltaTime;
        if (offsetTimer <= 0)
        {
            offsetTimer = offsetInterval;
            offset = Random.Range(-3f, 3f);
        }

        updateLogo();
    }

    void updateLogo() {
        Logo.transform.position = transform.position;

        int newIdx = 0;
        if (transform.position.y < -50)
        {
            newIdx = 1;
        }
        else {
            newIdx = 0;
		}

        if (newIdx != logogIdx) {
            logogIdx = newIdx;
            LogoSprite.sprite = Logos[logogIdx];
		}
    }

    public void StartRaise(float amount)
    {
        _raiseTimer = RaiseTimerIntervalInMilliseconds;
        VerticalSpeed += -amount;
    }

    public void SpawnTrail()
    {
        var newTrailChunk = Instantiate(TrailPrefab, _trailPool.transform);
        newTrailChunk.transform.position = transform.position;
        newTrailChunk.transform.rotation = transform.rotation;
        newTrailChunk.GetComponent<SpriteRenderer>().color = Raising ? Color.green : Color.red;
    }
}
