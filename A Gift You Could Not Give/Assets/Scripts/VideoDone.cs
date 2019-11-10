using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoDone : MonoBehaviour
{
    public VideoPlayer video;
    private float timer = 0;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 30)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
