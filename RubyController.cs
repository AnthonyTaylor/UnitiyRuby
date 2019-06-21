using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{

    public float speed = 3.0f;
    public float sprintSpeedMultiplier = 2.5f;
    float moveSpeed;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public int health { get { return currentHealth; } }
    int currentHealth;
    bool isInvincible;
    float invincibleTimer;


    public GameObject projectilePrefab;

    Rigidbody2D rigidbody2d;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public OpponentController oppController;

    AudioSource audioSource;
    public AudioClip thrownClip;
    public AudioClip hitClip;
    public AudioClip questComplete;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

    }

    /// <summary>
    /// Handle movement,IFrames, user input
    /// </summary>
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
            GetComponent<AudioSource>().UnPause();
        }
        else
        {
            GetComponent<AudioSource>().Pause();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2d.position;

        moveSpeed = (Input.GetKey(KeyCode.LeftShift)) ? (speed * sprintSpeedMultiplier) : speed;

        position = position + move * moveSpeed * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            PlaySound(thrownClip);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    if (oppController.opponentsLeft != 0)
                    {
                        //begin text
                        character.DisplayDialog(false);
                    }
                    else
                    {
                        //complete text
                        character.DisplayDialog(true);
                        PlaySound(questComplete);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(oppController.opponentsLeft);
        }
    }

    /// <summary>
    /// Allows the health of the main character to be changed, both up and down
    /// </summary>
    /// <param name="amount">Integer - Can be set to positive or negative</param>
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");
            PlaySound(hitClip);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    /// <summary>
    /// Creates new copy of the cog and launch it
    /// </summary>
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

    /// <summary>
    /// Plays the sound clip from ruby's perspective
    /// </summary>
    /// <param name="clip"AudioClip - sound file needs to be attached to and object in editor></param>
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
