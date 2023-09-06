using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    public GameObject[] characters;
    [HideInInspector] public bool isSelected = false;
    public Transform RightCorner;
    public Transform LeftCorner;
    public System.Action OnSelect;
    public GameObject[] finishes;
    [HideInInspector] public bool[] isFinished = new bool[3];
    public bool isGrounded;
    public bool jumpKeyHeld;
    [HideInInspector] public int countheldjump;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCharacter(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCharacter(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCharacter(2);
        }
        if (!isSelected) return;

        for (int i = 0; i < 3; i++)
        {
            if (Mathf.Abs(characters[i].transform.position.y - finishes[i].transform.position.y) <= 0.1f
                && Mathf.Abs(characters[i].transform.position.x - finishes[i].transform.position.x) <= 0.1f)
            {
                isFinished[i] = true;
            }
            else
            {
                isFinished[i] = false;
            }
        }
        if (isFinished[0] && isFinished[1] && isFinished[2])
            Debug.Log("WIN");

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        // In Update()
        isGrounded = Physics2D.OverlapArea(RightCorner.position, LeftCorner.position);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            countheldjump = 0;
            jumpKeyHeld = true;
            // Debug.Log("test1 : " + isGrounded + " " + countheldjump + " " + jumpForce);
            float jumpHeight;
            if (rb.gameObject.name == "Thomas") 
                jumpHeight = 1.0f;
            else if (rb.gameObject.name == "John")
                jumpHeight = 6.0f;
            else
                jumpHeight = 7.0f;
            jumpForce = Mathf.Sqrt(2 * Physics2D.gravity.magnitude * jumpHeight);
            Debug.Log(rb.mass + " " + jumpForce + " " + Physics2D.gravity.magnitude);
            rb.AddForce((Vector2.up * jumpForce) / 2, ForceMode2D.Impulse);
        }
        else if(Input.GetButtonUp("Jump"))
        {
            // Debug.Log("test3");
            jumpKeyHeld = false;
        }
    }

    void FixedUpdate()
    {
        if (!isSelected) return;
        Vector2 newVelocity = rb.velocity;
        if (Input.GetKey(KeyCode.A))
        {
            // rb.AddForce(new Vector2(-10, 0) * moveSpeed);
            newVelocity.x = -moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // rb.AddForce(new Vector2(10, 0) * moveSpeed);
            newVelocity.x = moveSpeed;
        }
        else
        {
            newVelocity.x = 0;
        }
        rb.velocity = newVelocity;
        // IsGrounded = Physics2D.OverlapArea(RightCorner.position, LeftCorner.position) && Input.GetKey(KeyCode.Space);
        // if (IsGrounded)
        //     rb.AddForce(Vector2.up * jumpForce);
        // In FixedUpdate()
        if(jumpKeyHeld)
        {
            // Debug.Log("test5");
            if (countheldjump < 20)
            {
                rb.AddForce((Vector2.up * jumpForce * rb.mass) / 20, ForceMode2D.Impulse);
                countheldjump++;
            }
        }
    }

    public void SetSelected(bool value)
    {
        isSelected = value;
    }

    private void SwitchCharacter(int characterIndex)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            PlayerController playerController = characters[i].GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.SetSelected(i == characterIndex);
            }
        }
    }
}
