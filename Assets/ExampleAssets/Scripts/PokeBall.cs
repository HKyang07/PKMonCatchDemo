using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeBall : MonoBehaviour
{
    public GameObject catchDialogue;
    private void Awake()
    {
        
    }
    private Vector3 initPos;
    private Quaternion initQuat;
    // Start is called before the first frame update
    void Start()
    {
        initPos = this.transform.localPosition;
        initQuat = this.transform.localRotation;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        catchDialogue.SetActive(false);
    }
    private float startTime, catchTime;
    private Vector3 startPos;
    private Vector2 touchStartPos;
    private Touch touch;
    private bool catched = false;
    float coef = 0.3f;
    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTime = Time.time;
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            float speed = Input.GetAxis("Mouse Y");
            speed = Vector3.Distance(Input.mousePosition, startPos) / (Time.time - startTime);
            speed = speed > 1000 ? 1000 : speed;
            Debug.Log(Input.mousePosition + " " + startPos + " Speed: "+ speed);
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, coef * speed, coef * speed));
        }
#else
        if (Input.touchCount > 0)
        {
            Debug.Log("touchCount = " + Input.touchCount);
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTime = Time.time;
                touchStartPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                float speed = Vector2.Distance(touch.position, touchStartPos) / (Time.time - startTime); //touch.deltaPosition.magnitude / touch.deltaTime;
                speed = speed > 1000 ? 1000 : speed;
                Debug.Log("Touch move speed: " + speed);
                gameObject.GetComponent<Rigidbody>().useGravity = true;
                this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, coef * speed, coef * speed));
            }
        }
#endif
        if (transform.position.y < -10)
        {
            resetPosition();
        }
        if (catched && Time.time - catchTime > 2.0f)
        {
            resetPosition();
        }
    }

    private void FixedUpdate()
    {

    }

    GameObject pokemon = null;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("EnterCollision: " + collision.collider.name);
        if (collision.gameObject.tag == "Pokemon")
        {
            pokemon = collision.gameObject;
            pokemon.GetComponent<Rigidbody>().isKinematic = true;
            catched = true;
            catchTime = Time.time;
            catchDialogue.SetActive(true);
        }
    }

    private void resetPosition()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        transform.localPosition = initPos;
        transform.localRotation = initQuat;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        catched = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //gameObject.GetComponent<Rigidbody>().freezeRotation = true;
        //gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(-25, 180, 0);
        //gameObject.GetComponent<Rigidbody>().freezeRotation = false;
        catchDialogue.SetActive(false);
        if (pokemon != null)
        {
            pokemon.transform.localPosition = Vector3.zero;
            pokemon.GetComponent<Rigidbody>().isKinematic = false;
        }
        //gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }
}
