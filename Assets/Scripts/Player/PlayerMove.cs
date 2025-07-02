using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 originScale;
    
    [SerializeField]
    private float _moveSpeed = 13f;

    private Rigidbody2D _rigid;
    
    public Vector3 MoveDirection { get; set; } = Vector3.zero;

    private void Start()
    {
        originScale = transform.localScale;
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (MoveDirection.x != 0)
        {
            Vector3 newScale = originScale;
            newScale.x = Mathf.Abs(originScale.x) * (MoveDirection.x < 0 ? 1 : -1);
            transform.localScale = newScale;
        }

        _rigid.MovePosition(transform.position + MoveDirection * (_moveSpeed * Time.deltaTime));
    }
}
