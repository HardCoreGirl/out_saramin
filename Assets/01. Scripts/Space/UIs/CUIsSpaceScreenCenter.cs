using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CUIsSpaceScreenCenter : MonoBehaviour
{
    public GameObject m_goMain;
    public GameObject m_goDetail;

    public GameObject m_goLGTKMain;
    public GameObject m_goLGTKTalkBox;

    public GameObject m_goAgree;
    public Toggle m_toggleAgree;
    public GameObject m_goBtnPlay;
    
    // Start is called before the first frame update
    void Start()
    {
        HideDetail();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTKManager()
    {
        m_goLGTKMain.SetActive(true);
        m_goAgree.SetActive(true);
        UpdateButtonPlay();
        //m_goLGTKMain.GetComponent<CUIsLGTKManager>().InitLGTK();
    }

    public void OnClickExit()
    {
        CUIsSpaceManager.Instance.HideAllPage();
    }

    public void OnClickDetail()
    {
        ShowDetail();
    }

    public void OnClickExitDetail()
    {
        HideDetail();
    }

    public void ShowDetail()
    {
        m_goDetail.SetActive(true);
    }

    public void HideDetail()
    {
        m_goDetail.SetActive(false);
    }

    public void OnChangeToggle()
    {
        UpdateButtonPlay();
    }

    public void UpdateButtonPlay()
    {
        if (m_toggleAgree.isOn)
        {
            m_goBtnPlay.GetComponent<Button>().enabled = true;
            m_goBtnPlay.GetComponent<Image>().color = new Color(0, 0.5215687f, 1f);
        }
        else
        {
            m_goBtnPlay.GetComponent<Button>().enabled = false;
            m_goBtnPlay.GetComponent<Image>().color = new Color(0.7372549f, 0.8431373f, 1f);
        }
    }

    public void OnClickPlay()
    {
        if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            if (CQuizData.Instance.GetExamInfoDetail("LGTK").status.Equals("WAITING"))
            {
                Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("LGTK").idx);
            }

            if( CQuizData.Instance.GetQuiz("LGTK").sets[0].questions[0].test_answers[0].test_anwr_idx != 0 )
            {
                m_goLGTKMain.GetComponent<CUIsLGTKManager>().SetTutorial(false);
            }
        } else
        {
            //m_goLGTKMain.GetComponent<CUIsLGTKManager>().SetTutorial(false);
        }
        m_goAgree.SetActive(false);
        m_goLGTKMain.GetComponent<CUIsLGTKManager>().InitLGTK();
    }

}
