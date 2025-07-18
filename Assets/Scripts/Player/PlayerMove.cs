using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IParts.IDodgeable
{
    private Vector3 originScale;
    [SerializeField]
    private float _moveSpeed = 13f;
    
    private Rigidbody2D _rigid;
    
    public Vector3 MoveDirection { get; set; } = Vector3.zero;
    public int DodgeCount { get; set; } = 1;
    public int JumpCount { get; set; } = 1;

    private void Start()
    {
        originScale = transform.localScale;
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (MoveDirection.x != 0)
        {
            Vector3 newScale = originScale;
            newScale.x = Mathf.Abs(originScale.x) * (MoveDirection.x < 0 ? 1 : -1);
            transform.localScale = newScale;
        }

        _rigid.MovePosition(transform.position + MoveDirection * (_moveSpeed * Time.deltaTime));
    }
    
    public void Jump()
    {
        throw new NotImplementedException();
    }
    
    public void Dodge()
    {
        throw new NotImplementedException();
    }
}
