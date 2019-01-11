using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Effect : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioClip Decision;
    private bool One = false;
    // Use this for initialization
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        One = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (One == false)
            {
                Decision = gameObject.GetComponent<AudioSource>().clip;
                gameObject.GetComponent<AudioSource>().PlayOneShot(Decision);
                One = true;
            }
        }
    }
}
