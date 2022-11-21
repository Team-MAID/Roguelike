using System;
using UnityEngine;

/// <summary>
/// Create the smooth dropping effect when the player drop an item from the inventory
/// </summary>
public class ItemDropEffect : MonoBehaviour
{
    [Header("The time it will take for the object to reach its drop position.")] [SerializeField] [Range(0f, 1f)]
    private float smoothTime = 0.1f;

    private bool _isDroppingItem = false;
    public bool IsDroppingItem => _isDroppingItem;
    private Vector2 _dropTarget;
    private Vector2 _dropVelocity = Vector2.zero;
    private Collider2D _collider;
    private bool isColliderNotNull;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        isColliderNotNull = _collider != null;
    }


    private void Update()
    {
        if (!_isDroppingItem) return;

        transform.position = Vector2.SmoothDamp(transform.position, _dropTarget, ref _dropVelocity, smoothTime);

        if (Vector2.Distance(transform.position, _dropTarget) < 0.05)
        {
            _isDroppingItem = false;
            // Reactive the collider when we are sure that the item is at a sufficient distance
            if (isColliderNotNull)
            {
                _collider.enabled = true;
            }
            _dropVelocity = Vector2.zero;
        }
    }

    public void ActivateDropEffect(Vector2 target)
    {
        // Deactivate the Collider when dropping the item to prevent the player from triggering a collision with it
        if (isColliderNotNull)
        {
            _collider.enabled = false;
        }

        _dropTarget = target;
        _dropVelocity = Vector2.zero;

        _isDroppingItem = true;
    }
}