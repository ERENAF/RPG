using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMove : MonoBehaviour
{
    [Header("MoveSetting")]

    [SerializeField] float speed = 10f;
    private Animator animator;
    private Vector2 lastMovement = Vector2.zero;
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movementDirection = CalculateMovementDirection(moveX,moveY);

        transform.position += movementDirection * Time.deltaTime * speed;

        MoveAnim(moveX,moveY);
    }

    public void MoveAnim(float moveX, float moveY)
    {
        float adjustedX = moveX;
        float adjustedY = moveY;

        if (Mathf.Abs(moveX) > 0.1f && Mathf.Abs(moveY) > 0.1f)
        {
            if (Mathf.Abs(moveX) >= Mathf.Abs(moveY))
            {
                adjustedY = 0f;
            }
            else
            {
                adjustedX = 0f;
            }

            if (lastMovement != Vector2.zero)
            {
                if (Mathf.Abs(lastMovement.x) > Mathf.Abs(lastMovement.y))
                {
                    adjustedX = Mathf.Sign(moveX);
                    adjustedY = 0f;
                }
                else
                {
                    adjustedY = Mathf.Sign(moveY);
                    adjustedX = 0f;
                }
            }
        }

        animator.SetFloat("MoveX", adjustedX);
        animator.SetFloat("MoveY", adjustedY);

        float currentSpeed = new Vector2(moveX, moveY).magnitude;
        animator.SetFloat("Speed", currentSpeed);

        if (currentSpeed > 0.1f)
        {
            lastMovement = new Vector2(moveX, moveY);
        }
    }

    private Vector2 CalculateMovementDirection(float horizontal, float vertical)
    {
        Vector3 up = transform.up;
        Vector3 right = transform.right;

        up.z = 0f;
        right.z = 0f;
        up.Normalize();
        right.Normalize();

        return (up*vertical+right*horizontal).normalized;
    }
}
