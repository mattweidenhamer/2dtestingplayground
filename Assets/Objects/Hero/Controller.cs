using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] int moveSpeed = 2;
    private float flatMoveModifier = 0.2f;
    public float xMoveModifier = 1;
    public float yMoveModifier = 1;
    private float savedXMoveModifier = 1;
    private float savedYMoveModifier = 1;
    public bool movementLocked = false;
    private void Start() {
        //In case unlock
        savedXMoveModifier = xMoveModifier;
        savedYMoveModifier = yMoveModifier;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xMovement = xMoveModifier * flatMoveModifier * moveSpeed * Input.GetAxis("Horizontal");
        float yMovement = yMoveModifier * flatMoveModifier * moveSpeed * Input.GetAxis("Vertical");

        //Consider using these with a fixed get/set values
        transform.Translate(xMovement, yMovement, 0 );
    }
    public void lockMovement(){
        movementLocked = true;
        savedXMoveModifier = xMoveModifier;
        xMoveModifier = 0;
        savedYMoveModifier = yMoveModifier;
        yMoveModifier = 9;
    }
    public void unlockMovement(){
        movementLocked = false;
        xMoveModifier = savedXMoveModifier;
        yMoveModifier = savedYMoveModifier;
    }
}
