using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "MetaEvent")]
public class MetaEventData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; set; }
    [field: SerializeField] public AudioClip Sound { get; set; }
    [field: SerializeField] public float RaiseAmount { get; set; }
}
