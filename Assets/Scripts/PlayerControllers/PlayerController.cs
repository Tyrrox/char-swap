using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected LayerMask platformLayerMask;
    protected Rigidbody2D rb;
    protected BoxCollider2D boxColl;
    protected Animator animate;
    protected SpriteRenderer sprite;

    protected float horizontalInput;
    protected float playerSpeed = 7f;
    protected float jumpStrength = 17f;

    protected bool grounded = false;
    protected bool facingRight = true;

    private float transformationTime = 0.17f;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxColl = GetComponent<BoxCollider2D>();
        animate = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        // Take movement inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // jumping logic
        grounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }
        // jump animation logic
        animate.SetBool("IsGrounded", grounded);

        // Perform ability actions
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ActivateAbility();
        }
    }

    protected virtual void FixedUpdate()
    {
        // Perform physics actions
        MovePlayer();
    }

    protected virtual void ActivateAbility()
    {

    }

    private void MovePlayer()
    {
        // Only allow movement if the rigid body is not frozen
        if (rb.constraints != RigidbodyConstraints2D.FreezeAll)
        {
            // Adjust horizontal velocity based on key press
            rb.velocity = new Vector2(horizontalInput * playerSpeed, rb.velocity.y);
            animate.SetFloat("Speed", Mathf.Abs(horizontalInput));

            if (horizontalInput < 0 && facingRight)
            {
                FlipSprite();
            }
            else if (horizontalInput > 0 && !facingRight)
            {
                FlipSprite();
            }
        }
    }

    private void FlipSprite()
    {
        facingRight = !facingRight;

        Vector2 flippedScale = transform.localScale;
        flippedScale.x *= -1;
        transform.localScale = flippedScale;
    }

    public void SetFacingRight(bool set)
    {
        if (set != facingRight)
        {
            FlipSprite();
        }
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }

    protected void Jump()
    {
        // Rapid velocity increase for jump
        rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
    }

    protected bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxColl.bounds.center, boxColl.bounds.size, 0f, Vector2.down, extraHeight, platformLayerMask);
        bool raycastHasHit = raycastHit.collider != null;

        if (GameManager.gameManager.IsDebugMode())
        {
            Color boxColour;
            if (raycastHasHit)
            {
                boxColour = Color.green;
            }
            else
            {
                boxColour = Color.red;
            }

            Debug.DrawRay(boxColl.bounds.center + boxColl.bounds.extents, Vector2.down * boxColl.bounds.size.y, boxColour);
            Debug.DrawRay(boxColl.bounds.center - boxColl.bounds.extents, Vector2.up * boxColl.bounds.size.y, boxColour);
            Debug.DrawRay(boxColl.bounds.center + boxColl.bounds.extents, Vector2.left * boxColl.bounds.size.x, boxColour);
            Debug.DrawRay(boxColl.bounds.center - boxColl.bounds.extents, Vector2.right * boxColl.bounds.size.x, boxColour);
        }

        return raycastHasHit;
    }

    public IEnumerator TransformCharacterOut()
    {
        // Start transformation process, play animation, set kinematic to true to freeze object
        animate.SetBool("IsTransformingOut", true);
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(transformationTime);
        // Cancel animation after transformation, unfreeze
        animate.SetBool("IsTransformingOut", false);
    }

    public virtual IEnumerator TransformCharacterIn()
    {
        // Wait for transforming in animation
        yield return new WaitForSeconds(transformationTime);
        // after transformation animation, unfreeze
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void Die()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animate.SetBool("isDead", true);
        sprite.sortingLayerName = "DyingPlayer";
        // Play animation directly to avoid state transitions
        animate.Play("Dying");
        StartCoroutine(PlayerManager.playerManager.RespawnPlayer());
    }

    public void Respawn()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animate.SetBool("isDead", false);
        sprite.sortingLayerName = "Player";
    }
}
