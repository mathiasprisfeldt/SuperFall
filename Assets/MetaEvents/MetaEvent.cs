using System;
using UnityEngine;

public class MetaEvent : MonoBehaviour
{
    private bool _grabbed;
    private Vector2 _grabbedPoint;

    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; set; }

    [field: SerializeField] public float RaiseAmount { get; set; }

    private void Start()
    {
        SpriteRenderer.color = RaiseAmount > 0 ? Color.green : Color.red;
    }

    void Update()
    {
        var mousePosInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckIfGrabbed();

        if (Input.GetMouseButtonUp(0))
        {
            _grabbed = false;
        }

        if (_grabbed)
        {
            transform.position = new Vector3(mousePosInWorldSpace.x - _grabbedPoint.x, mousePosInWorldSpace.y - _grabbedPoint.y);
        }
    }

    private void CheckIfGrabbed()
    {
        if (_grabbed) return;

        var raycastHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (raycastHit && raycastHit.transform.gameObject == gameObject && Input.GetMouseButtonDown(0))
        {
            _grabbed = true;
            _grabbedPoint = transform.InverseTransformPoint(raycastHit.point);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player)
        {
            player.StartRaise(RaiseAmount);
            Destroy(gameObject);
        }
    }
}
