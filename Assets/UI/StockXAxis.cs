using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StockXAxis : MonoBehaviour
{
    private string[] _months = {
        "J",
        "F",
        "M",
        "A",
        "M",
        "J",
        "J",
        "A",
        "S",
        "O",
        "N",
        "D"
    };

    [field: SerializeField] public TextMeshPro StockPricePrefab { get; set; }
    [field: SerializeField] public TextMeshPro YearText { get; set; }

    // Update is called once per frame
    void Start()
    {
        var worldMinX = -1500;
        var worldMaxX = 1500;
        var incrementor = 10;
        var currentMonthIndex = 0;

        for (int x = worldMinX; x < worldMaxX; x += incrementor)
        {
            SpawnNewStockPrice(x, _months[currentMonthIndex]);
            currentMonthIndex++;

            if (currentMonthIndex >= _months.Length)
                currentMonthIndex = 0;
        }
    }

    private void Update()
    {
        var yPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        transform.position = new Vector3(transform.position.x, yPosition.y + 1);

        var yearTextWorldCorners = new Vector3[4];
        YearText.rectTransform.GetWorldCorners(yearTextWorldCorners);

        var xScreenMiddle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, Camera.main.nearClipPlane)).x;
        var currentYear = Mathf.FloorToInt((ServiceProvider.Player.transform.position.x + 60) / 120);
        YearText.text = (2022 + currentYear).ToString();
        var widthOffset = (yearTextWorldCorners[3] - yearTextWorldCorners[0]) / 4;
        Debug.Log(widthOffset);
        YearText.transform.position = new Vector3(xScreenMiddle + widthOffset.x, YearText.transform.position.y);
    }

    void SpawnNewStockPrice(float xPosition, string text)
    {
        var stockPriceText = Instantiate(StockPricePrefab, transform);
        stockPriceText.text = text;
        stockPriceText.transform.position = new Vector3(xPosition, 0);
    }
}
