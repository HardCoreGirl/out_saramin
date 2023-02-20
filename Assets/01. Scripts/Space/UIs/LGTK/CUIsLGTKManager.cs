using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsLGTKManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsLGTKManager _instance = null;

    public static CUIsLGTKManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsLGTKManager install null");

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            _instance = null;
        }
    }
    #endregion

    public GameObject m_goTalkBox;

    public GameObject m_goPopupFinish;
    public GameObject m_goPopupTimeOver;
    public GameObject m_goPopupToLobby;
    public Text m_txtPopupToLobbyMsg;
    public Text m_txtPopupToLobbyRemainTime;
    public GameObject m_goPopupToLobbyOver;
    public Text m_txtPopupToLobbyOverMsg;
    public Text m_txtPopupToLobbyOverRemainTime;

    public Text m_txtRemain;

    public GameObject m_goDropdownContent;

    private bool m_bIsTutorial = true;
    private int m_nRemainTime;

    // Start is called before the first frame update
    void Start()
    {
        //InitLGTK();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTK()
    {
        HideTalkBox();

        if( m_bIsTutorial)
        {
            m_txtRemain.text = "시작전";
            OnClickTalkBox();
        } else
        {
            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
            //m_nRemainTime = quizLGTK.exm_time;
            m_nRemainTime = quizLGTK.progress_time;
            //m_nRemainTime = 30;
            Debug.Log("LGTK Remain Time : " + m_nRemainTime);

            // TODO : Set_Gudes
            //for(int i= 0; i < quizLGTK.set_gudes.Length; i++)
            //{
            //    GameObject goDropdown = Instantiate(Resources.Load("Prefabs/LGTKDropdown") as GameObject);
            //    goDropdown.transform.parent = m_goDropdownContent.transform;
            //    goDropdown.GetComponent<CObjectLGTKDropdown>().InitLGTKDropdown(quizLGTK.set_gudes[i].gude_nm, quizLGTK.set_gudes[i].gude_seur_grd, quizLGTK.set_gudes[i].gude_reg_dtm);
            //}

            StartCoroutine("ProcessPlayExam");
        }
    }

    IEnumerator ProcessPlayExam()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        int nRequestTimer = 0;

        m_txtRemain.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        while (true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;

            nMin = (int)(m_nRemainTime / 60);
            nSec = (int)(m_nRemainTime % 60);

            m_txtRemain.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            if ((nRequestTimer % 5) == 0)
            {
                Server.Instance.RequestPOSTPartTimer(CQuizData.Instance.GetQuiz("LGTK").part_idx);
            }
            nRequestTimer++;


            if (m_nRemainTime <= 0)
                break;
        }

        HideAllPopup();
        ShowPopupTimeOver();
    }
    //IEnumerator ProcessQuiz()
    //{

    //}

    public void OnClickExit()
    {
        ShowPopupToLobby();
    }

    public void ShowTalkBox()
    {
        m_goTalkBox.SetActive(true);
    }

    public void HideTalkBox()
    {
        m_goTalkBox.SetActive(false);
    }

    public void HideAllPopup()
    {
        HidePopupFinish();
        HidePopupTimeOver();
    }

    public void ShowPopupFinish()
    {
        m_goPopupFinish.SetActive(true);
    }

    public void HidePopupFinish()
    {
        m_goPopupFinish.SetActive(false);
    }

    public void OnClickPopupFinishOK()
    {
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.ScreenActive(false);
    }

    public void ShowPopupTimeOver()
    {
        m_goPopupTimeOver.SetActive(true);
    }

    public void HidePopupTimeOver()
    {
        m_goPopupTimeOver.SetActive(false);
    }

    public void OnClickPopupTimeOverLobby()
    {
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        //CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        //CUIsSpaceManager.Instance.ScreenActive(false);

        gameObject.SetActive(false);

        Debug.Log("OnClickPopupFinishOK");
        CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("LGTK").part_idx, 2);
        CUIsSpaceManager.Instance.HideCenterPage();


    }

    // Popup To Lobby -------------------------------
    public void ShowPopupToLobby()
    {
        if( m_bIsTutorial )
        {
            CUIsSpaceManager.Instance.ScreenActive(false, true);
            CUIsSpaceManager.Instance.HideCenterPage();
            return;
        }

        if (CQuizData.Instance.GetEnableExitCount() > 0)
        {
            m_goPopupToLobby.SetActive(true);
            m_txtPopupToLobbyMsg.text = "아직 시간이 남아있습니다. 메인 로비로 이동한 후 다시 본 미션을 수행하려면 총 <color=#FF0000>" + CQuizData.Instance.GetEnableExitCount().ToString() + "</color>번의 메인로비 이동 기회 중 1회 차감됨니다.<color=#FF0000>(" + CQuizData.Instance.GetEnableExitCount().ToString() + "/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>";
        }
        else
        {
            m_goPopupToLobbyOver.SetActive(true);
            m_txtPopupToLobbyOverMsg.text = "메인 로비 이동횟수를 모두 사용하셨습니다 <color=#FF0000>(0/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>.본 미션을 완료한 후에 이동할 수 있습니다.";
        }

        StartCoroutine("ProcessToLobbyRemainTime");
    }

    IEnumerator ProcessToLobbyRemainTime()
    {
        while (true)
        {
            int nRemainTime = m_nRemainTime;
            int nMin = (int)(nRemainTime / 60);
            int nSec = (int)(nRemainTime % 60);

            if (m_goPopupToLobby.activeSelf)
                m_txtPopupToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            if (m_goPopupToLobbyOver.activeSelf)
                m_txtPopupToLobbyOverRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            yield return new WaitForEndOfFrame();
        }
    }

    public void HidePopupToLobby()
    {
        StopCoroutine("ProcessToLobbyRemainTime");
        m_goPopupToLobby.SetActive(false);
        m_goPopupToLobbyOver.SetActive(false);
    }

    public void OnClickPopupToLobbyToLobby()
    {
        Server.Instance.RequestPUTActionExit();
        HidePopupToLobby();
        CUIsSpaceManager.Instance.ScreenActive(false, true);
        CUIsSpaceManager.Instance.HideCenterPage();
    }

    public void OnClickPopupToLobbyExit()
    {
        HidePopupToLobby();
    }
    // -----------------------------------------------------------

    public void OnClickTalkBox()
    {
        ShowTalkBox();

        m_goTalkBox.GetComponent<CUIsLGTKTalkBoxManager>().InitLGTKTalkBoxMansger();
    }

    public void SetTutorial(bool bIsTutorial)
    {
        m_bIsTutorial = bIsTutorial;
    }

    public bool IsTutorial()
    {
        return m_bIsTutorial;
    }
}
