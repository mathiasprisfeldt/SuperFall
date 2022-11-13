using UnityEngine;

public class TimeStopManager : MonoBehaviour
{
    private Vector3 _previousMousePosition;

    [field: SerializeField] public float MaxMagnitude { get; set; }
    [field: SerializeField] private float MinimumTimeScale { get; set; }
    [field: SerializeField]  private float MaximumTimeScale { get; set; }

    void Update()
    {
        if (!ServiceProvider.MetaEventManager.GameIsStarted) return;

        var currentMousePosition = Input.mousePosition;
        var mousePositionDelta = _previousMousePosition - currentMousePosition;
        var targetTimeScale = mousePositionDelta.magnitude.Map(0, MaxMagnitude, MinimumTimeScale, MaximumTimeScale);

        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, Time.deltaTime * 100);

        _previousMousePosition = Input.mousePosition;
    }
}
