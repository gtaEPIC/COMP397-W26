using System;
using UnityEngine;
using UnityEngine.InputSystem;
using KBCore.Refs;

[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{

    private InputAction move;
    private InputAction look;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float gravity = -9.87f;
    [SerializeField] private float rotationSpeed = 4.0f;
    [SerializeField] private float mouseSensY = 4.0f;
    private Vector3 velocity;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();
        
        // Movement of the player
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.Move(movement);
        
        // Rotation of the player
        transform.Rotate(Vector3.up, readLook.x * rotationSpeed * Time.deltaTime);
        
        // Rotate the camera
        mouseSensY *= readLook.y;
        mouseSensY = Mathf.Clamp(mouseSensY, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(mouseSensY * readLook.y, 0, 0);
    }

    private void OnValidate()
    {
        this.ValidateRefs();
    }
}
