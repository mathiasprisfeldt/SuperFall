using UnityEngine;

public class PlayerTrailPart : MonoBehaviour
{
    void Update()
    {
        var cameraMinXInWorldSpace = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.rect.xMin, 0, 0)) ;
        if (transform.position.x < cameraMinXInWorldSpace.x)
        {
            DestroyImmediate(gameObject);
        }
    }
}
