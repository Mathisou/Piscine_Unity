using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject textToShow;
    private void OnCollisionEnter(Collision collision)
    {
        rb = GetComponent<Rigidbody>();
        
        if (collision.gameObject.CompareTag("Floor"))
        {
            // Destroy(gameObject);
            // Player = new GameObject("Sphere");
            transform.position = new Vector3(-3.969f, 4.011f, -3f);
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.None;
            // transform.rotation = Quaternion.identity;;
            Debug.Log("GameOver");
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
            textToShow.SetActive(true);
            Debug.Log("Congratulations");
        }

    }
}