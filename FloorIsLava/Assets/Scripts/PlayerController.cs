using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float rollSpeed;

    public float jumpForce;

    private Rigidbody rb;

    private bool isGrounded;
    public GameObject textToShow;

    void Start () {
        rb = GetComponent<Rigidbody>();
        textToShow.SetActive(false);
    }
    void Update () {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(verticalInput, 0f, -horizontalInput) * rollSpeed * Time.deltaTime;

        rb.AddForce(movement);

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.6f);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

    }
    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}