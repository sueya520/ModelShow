using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Vuforia;

public class ScreenShot : MonoBehaviour {
    /// <summary>照片名稱</summary>
    public string fileName;
    /// <summary>相簿名稱</summary>
    public string albumName;
    /// <summary>拍照成功訊息文字</summary>
    //拍照成功訊息
    public string successMsg;
    /// <summary>拍照失敗訊息文字</summary>
    public string failMsg;
    /// <summary>UIManager.cs</summary>
    public UIManager UI;
    /// <summary>TrackStatusManager.cs</summary>
    public TrackableStatusManager[] TSM;

    void Update()
    {
        //編輯模式
        #if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
        //手機模式
        #elif UNITY_ANDROID || UNITY_IPHONE
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        #endif
        {
            //自動對焦
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
    /// <summary>拍照</summary>
    public void Shot()
    {
        //辨識目標數量
        int trackAmount = 0;
        foreach(TrackableStatusManager i in TSM)
        {
            //辨識目標為辨識狀態
			if (i.Status == TrackableStatusManager.TurnStates.TRACK)
            {
                //辨識目標數量增加
                trackAmount++;
                //啟動拍照程序
                StartCoroutine(ScreenshotManager.Save(fileName, SuccessShot, FailShot, albumName: albumName, callback: true));
                break;
            }
        }
        //無辨識目標
        if (trackAmount == 0)
        {
            FailShot();
        }
        
    }
    /// <summary>拍照成功訊息</summary>
    public void SuccessShot()
    {
        UI.ScreenShotMessage(successMsg);
    }
    /// <summary>拍照失敗訊息</summary>
    public void FailShot()
    {
        UI.ScreenShotMessage(failMsg);
    }
}
