using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMove _playerMove;
    private PlayerController _playerController;
    
    void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }
}
