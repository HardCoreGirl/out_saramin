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
            m_txtRemain.text = "Ω√¿€¿¸";
            OnClickTalkBox();
        } else
        {
            Quiz quizLGTK = CQuizData.Instance.GetQuiz("LGTK");
            m_nRemainTime = quizLGTK.exm_time;
            Debug.Log("LGTK Remain Time : " + m_nRemainTime);
            //m_nRemainTime = 30;

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

        m_txtRemain.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        while (true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;

            nMin = (int)(m_nRemainTime / 60);
            nSec = (int)(m_nRemainTime % 60);

            m_txtRemain.text = nMin.ToString("00") + ":" + nSec.ToString("00");

            if (m_nRemainTime == 0)
                break;
        }

        HideAllPopup();
        ShowPopupTimeOver();
    }
    //IEnumerator ProcessQuiz()
    //{

    //}

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
        //CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.ScreenActive(false);

        gameObject.SetActive(false);
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
        CUIsSpaceManager.Instance.ScreenActive(false);

        gameObject.SetActive(false);
    }

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
