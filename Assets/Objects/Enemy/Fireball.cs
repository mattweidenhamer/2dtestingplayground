using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    //When actually programming, have a single Attack class that all behavior is derived from
    public float xMoveSpeed;
    public float yMoveSpeed;
    private float moveModifier = 0.2f;
    public int damage = 1;

    private void FixedUpdate() {
        transform.Translate(xMoveSpeed * moveModifier, yMoveSpeed * moveModifier, 0);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);    
    }
}
