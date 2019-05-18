using UnityEngine;
using System.Collections;

/// <summary>
/// This is the main class for controlling both camera and player. It is recommended to attach this to the player or camera in the scene, but not necessary
/// </summary>
public class EzPlayerController : MonoBehaviour
{
    [SerializeField] private EzCamera m_camera = null;
    [SerializeField] private EzMotor m_controlledPlayer = null;
    public float speed;
    public GameObject shot;
    public GameObject shotSpawn;
    private Rigidbody rb;

    private void Start()
    {
        // if either the player or camera are null, attempt to find them
        rb = GetComponent<Rigidbody>();
        SetUpControlledPlayer();
        SetUpCamera();
    }

    private void Update()
    {
        if (m_controlledPlayer != null && m_camera != null)
        {
            HandleInput();
        }

    }

    private void SetUpControlledPlayer()
    {
        if (m_controlledPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                m_controlledPlayer = playerObj.GetComponent<EzMotor>();
            }
        }
    }

    private void SetUpCamera()
    {
        if (m_camera == null)
        {
            m_camera = Camera.main.GetComponent<EzCamera>();
            if (m_camera == null)
            {
                m_camera = Camera.main.gameObject.AddComponent<EzCamera>();
            }
        }
    }

    void FixedUpdate()
    {
        Movement();
        Direction();
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(shot, shotSpawn.transform.position, shotSpawn.transform.rotation);
        }
    }

    //Map the buttons
    private void Movement()
    {
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector3(-1, 0, 1) * speed;
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(-1, 0, -1) * speed;
        }
        else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(1, 0, -1) * speed;
        }
        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(1, 0, 1) * speed;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector3.forward * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector3.left * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = Vector3.back * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector3.right * speed;
        }

        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void Direction()
    {
        if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            rb.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.left);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            rb.rotation = Quaternion.FromToRotation(Vector3.left, Vector3.down);
        }
        else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rb.rotation = Quaternion.FromToRotation(Vector3.down, Vector3.right);
        }
        else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rb.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.right);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.rotation = Quaternion.FromToRotation(Vector3.left, Vector3.left);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.rotation = Quaternion.FromToRotation(Vector3.down, Vector3.down);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.rotation = Quaternion.FromToRotation(Vector3.right, Vector3.right);
        }

        else
        {
            rb.rotation = Quaternion.identity;
        }
    }

    private void HandleInput()
    {
        // Update player movement first
        // cache the inputs
        float horz = Input.GetAxis(ExtensionMethods.HORIZONTAL);
        float vert = Input.GetAxis(ExtensionMethods.VERITCAL);

        // Convert movement to camera space
        Vector3 moveVector = m_camera.ConvertMoveInputToCameraSpace(horz, vert);

        // Move the Player
        m_controlledPlayer.MovePlayer(moveVector.x, moveVector.z, Input.GetKey(KeyCode.LeftShift));

    }
}
