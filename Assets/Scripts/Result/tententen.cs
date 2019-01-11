using UnityEngine;
using UnityEngine.UI;
public class tententen : MonoBehaviour {
    [SerializeField]
    Text TEXT_TENTEN;
    string tenten;
    float time=0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time = time + Time.deltaTime;
        if (time < 1f)
        {
            TEXT_TENTEN.text = "";
        }
        else if(time < 2f)
        {
            TEXT_TENTEN.text = ".";
        }
        else if (time < 3f)
        {
            TEXT_TENTEN.text = "..";
        }
        else if (time < 4f)
        {
            TEXT_TENTEN.text = "...";

        }
        else if (time < 5f)
        {
            time = 0;
        }
    }
}
