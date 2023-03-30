using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    
    [SerializeField] private CharacterController characterController;
    
    private bool isRun;

    public bool IsRun
    {
        get => isRun;
        set => isRun = value;
    }

    private void Start()
    {
        IsRun = true;
    }

    private void Update()
    {
        if (IsRun)
        {
            PlayerMove();
            //transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void PlayerMove()
    {
        var direction = transform.forward * speed;
        characterController.SimpleMove(direction);
    }
}
