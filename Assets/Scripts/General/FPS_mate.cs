using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS_mate : MonoBehaviour {

    public TextMeshProUGUI FPS;

    public float m_updateInterval = 1.0f;  // 更新される頻度
    float m_accumulated = 0.0f;
    float m_timeUntilNextInterval; //  次の更新までの残り時間
    int m_numFrames = 0;

    void Start()
    {
        FPS.enabled = false;
        m_timeUntilNextInterval = m_updateInterval;
    }

    void Update()
    {
        m_timeUntilNextInterval -= Time.deltaTime;
        m_accumulated += Time.timeScale / Time.deltaTime;
        ++m_numFrames;

        if (m_timeUntilNextInterval <= 0.0)
        {
            // FPSの計算と表示
            float fps = m_accumulated / m_numFrames;
            string format = System.String.Format("FPS: {0:F2}", fps);
            FPS.text = format;

            m_timeUntilNextInterval = m_updateInterval;
            m_accumulated = 0.0F;
            m_numFrames = 0;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            FPS.enabled = !FPS.enabled;
        }
    }
}
