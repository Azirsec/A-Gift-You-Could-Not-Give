using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winscreen : MonoBehaviour
{
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.anyKey && timer > 3.0f)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
