using UnityEngine;

[CreateAssetMenu(menuName = "MetaEvent")]
public class MetaEventData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; set; }
    [field: SerializeField] public AudioClip SFX { get; set; }
    [field: SerializeField] public float RaiseAmount { get; set; }
}
