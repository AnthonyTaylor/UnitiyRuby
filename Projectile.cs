using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    /// <summary>
    /// On Creation connect to component
    /// </summary>
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// If it has reached 1000 units from centre, destroy
    /// </summary>
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Sets speed and direction of cog
    /// </summary>
    /// <param name="direction">Vector2</param>
    /// <param name="force">float - currently hardcoded to 300</param>
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    /// <summary>
    /// If colides with robot,
    /// Run fix function.
    ///  Destroy regardless
    /// </summary>
    /// <param name="other">Type of object that collides with cog</param>
    void OnCollisionEnter2D(Collision2D other)
    {

        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            e.Fix();
        }

        Destroy(gameObject);
    }
}

