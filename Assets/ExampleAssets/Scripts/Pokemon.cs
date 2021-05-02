using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    private void Awake()
    {
        
    }

    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 300);
        gameObject.GetComponent<Rigidbody>().freezeRotation = true;
    }

    //private float horizontalSpeed = 2.0f;
    //private float verticalSpeed = 2.0f;
    // Update is called once per frame
    void Update()
    {
        //float h = horizontalSpeed * Input.GetAxis("Mouse X");
        //float v = verticalSpeed * Input.GetAxis("Mouse Y");
        //transform.Rotate(v, h, 0);
    }
    float dx = 1, dy = 1, dz = 1;
    float startTime = 0;
    private void FixedUpdate()
    {
        if (Mathf.Abs(transform.position.x) > 5)
        {
            //dx = Random.value < 0.5f ? -1 : 1;
            //dy = Random.value < 0.5f ? -1 : 1;
            //dz = Random.value < 0.5f ? -1 : 1;
            dx *= -1;
            dy *= -1;
            dz *= -1;
        }
        //transform.Translate(new Vector3(0.1f * dx, 0.0f * dy, 0.0f * dz));

        if (Time.time - startTime > 2.5f)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 300);
            startTime = Time.time;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("PokemonCollisionFunction:" + collision.collider.name);
        //this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(1000, 1000, 1));
    }

}
