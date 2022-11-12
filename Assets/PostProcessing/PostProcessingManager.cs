using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class PostProcessingManager : MonoBehaviour
{
    private Volume _volume;

    [field: SerializeField] public Player Player { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_volume.sharedProfile.TryGet<LensDistortion>(out var lensDistortion))
        {
            var targetIntensity = Player.Speed.Map(50, 200, 0, -.25f);
            var intensity = Mathf.Lerp(lensDistortion.intensity.value, targetIntensity, Time.deltaTime * 100);
            lensDistortion.intensity.Override(intensity);
        }

        if (_volume.sharedProfile.TryGet<ChromaticAberration>(out var chromaticAberration))
        {
            var targetChromaticAberration = Player.VerticalSpeed.Map(-25, -100, 0, 1);
            var intensity = Mathf.Lerp(chromaticAberration.intensity.value, targetChromaticAberration, Time.deltaTime * 100);
            chromaticAberration.intensity.Override(intensity);
        }
    }
}
