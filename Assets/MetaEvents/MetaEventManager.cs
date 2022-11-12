using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MetaEventManager : MonoBehaviour
{
    private float _spawnTimer;

    [field: SerializeField] public GameObject MetaEventPrefab { get; set; }
    [field: SerializeField] public float SpawnIntervalInMilliseconds { get; set; }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime * 1000;
        if (_spawnTimer > 0) return;

        var cameraMain = Camera.main;
        var minY = cameraMain.ScreenToWorldPoint(new Vector3(Screen.width, 0, cameraMain.nearClipPlane));
        var maxY = cameraMain.ScreenToWorldPoint(new Vector3(Screen.height, Screen.height, 0));
        var x = minY.x;
        var y = Random.Range(minY.y, maxY.y);

        var metaEvent = Instantiate(MetaEventPrefab).GetComponent<MetaEvent>();
        metaEvent.transform.position = new Vector3(x, y, 0);
        metaEvent.RaiseAmount = Random.Range(-15, 15);

        _spawnTimer = SpawnIntervalInMilliseconds;
    }
}
