using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float tSpeed;
    private Rigidbody playerRB;
    public GameObject bombPrefab; 

    public bool hasPowerup;
    private float powerUpStrength = 15.0f;
    public GameObject powerUpIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        powerUpIndicator.transform.position = this.transform.position + new Vector3(0, -0.5f, 0);
       
        float horizontalInput = Input.GetAxis("Horizontal");
        float forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * forwardInput * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = new Vector3(transform.position.x, 3.5f + transform.position.y, transform.position.z);
            Instantiate(bombPrefab, pos, bombPrefab.transform.rotation);
        }

        //playerRB.AddForce(Vector3.right * horizontalInput * speed);
        //playerRB.AddForce(Vector3.forward * forwardInput * speed); /*float forwardInput = Input.GetAxis("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mine") || other.gameObject.CompareTag("Heal Potion"))
        {
            powerUpIndicator.gameObject.SetActive(true);
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        powerUpIndicator.gameObject.SetActive(false);
        hasPowerup = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;

        if (otherObject.CompareTag("Enemy") && hasPowerup)
        {
            Debug.Log("Player collided with the " + otherObject.name + " with power up set to " + hasPowerup);

            Rigidbody enemyRB = otherObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (otherObject.transform.position - this.transform.position);

            enemyRB.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }
}
