using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    CharacterController characterController;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector3 moveDirection;
    

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void ChangeDirection(Vector3 vec)
    {
        moveDirection = vec;
    }
}
