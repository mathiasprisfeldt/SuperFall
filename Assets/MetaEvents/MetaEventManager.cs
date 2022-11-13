using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MetaEventManager : MonoBehaviour
{
    private float _spawnTimer;

    [field: SerializeField] public GameObject MetaEventPrefab { get; set; }
    [field: SerializeField] public float SpawnIntervalInMilliseconds { get; set; }

    [field: SerializeField] public List<MetaEventData> MetaEventDatas { get; set; }

    public bool GameIsStarted { get; set; }

    private void Update()
    {
        if (!GameIsStarted) return;

        _spawnTimer -= Time.deltaTime * 1000;
        if (_spawnTimer > 0) return;

        var eventsToChooseFrom = MetaEventDatas.ToList();
        var negativeEventChance = Mathf.Max(0.5f, ServiceProvider.Player.transform.position.y.Map(100, 600, 0.0f, 0.9f));
        var spawnNegativeEvent = Random.Range(0.0f, 1.0f) > 1 - negativeEventChance;

        eventsToChooseFrom = eventsToChooseFrom.Where(x => x.IsPositive == !spawnNegativeEvent).ToList();

        var cameraMain = Camera.main;
        var topLeft = cameraMain.ScreenToWorldPoint(new Vector3(0, 0, cameraMain.nearClipPlane));
        var bottomRight = cameraMain.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cameraMain.nearClipPlane));
        var spawnMultiplier = 1f;
        var x = Random.Range(topLeft.x * spawnMultiplier, bottomRight.x * spawnMultiplier);
        var y = Random.Range(topLeft.y * spawnMultiplier, bottomRight.y * spawnMultiplier);

        var metaEvent = Instantiate(MetaEventPrefab).GetComponent<MetaEvent>();
        metaEvent.transform.position = new Vector3(x, y, 0);
        metaEvent.Configure(eventsToChooseFrom[Random.Range(0, eventsToChooseFrom.Count)]);

        _spawnTimer = SpawnIntervalInMilliseconds;
    }

    public void StartGame()
    {
        ServiceProvider.Player.StartGame();
        GameIsStarted = true;
    }
}
