using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    Vector3 direction = new Vector3(0, 0, 0);
    Vector3 velocity = new Vector3(0, 0, 0);
    Vector3 acceleration = new Vector3(0, 0, 0);

    [SerializeField] private float speed;
    [SerializeField] private float slideDuration;
    [SerializeField] private float slideCooldown;
    [SerializeField] private float slideSpeed;
    private float slideTimer = 0;

    bool canSlide = true;
    bool sliding = false;
    bool lose = false;

    public string scene;
    public string nextLevel;

    public AudioSource sound;

    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lose)
        {
            timer += Time.deltaTime;
            if (timer > 4.6)
            {
                timer = 0;
                lose = false;
                SceneManager.LoadScene(scene);
            }
        }

        else
        {
            if (canSlide || slideTimer > slideDuration)
            {
                direction.x = Input.GetAxisRaw("Horizontal");
                direction.z = Input.GetAxisRaw("Vertical");

                direction = direction.normalized;
                acceleration = direction * speed;// * Time.deltaTime;

                gameObject.GetComponent<Rigidbody>().AddForce(acceleration, ForceMode.Impulse);
            }
            /// regular movement ends

            /// slide movement begins
            if ((Input.GetButton("Slide") || Input.GetKey(KeyCode.Space)) && canSlide)
            {
                sliding = true;
                canSlide = false;
                slideTimer = 0;
                GetComponent<Rigidbody>().AddForce(slideSpeed * acceleration, ForceMode.Impulse);
            }

            if (sliding)
            {
                //add to slide timer
                slideTimer += Time.deltaTime;

                //if slide is still occurring move the character
                if (slideTimer < slideDuration)
                {
                    //gameObject.transform.position += (velocity.normalized * slideSpeed * Time.deltaTime);
                }

                //allow the player to slide again
                if (slideTimer > slideCooldown + slideDuration)
                {
                    sliding = false;
                    canSlide = true;
                }
            }
            ///slide movement ends
            if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.1f)
            {
                gameObject.transform.forward = new Vector3(GetComponent<Rigidbody>().velocity.normalized.x, 0, GetComponent<Rigidbody>().velocity.normalized.z);
            }

        }



    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bad")
        {
            sound.Play();
            lose = true;
            //SceneManager.LoadScene(scene);
        }
        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
