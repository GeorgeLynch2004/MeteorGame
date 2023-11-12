using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float _MovementSpeed;
    [SerializeField] public int coinCount;
    [SerializeField] private GameObject graphics;

    private Vector3 lastFramePos;
    private Vector3 initScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initScale = graphics.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = graphics.transform.localScale;
        Vector3 pos = transform.position;

        // Handle Key Presses
        if (Input.GetKey(KeyCode.A))
        {
            pos -= Vector3.right * _MovementSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos += Vector3.right * _MovementSpeed * Time.deltaTime;
        }

        // Enables player to run off one side of the map and appear at the other side, PACMAN style
        if (pos.x >= 10f || pos.x <= -10f)
        {
            pos.x *= -1;
        }

        transform.position = pos;
        
        if (transform.position.x < lastFramePos.x)
        {
            scale.x = -initScale.x;
        }
        else if (transform.position.x > lastFramePos.x)
        {
            scale.x = initScale.x;
        }

        graphics.transform.localScale = scale;

        lastFramePos = transform.position;
    }
}