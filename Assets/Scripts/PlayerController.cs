using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    private Rigidbody rb;
    [SerializeField] private RectTransform Rotator;

    [Header("Movement")]
    [SerializeField] private bool UseMouse = false;
    [SerializeField] private float constantDeceleration;
    [SerializeField] private float aimingDeceleration;
    [SerializeField] private float impulseSpeed;
    private Vector3 direction = Vector3.zero;

    [Header("Input")]
    [SerializeField] private KeyCode keyboardDashKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode mouseDashKey = KeyCode.Mouse0;
    private Vector2 mouseStartPosition = Vector2.zero;

    [HideInInspector] public bool canDestroy = false;

    private KeyCode dashKey;
    private bool dashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (UseMouse)
        {
            dashKey = mouseDashKey;
        }
        else
        {
            dashKey = keyboardDashKey;
        }
    }

    private void Update()
    {
        if (UseMouse)
        {

            if (Input.GetKeyDown(dashKey))
            {
                mouseStartPosition = Input.mousePosition;
            }

            if (Input.GetKey(dashKey))
            {
                Decelerate(aimingDeceleration);
                GetDirection(mouseStartPosition);
            }
            else
                Decelerate(constantDeceleration);

            if (Input.GetKeyUp(dashKey))
            {
                dashing = true;
                Rotator.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Input.GetKey(dashKey))
            {
                Decelerate(aimingDeceleration);
                GetDirection();
            }
            else
                Decelerate(constantDeceleration);

            if (Input.GetKeyUp(dashKey))
            {
                dashing = true;
                Rotator.gameObject.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (dashing)
        {
            rb.AddForce(direction * impulseSpeed, ForceMode.VelocityChange);
            dashing = false;
        }
    }
    private void GetDirection()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        float angle = Mathf.Atan2(direction.z, direction.x);
        Rotator.position = transform.position;
        Vector3 rotation = new Vector3(0, angle * 180 / Mathf.PI + 90, 0);
        Rotator.localEulerAngles = -rotation;

        if (direction.x != 0 || direction.z != 0) Rotator.gameObject.SetActive(true);
        else Rotator.gameObject.SetActive(false);

    }
    private void GetDirection(Vector2 mouseStartPosition)
    {
        Vector2 mouseCurrentPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        Vector2 tempDirection = (mouseStartPosition - mouseCurrentPosition).normalized;

        float angle = Mathf.Atan2(tempDirection.y, tempDirection.x);

        if (angle % (Mathf.PI / 4) != 0)
        {
            angle = Mathf.Round(angle / (Mathf.PI / 4)) * (Mathf.PI / 4) - (Mathf.PI / 4);
            tempDirection = new Vector2(Mathf.Cos(angle) - Mathf.Sin(angle), Mathf.Sin(angle) + Mathf.Cos(angle));
        }

        direction = new Vector3(tempDirection.x, 0, tempDirection.y).normalized;

        Rotator.position = transform.position;
        Vector3 rotation = new Vector3(0, angle * 180 / Mathf.PI + 135, 0);
        Rotator.localEulerAngles = -rotation;
        Rotator.gameObject.SetActive(true);

    }

    private void Decelerate(float deceleration)
    {
        rb.linearVelocity -= rb.linearVelocity * deceleration;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canDestroy)
        {
            if (collision.transform.GetComponent<PlayerController>() != null)
            {
                Destroy(collision.gameObject);
                //if (UseMouse)
                //    GameManager.instance.NextRound(false);
                //else
                //    GameManager.instance.NextRound(true);
            }
        }
    }
}
