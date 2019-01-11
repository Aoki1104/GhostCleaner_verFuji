using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeScript : MonoBehaviour {
	void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.KeypadPeriod)){
            SceneManager.LoadScene("Title");
            Destroy(this.gameObject);
        }

        if (SceneManager.GetActiveScene().name == "Result")
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
