using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction; 
    public float gravity = 12f * 2f;
    public float jump = 10f;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()  
    {
        direction = Vector3.zero;
    } 

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            // direction = Vector3.down;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction = Vector3.up * jump;
            }
        }

        character.Move(direction * Time.deltaTime);
    }
}
