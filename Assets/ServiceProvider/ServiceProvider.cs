using UnityEngine;

public class ServiceProvider : MonoBehaviour
{
    public static Player Player { get; set; }
    public static SoundManager SoundManager { get; set; }

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        SoundManager = FindObjectOfType<SoundManager>();
    }
}
