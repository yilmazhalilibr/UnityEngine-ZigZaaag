using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject particle;
    [SerializeField]
    private float speed;
    Rigidbody rb;
    bool started;
    bool gameOver;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        started = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = new Vector3(speed, 0f, 0f);
                started = true;
                GameManager.instance.StartGame();
            }
        }

        Debug.DrawRay(transform.position, Vector3.down, Color.red);

        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            gameOver = true;
            rb.velocity = new Vector3(0f, -25f, 0f);
            Camera.main.GetComponent<CameraFollow>().gameOver = true;
            GameManager.instance.GameOver();
        }


        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            SwitchDirection();
        }
    }
    void SwitchDirection()
    {
        if (rb.velocity.z > 0)
        {
            rb.velocity = new Vector3(speed, 0f, 0f);
        }
        else if (rb.velocity.x > 0)
        {
            rb.velocity = new Vector3(0f, 0f, speed);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Daimond"))
        {
            GameObject part = Instantiate(particle, other.gameObject.transform.position, Quaternion.identity) as GameObject;
            Destroy(other.gameObject);
            Destroy(part, 1.5f);
        }
    }
}
