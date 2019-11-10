using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlemove : MonoBehaviour
{
    public int speedSin = 1;
    public int speedCos = 1;
    public float speed = 1;

    private float sinWave = 0;
    private float cosWave = 0;

    private Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime * speed;
        sinWave += dt;
        cosWave += dt;


        gameObject.transform.localPosition =
            (transform.right * Mathf.Sin(sinWave) * speedSin + transform.forward * Mathf.Cos(cosWave) * speedCos) + startpos;
    }
}
