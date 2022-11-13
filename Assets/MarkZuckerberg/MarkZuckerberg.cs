using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkZuckerberg : MonoBehaviour
{
    void Update()
    {
        var y = Mathf.Max(ServiceProvider.Player.ShowWinElements, ServiceProvider.Player.transform.position.y + 2);
        transform.position = new Vector3(ServiceProvider.Player.transform.position.x, y, 0);
    }
}
