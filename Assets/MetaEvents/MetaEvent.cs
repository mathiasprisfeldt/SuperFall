using System;
using UnityEngine;
using UnityEngine.Rendering;

public class MetaEvent : MonoBehaviour
{
    public static int SortingOrderCounter = 1;

    private float _destroyTimer;
    private int _initialSortingOrder;
    private bool _grabbed;
    private Vector2 _grabbedPoint;
    private float _raiseAmount;
    private AudioClip _audioClip;
    private bool _isBeingDestroyed;

    [field: SerializeField] public SpriteRenderer SpriteRenderer { get; set; }
    [field: SerializeField] public SpriteRenderer BorderSpriteRenderer { get; set; }
    [field: SerializeField] public SortingGroup SortingGroup { get; set; }
    [field: SerializeField] public SpriteMask SpriteMask { get; set; }
    [field: SerializeField] public Animator Animator { get; set; }

    public void Configure(MetaEventData data)
    {
        SpriteRenderer.sprite = data.Image;

        _audioClip = data.SFX;
        _raiseAmount = data.RaiseAmount;

        BorderSpriteRenderer.color =
            _raiseAmount > 0 ? ServiceProvider.Player.RaisingColor : ServiceProvider.Player.DecrasingColor;
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

        if (_isBeingDestroyed)
        {
            _destroyTimer += Time.deltaTime;
            transform.position = transform.position.Lerp(ServiceProvider.Player.transform.position, _destroyTimer);
            transform.localScale = transform.localScale.Lerp(Vector3.zero, _destroyTimer);

            if (_destroyTimer >= 1)
                Destroy(gameObject);
        }
    }

    private void CheckIfGrabbed()
    {
        if (_grabbed || _isBeingDestroyed) return;

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
            ServiceProvider.SoundManager.PlaySFX(_audioClip);
            _isBeingDestroyed = true;
            _grabbed = false;
            Animator.enabled = false;
        }
    }
}
