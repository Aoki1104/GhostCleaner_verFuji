using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//以下追加
using System.IO.Ports;
using System.Threading;
using UnityEngine.SceneManagement;

public class SerialHandler : MonoBehaviour
{

    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    public string portName; //ポート名
    private const int baudRate = 115200;       //レート

    public SerialPort serialPort_;
    private Thread thread_;
    private bool isRunninng_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    private string scene_name;

    void Awake()
    {
        scene_name = SceneManager.GetActiveScene().name;

        if (scene_name == "Capture" || scene_name == "Result1" || scene_name == "Title") {
			portName = PlayerPrefs.GetString("PortNum");
		}
        open();
    }

    void Update()
    {
        if (isNewMessageReceived_)
        {
         //   OnDataReceived(message_);
            ////Debug.Log(message_);
        }
        isNewMessageReceived_ = false;

        if (scene_name == "COM")
        {
                portName = PlayerPrefs.GetString("PortNum");
        }
    }

    void OnDestroy()
    {
        close();
    }

    private void open()
    {
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        serialPort_.ReadTimeout = 20;
        serialPort_.Open();
        serialPort_.NewLine = "\n";

        isRunninng_ = true;

        thread_ = new Thread(Read);
        thread_.Start();
    }

    private void close()
    {
        isNewMessageReceived_ = false;
        isRunninng_ = false;

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    //シリアルポートの内容を読み込む関数
    private void Read()
    {
        while (isRunninng_ && serialPort_ != null && serialPort_.IsOpen)
        {
            try
            {
                message_ = serialPort_.ReadLine();
                isNewMessageReceived_ = true;
            }
            catch (System.Exception e)
            {
                //Debug.LogWarning(e.Message);
            }
        }
    }

    //シリアルポートにテキストの書き出しを行う関数
    //例: Write("書き出したいテキスト")
    public void Write(byte[] portNum, int Dir, int vibStr)
    {
        try
        {
            serialPort_.Write(portNum, Dir, vibStr);
        }
        catch (System.Exception e)
        {
            //Debug.LogWarning(e.Message);
        }
    }

    //モーター制御用関数
    //「,」区切り「.」終了で
    //Motor(モーター番号,ON OFF)
    public void Motor(int motorNum, int rotate)
    {
        string serialText = motorNum.ToString() + rotate.ToString();
        //Debug.Log(serialText);

        try
        {
            serialPort_.Write(serialText);
        }
        catch (System.Exception e)
        {
           //Debug.LogWarning(e.Message);
        }
    }

    public void Motor_Stop(int motorNum)
    {
        string serialText = motorNum + ",2,0.";

        try
        {
            serialPort_.Write(serialText);
        }
        catch (System.Exception e)
        {
            //Debug.LogWarning(e.Message);
        }
    }

    /*
    public void LedLighter(int Level)
    {
        string serialText = "2," + Level +  ",0.";

        try
        {
            serialPort_.Write(serialText);
        }
        catch (System.Exception e)
        {
            //Debug.LogWarning(e.Message);
        }

    }
    */
}
