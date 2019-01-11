using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class End_Processing : MonoBehaviour
{

    public AudioSource[] audiosource;
    private float stop_time;
    private bool stop = false;
    bool bl = true;//一回のみ実行
    // Use this for initialization

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        // audiosource = gameObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Delete))
        {
            Destroy(this.gameObject);
        }

            if (SceneManager.GetActiveScene().name == "Title" && bl == true || SceneManager.GetActiveScene().name == "Tutorial" && bl == true)
        {
            audiosource[0].Play();
            bl = false;

            /*
            if (stop)
            {
                stop_time += Time.deltaTime;
                if (stop_time > 2.0f)
                {
                    stop = false;
                    stop_time = 0.0f;
                }
            }
            */
        }


        if (SceneManager.GetActiveScene().name == "Capture" && bl == false)
        {
            audiosource[0].Stop();
            audiosource[1].Play();
            bl = true;

        }

        if (SceneManager.GetActiveScene().name == "Result" && bl == true)
        {
            audiosource[1].Stop();
            audiosource[2].Play();
            bl = false;
        }

        if (SceneManager.GetActiveScene().name == "Result")
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                audiosource[2].Stop();
                Destroy(this.gameObject);
            }
        }




    }
}
