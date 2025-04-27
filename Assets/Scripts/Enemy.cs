using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Transform player;
    private Vector2 direction;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.position - transform.position;
        direction.Normalize();
    }
    private void LateUpdate()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {


        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);


    }
    private void FixedUpdate()
    {
        rb.linearVelocity = transform.right * speed;
    }



}

