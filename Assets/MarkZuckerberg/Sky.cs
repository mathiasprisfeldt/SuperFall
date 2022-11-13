using UnityEngine;

public class Sky : MonoBehaviour
{
    void Update()
    {
        var y = Mathf.Max(ServiceProvider.Player.ShowWinElements, ServiceProvider.Player.transform.position.y - 5);
        transform.position = new Vector3(ServiceProvider.Player.transform.position.x, y, 0);
    }
}
