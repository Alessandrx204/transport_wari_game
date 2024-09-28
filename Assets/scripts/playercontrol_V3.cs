/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_V3 : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;
    private Vector2 input;
    
    // Reference to the Animator
    private Animator animator;
    
    // Track the last movement direction
    private string lastDirection = "down";

    private void Start()
    {
        // Get the Animator component from the player
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Prevent diagonal movement: if both directions are pressed, cancel one direction
            if (input.x != 0 && input.y != 0)
            {
                input.y = 0; // Prioritize horizontal input
            }

            // Set animation parameters and track the last direction
            if (input.x > 0)
            {
                animator.CrossFade("Wari_walking_right",0); // Walking right animation
                lastDirection = "right"; // Store last direction
            }
            else if (input.x < 0)
            {
                animator.CrossFade("Wari_walking_left2",0); // Walking left animation
                lastDirection = "left"; // Store last direction
            }
            else if (input.y > 0)
            {
                animator.CrossFade("Wari_walking_up",0); // Walking up animation
                lastDirection = "up"; // Store last direction
            }
            else if (input.y < 0)
            {
                animator.CrossFade("Wari_walking_down",0); // Walking down animation
                lastDirection = "down"; // Store last direction
            }
            else
            {
                // If there's no movement input, play the appropriate idle animation
                PlayIdleAnimation();
            }

            // Initiate movement if there's input
            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                StartCoroutine(Move(targetPos));
            }
        }
    }

    private void PlayIdleAnimation()
    {
        // Play the idle animation based on the last movement direction
        switch (lastDirection)
        {
            case "right":
                animator.CrossFade("Wari_idle_right",0);
                break;
            case "left":
                animator.CrossFade("Wari_idle_left",0);
                break;
            case "up":
                animator.CrossFade("Wari_idle_up",0);
                break;
            case "down":
            default:
                animator.CrossFade("Wari_idle_down",0);
                break;
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }
    
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_V3 : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask collisionLayer; // Layer for walls/obstacles

    private bool isMoving;
    private Vector2 input;

    // Reference to the Animator
    private Animator animator;

    // Track the last movement direction
    private string lastDirection = "down";

    private void Start()
    {
        // Get the Animator component from the player
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (!isMoving)
        {
            // Get input
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Prevent diagonal movement: if both directions are pressed, cancel one direction
            if (input.x != 0 && input.y != 0)
            {
                input.y = 0; // Prioritize horizontal input
            }

            // Update animation based on input
            UpdateAnimation();

            // Initiate movement if there's valid input
            if (input != Vector2.zero)
            {
                Vector3 targetPos = transform.position + (Vector3)input; // Calculate target position
                if (CanMove(targetPos)) // Check if the target position is valid
                {
                    StartCoroutine(Move(targetPos));
                }
            }
            else
            {
                // If there's no movement input, play the appropriate idle animation
                PlayIdleAnimation();
            }
        }
    }

    private void UpdateAnimation()
    {
        if (input.x > 0)
        {
            animator.CrossFade("Wari_walking_right", 0); // Walking right animation
            lastDirection = "right"; // Store last direction
        }
        else if (input.x < 0)
        {
            animator.CrossFade("Wari_walking_left2", 0); // Walking left animation
            lastDirection = "left"; // Store last direction
        }
        else if (input.y > 0)
        {
            animator.CrossFade("Wari_walking_up", 0); // Walking up animation
            lastDirection = "up"; // Store last direction
        }
        else if (input.y < 0)
        {
            animator.CrossFade("Wari_walking_down", 0); // Walking down animation
            lastDirection = "down"; // Store last direction
        }
    }

    private void PlayIdleAnimation()
    {
        // Play the idle animation based on the last movement direction
        switch (lastDirection)
        {
            case "right":
                animator.CrossFade("Wari_idle_right", 0);
                break;
            case "left":
                animator.CrossFade("Wari_idle_left", 0);
                break;
            case "up":
                animator.CrossFade("Wari_idle_up", 0);
                break;
            case "down":
            default:
                animator.CrossFade("Wari_idle_down", 0);
                break;
        }
    }

    // Checks if the player can move to the target position
    private bool CanMove(Vector3 targetPos)
    {
        // Cast a small box (like the player collider) to check for collisions at the target position
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (targetPos - transform.position).normalized, 1f, collisionLayer);
        return hit.collider == null; // Return true if no collider is hit
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true; // Set moving flag
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null; // Wait until the next frame
        }
        transform.position = targetPos; // Ensure the position is set to the target

        isMoving = false; // Reset moving flag
    }
}