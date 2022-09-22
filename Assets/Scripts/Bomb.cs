using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody bombRB;
    public float speed = 10.0f;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        bombRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherObject = collision.gameObject;
        if (otherObject.CompareTag("Player"))
        {
            //bombRB.AddForce(Vector3.left * speed);
            bombRB.AddForce(Vector3.back * speed);
        }

        if (otherObject.CompareTag("Enemy") && this.transform.position.y < 1)
        {
            Debug.Log("Bomb collided with Enemy");
            GameManager.score += 5;
            GameManager.gameManager.UpdateKills();
            Destroy(otherObject);
        }

        if (otherObject.CompareTag("Island"))
        {
            explosionParticle.Play();
            StartCoroutine(WaitForExplosion());
        }
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
