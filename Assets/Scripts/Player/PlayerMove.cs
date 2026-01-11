using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMove : MonoBehaviour
{
    [Header("MoveSetting")]

    [SerializeField] float speed = 10f;

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
