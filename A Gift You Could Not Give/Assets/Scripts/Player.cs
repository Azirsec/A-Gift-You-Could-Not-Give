using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    [SerializeField] private Image image;

    bool canSlide = true;
    bool sliding = false;
    bool lose = false;

    public string scene;
    public string nextLevel;

    public AudioSource sound;

    private float timer = 0;

    public Animator animator;

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

                if (gameObject.GetComponent<Rigidbody>().velocity.magnitude > 0.01f)
                {
                    animator.SetBool("Walking", true);
                }
                else
                {
                    animator.SetBool("Walking", false);
                }
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
                    animator.SetBool("Slide", true);
                }

                if (slideTimer > slideDuration)
                {
                    animator.SetBool("Slide", false);
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

    public void Die()
    {
        if (!lose)
        {
            image.enabled = true;

            animator.SetBool("Walking", false);
            animator.SetBool("Slide", false);
            sound.Play();
            lose = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bad")
        {
            Die();
            //SceneManager.LoadScene(scene);
        }
        if (other.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
