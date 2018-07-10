using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    /// <summary>TrackStatusManager.cs</summary>
    public TrackableStatusManager[] TSM;
    /// <summary>相機對焦框圖片</summary>
    public Image Fouce_img;
    /// <summary>拍照訊息圖片</summary>
    public GameObject SSM_Panel;
    /// <summary>拍照訊息</summary>
    public Text SSM_text;
    void Update()
    {
        foreach (TrackableStatusManager i in TSM)
        {
            //辨識目標為辨識狀態
            if (i.Status == TrackableStatusManager.TurnStates.TRACK)
            {
                //關閉相機對焦框
                Fouce_img.enabled = false;
                break;
            }
            else
            {
                //開啟相機對焦框
                Fouce_img.enabled = true;
            }
        }
    }
    /// <summary>拍照訊息設定</summary>
    /// <param name="msg">拍照訊息文字</param>
    public void ScreenShotMessage(string msg = "")
    {
        //設定拍照訊息
        SSM_text.text = msg;
        ScreenShotMessageSwitch(true);
    }
    /// <summary>拍照訊息開關</summary>
    /// <param name="switches">開啟true/關閉false</param>
    public void ScreenShotMessageSwitch(bool switches)
    {
        //開啟拍照訊息圖片
        SSM_Panel.SetActive(switches);
    }
}
