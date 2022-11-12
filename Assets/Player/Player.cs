using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public ParticleSystem ParticleSystem { get; set; }

    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float VerticalSpeed { get; set; }

    private float _modifiedVerticalSpeed;

    public bool Raising => _modifiedVerticalSpeed < 0;

    void Start()
    {
        _modifiedVerticalSpeed = VerticalSpeed;
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

        var particleSystemMain = ParticleSystem.main;
        particleSystemMain.startColor = new ParticleSystem.MinMaxGradient(Raising ? Color.green: Color.red);
        particleSystemMain.startRotationZ = lookDirection.x;
    }

    public void StartRaise(float amount)
    {
        _modifiedVerticalSpeed = amount;

        Task.Run(async () =>
        {
            await Task.Delay(2000);
            _modifiedVerticalSpeed = VerticalSpeed;
        });
    }
}
