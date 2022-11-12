using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StockYAxis : MonoBehaviour
{
    [field: SerializeField] public TextMeshPro StockPricePrefab { get; set; }

    // Update is called once per frame
    void Start()
    {
        var worldMinY = -1500;
        var worldMaxY = 1500;
        var incrementor = 10;

        for (int y = worldMinY; y < worldMaxY; y += incrementor)
        {
            SpawnNewStockPrice(y);
        }

    }

    private void Update()
    {
        var xPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        transform.position = new Vector3(xPosition.x + 3, transform.position.y);
    }

    void SpawnNewStockPrice(float yPosition)
    {
        var stockPriceText = Instantiate(StockPricePrefab, transform);
        stockPriceText.text = yPosition.ToString();
        stockPriceText.transform.position = new Vector3(0, yPosition);
    }
}
