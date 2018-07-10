using UnityEngine;
using System.Collections;

public class RaycastModel : MonoBehaviour
{
    /// <summary>TrackableStatusManager.cs</summary>
    public TrackableStatusManager TSM;
    /// <summary>AudioManager.cs</summary>
    public AudioManager AM;
    /// <summary>ModelManager.cs</summary>
    public ModelManager ModelManager;
    /// <summary>要開啟的家具模型</summary>
    //public GameObject ShowObject;
    /// <summary>正向順序顯示模型</summary>
    public bool ShowModelPositive;
    /// <summary>是否撥放音樂</summary>
    public bool PlayAudio;
    /// <summary>是否撥放音效</summary>
    public bool PlaySoundEffect;
    /// <summary>撥放第N首音樂</summary>
    public int PlayAudioOrder;
    /// <summary>撥放第N首音效</summary>
    public int PlaySoundEffectOrder;
    /// <summary>顯示模型編號</summary>
    private static int ShowObjectNum = 0;
    void Update()
    {
        //手機模式
        //點擊螢幕
#if UNITY_EDITOR
        //PC編輯模式
        //點擊滑鼠左鍵
        if (Input.GetMouseButtonDown(0))
        {
            //雷射線初始化
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //雷射線碰撞
            RaycastHit hit;
            //雷射線碰撞到物件且碰撞到本體
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == this.gameObject)
            {
                //辨識目標為辨識狀態
                if (TSM.Status == TrackableStatusManager.TurnStates.TRACK)
                {
                    //ModelManager.cs不是空值
                    if (ModelManager != null)
                    {
                        //本體不是點擊狀態
                        //if (!ModelManager.isBtnPress(gameObject))
                        //{
                        //更換本體貼圖
                        //ModelManager.ButtonTexture(gameObject);
                        //正向順序
                        if (ShowModelPositive)
                        {
                            //增加顯示模型編號
                            ShowObjectNum += 1;
                        }
                        else
                        {
                            //減少顯示模型編號
                            ShowObjectNum -= 1;
                        }
                        //模型編號大於模型陣列長度
                        if (ShowObjectNum > ModelManager.Model.Length - 1)
                        {
                            //模型編號歸零
                            ShowObjectNum = 0;
                        }
                        //模型編號小於0
                        else if (ShowObjectNum < 0)
                        {
                            //模型編號等於最後一個模型陣列長度
                            ShowObjectNum = ModelManager.Model.Length - 1;
                        }
                        //開啟相對應家具模型
                        ModelManager.ShowModel(ModelManager.Model[ShowObjectNum]);
                        //AudioManager.cs不是空值
                        if (AM != null)
                        {
                            //撥放音樂狀態
                            if (PlayAudio)
                                //撥放音樂
                                AM.AudioPlay(PlayAudioOrder);

                            //撥放音效狀態
                            if (PlaySoundEffect)
                                //撥放音效
                                AM.SoundEffectPlay(PlaySoundEffectOrder);
                        }
                    }
                }
            }
        }
#elif UNITY_ANDROID || UNITY_IPHONE
        //點擊螢幕
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //雷射線初始化
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //雷射線碰撞
            RaycastHit hit;
            //雷射線碰撞到物件且碰撞到本體
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject == this.gameObject)
            {
                //辨識目標為辨識狀態
                if (TSM.Status == TrackableStatusManager.TurnStates.TRACK)
                {
                    //ModelManager.cs不是空值
                    if (ModelManager != null)
                    {
                        //本體不是點擊狀態
                        if (!ModelManager.isBtnPress(gameObject))
                        {
                            //更換本體貼圖
                            //ModelManager.ButtonTexture(gameObject);
                            //正向順序
                            if (ShowModelPositive)
                            {
                                //增加顯示模型編號
                                ShowObjectNum++;
                            }
                            else
                            {
                                //減少顯示模型編號
                                ShowObjectNum--;
                            }
                            //模型編號大於最大模型陣列編號
                            if(ShowObjectNum > ModelManager.Model.Length-1)
                            {
                                //模型編號歸零
                                ShowObjectNum = 0;
                            }
                            //模型編號小於0
                            else if (ShowObjectNum < 0)
                            {
                                //模型編號等於最後一個模型陣列編號
                                ShowObjectNum = ModelManager.Model.Length-1;
                            }
                            //開啟相對應家具模型
                            ModelManager.ShowModel(ModelManager.Model[ShowObjectNum]);
                            //AudioManager.cs不是空值
                            if (AM != null)
                            {
                                //撥放音樂狀態
                                if (PlayAudio)
                                    //撥放音樂
                                    AM.AudioPlay(PlayAudioOrder);
                                //撥放音效狀態
                                if (PlaySoundEffect)
                                    //撥放音效
                                    AM.SoundEffectPlay(PlaySoundEffectOrder);
                            }
                        }
                    }
                }
            }
        } 
#endif
    }
}
