using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float WinY = 500;
    public float ShowWinElements = 400;

    private bool _isRaiseTimerRunning;
    private LineRenderer _currentTrail;
    private float _raiseTimer;

    public float Speed { get; private set; }
    public float VerticalSpeed { get; private set; }

    [field: SerializeField] public GameObject LineRendererPrefab { get; set; }

    [field: SerializeField] public float DefaultSpeed { get; set; }
    [field: SerializeField] public float DefaultVerticalSpeed { get; set; }
    [field: SerializeField] public float RaiseTimerIntervalInMilliseconds { get; set; }

    [field: SerializeField] public Color RaisingColor { get; set; }
    [field: SerializeField] public Color DecrasingColor { get; set; }
    [field: SerializeField] public Color DefaultColor { get; set; }

    [field: SerializeField] public float offsetInterval { get; set; }
    private float offsetTimer;
    private float offset;

    public GameObject Logo;
    public List<Sprite> Logos;
    public SpriteRenderer LogoSprite;
    private int logogIdx = 0;

    public bool Raising => VerticalSpeed < 0;
    public bool IsWinning => transform.position.y > ShowWinElements;

    void Start()
    {
        VerticalSpeed = 0;

        ChangeTrailDirection();
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

        // Player is winning, don't let him fall.
        if (IsWinning)
        {
            _raiseTimer = float.MaxValue;
            VerticalSpeed = Mathf.Max(VerticalSpeed, DefaultVerticalSpeed);
        }

        if (ServiceProvider.MetaEventManager.GameIsStarted && _isRaiseTimerRunning)
        {
            _raiseTimer -= Time.deltaTime * 1000;
            if (_raiseTimer <= 0)
            {
                VerticalSpeed = -10;
                ChangeTrailDirection();
                _isRaiseTimerRunning = false;
            }
        }

        Speed = Mathf.Max(0, Mathf.Abs(VerticalSpeed) - DefaultVerticalSpeed) + DefaultSpeed;

        offsetTimer -= Time.deltaTime;
        if (offsetTimer <= 0)
        {
            offsetTimer = offsetInterval;
            offset = Random.Range(-3f, 3f);
        }

        updateLogo();

        if (transform.position.y < -150)
            GameOver();

        if (transform.position.y > WinY)
            Win();

        _currentTrail.SetPosition(1, transform.position);
    }

    private void Win()
    {
        SceneManager.LoadScene("WinScene");
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    void updateLogo() {
        Logo.transform.position = transform.position;
        float maxSpriteY = 0;
        float minSpriteY = -100;
        float spriteYLength = (maxSpriteY - minSpriteY) / Logos.Count;
        int newIdx = (int)((maxSpriteY - transform.position.y) / spriteYLength);
        newIdx = System.Math.Max(System.Math.Min(newIdx, Logos.Count - 1), 0);
        if (newIdx != logogIdx) {
            logogIdx = newIdx;
            LogoSprite.sprite = Logos[logogIdx];
		}
    }

    public void StartRaise(float amount)
    {
        if (IsWinning) return;

        _raiseTimer = RaiseTimerIntervalInMilliseconds;
        VerticalSpeed += -amount;
        ChangeTrailDirection();
        _isRaiseTimerRunning = true;
    }

    private void ChangeTrailDirection()
    {
        _currentTrail = Instantiate(LineRendererPrefab, transform).GetComponent<LineRenderer>();
        _currentTrail.SetPosition(0, transform.position);

        var color = DefaultColor;
        if (VerticalSpeed != 0)
            color = Raising ? RaisingColor : DecrasingColor;

        _currentTrail.startColor = color;
        _currentTrail.endColor = color;
    }

    public void StartGame()
    {
        VerticalSpeed = DefaultVerticalSpeed;
        ChangeTrailDirection();
    }
}
