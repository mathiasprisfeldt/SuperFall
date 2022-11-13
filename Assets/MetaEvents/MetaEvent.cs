using System;
using UnityEngine;
using UnityEngine.Rendering;

public class MetaEvent : MonoBehaviour
{
    public static int SortingOrderCounter = 1;

    private int _initialSortingOrder;
    private bool _grabbed;
    private Vector2 _grabbedPoint;
    private float _raiseAmount;
    private AudioClip _audioClip;

    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; set; }
    [field: SerializeField] public SpriteRenderer BorderSpriteRenderer { get; set; }
    [field: SerializeField] public SortingGroup SortingGroup { get; set; }
    [field: SerializeField] public SpriteMask SpriteMask { get; set; }

    public void Configure(MetaEventData data)
    {
        SpriteRenderer.sprite = data.Image;

        _audioClip = data.SFX;
        _raiseAmount = data.RaiseAmount;

        BorderSpriteRenderer.color = _raiseAmount > 0 ? Color.green : Color.red;
        _initialSortingOrder = SortingOrderCounter++;
        SetSortingOrder(_initialSortingOrder);
    }

    private void SetSortingOrder(int sortingOrder)
    {
        SortingGroup.sortingOrder = sortingOrder;
        SpriteMask.frontSortingOrder = sortingOrder;
    }

    void Update()
    {
        var mousePosInWorldSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        CheckIfGrabbed();

        if (Input.GetMouseButtonUp(0))
        {
            _grabbed = false;
            SetSortingOrder(_initialSortingOrder);
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
            SetSortingOrder(9999);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player)
        {
            player.StartRaise(_raiseAmount);
            ServiceProvider.SoundManager.Play(_audioClip);
            Destroy(gameObject);
        }
    }
}
