using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    public float Delay_time;
    private bool set_scene = false;
    public void SceneChanegeFanc(string SceneName)
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            set_scene = true;
            
        }
        if (set_scene)
        {
            Delay_time += Time.deltaTime;
            if (Delay_time > 0.5)
            {
                SceneManager.LoadScene(SceneName);
                set_scene = false;
                Delay_time = 0;
            }
        }
    }

    public void GoToDemo()
    {
        set_scene = true;
    }
}