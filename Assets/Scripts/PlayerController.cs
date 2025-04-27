using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxSpeed;

    private Rigidbody2D rb;

    private Vector2 lookDirectionInput;
    private Vector2 moveDirection;

    private bool isAttack;
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private GameObject spawnBullets;

    [SerializeField] private float fireRate;
    private float lastFireTime;
    [SerializeField] float power;


    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack && lastFireTime + fireRate < Time.time) 
        {
            float angle = transform.rotation.eulerAngles.z;

            Instantiate(bulletPref, spawnBullets.transform.position, Quaternion.Euler(0, 0, angle));
            lastFireTime = Time.time;

            float radians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            rb.AddForce(-direction * power , ForceMode2D.Impulse);
        }
    }

    private void LateUpdate()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (lookDirectionInput.magnitude == 0)
        {
            return; //завершить функцию
        }
        float targetAngle = Mathf.Atan2(lookDirectionInput.y, lookDirectionInput.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude < maxSpeed)
        {
            rb.AddForce(moveDirection * speed);
        }
    }

    private void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
    }

    private void OnLook(InputValue input)
    {
        lookDirectionInput = input.Get<Vector2>().normalized;
    }

    private void OnAttack(InputValue input) 
    {
        isAttack = input.isPressed;
    }
}
