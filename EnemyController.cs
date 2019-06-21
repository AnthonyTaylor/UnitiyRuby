using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    bool broken = true;
    public ParticleSystem smokeEffect;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    Animator animator;

    public AudioClip robotFix;
    AudioSource audioSource;

    public OpponentController oppController;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        oppController.opponentHasAppeared();
    }

    // Update is called once per frame

    /// <summary>
    /// While not broken
    /// walk for timer
    /// </summary>
    void Update()
    {
        if (!broken) return;

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigidbody2D.position;
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    /// <summary>
    /// If collides with Ruby,
    /// Lower Ruby health
    /// </summary>
    /// <param name="other"></param>
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    /// <summary>
    /// If cog hits enemy,
    /// Stop walking,
    /// Lower Enemy Remaining count,
    /// Play fix  sount
    /// </summary>
    public void Fix()
    {
        broken = false;
        smokeEffect.Stop();
        audioSource.Stop();
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        oppController.opponentHasDied();
        
        audioSource.PlayOneShot(robotFix);
    }
}
