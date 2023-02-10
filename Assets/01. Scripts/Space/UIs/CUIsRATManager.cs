using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Networking;

public class CUIsRATManager : MonoBehaviour
{
    public GameObject m_goTutorialMsg;

    public Text m_txtBtnSendAnswer;
    public Text m_txtRemainTime;

    public Image[] m_listImageHint = new Image[5];
    public Text[] m_listTxtHintWord = new Text[16];

    public GameObject[] m_listImageHintTutorial = new GameObject[5];

    public InputField m_ifAnswer;

    public GameObject m_goPopupSendAnswer;
    public Text m_txtSendAnswerRemainTime;

    public GameObject m_goPopupTimeover;

    public GameObject m_goPopupToLobby;
    public Text m_txtToLobbyRemainTime;

    public GameObject m_goPopupToLobbyOver;

    private int m_nTutorialStep = 0;
    private int m_nRemainTime = 0;

    private int m_nQuizIndex = 0;

    private string[] m_listHintURL = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        InitRATPage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GetTexture()
    {
        for (int i = 0; i < 5; i++)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(m_listHintURL[i]);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Rect rect = new Rect(0, 0, myTexture.width, myTexture.height);
                m_listImageHint[i].sprite = Sprite.Create(myTexture, rect, new Vector2(150, 150));
            }
        }
    }

    public void InitRATPage()
    {
        m_goTutorialMsg.SetActive(false);
        HideAllPopup();

        m_ifAnswer.text = "";

        for (int i = 0; i < m_listImageHintTutorial.Length; i++)
        {
            m_listImageHintTutorial[i].SetActive(false);
        }

        if ( CUIsSpaceScreenLeft.Instance.IsRATTutorial() )
        {
            m_txtBtnSendAnswer.text = "본 퀴즈 시작하기";
            m_txtRemainTime.text = "시작전";

            //for(int i = 0; i < m_listImageHintTutorial.Length; i++)
            for (int i = 0; i < 3; i++)
            {
                m_listImageHintTutorial[i].SetActive(true);
            }
            //m_listHintURL[0] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjExMjBfMTM4%2FMDAxNjY4OTAzNjE4NDUz.vUJ7W_roz4Zm8W3i0CGHdU68AgBk-wIv1yvKBzqpC-4g.bCV4H4hhl9HfaEV6L8l6oKSe7U9s9hYabQK46x6by3Ag.JPEG.runabd%2F20221018%25A3%25DF182021.jpg&type=a340";
            //m_listHintURL[1] = "https://search.pstatic.net/sunny/?src=https%3A%2F%2Fi.pinimg.com%2F736x%2Ffc%2F75%2F95%2Ffc7595d8e4d5c0db2124c0c43c00d9ec.jpg&type=a340";
            //m_listHintURL[2] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjExMjZfMTMy%2FMDAxNjY5NDE4MzMwODc5.1LEWPRuLoxijrOBL5q6GX9YvhlX1ngkCnDS29hd-euwg.kjebxyj-NYwihV_5sGS9nToLNHN96iPhdBj28Dj4LOUg.PNG.baekhw1%2F20221126_081425-removebg-preview.png&type=a340";
            //m_listHintURL[3] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA3MDFfNzAg%2FMDAxNjU2Njg2MTMxNjE3.7ncCz9d7XgMyjYUTCZAQWh1gFIYuajZyPP1UxIkJ1mwg.yWa2Z2QakUy1DjkKo2e2_ck8-MzBc4Is8FX-ARKJ-fUg.JPEG.chlwogud123%2F59bbf73b123d0f9f693be3c3de9506b24a1f2a3067b4ffd0207a3a08eee32d750ebf1ca3e330.jpg&type=a340";
            //m_listHintURL[4] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjExMjZfNzAg%2FMDAxNjY5NDY3MDIyODU4.xLZcFMLv9G3K3kDNWsvc2EGE2fvSHYq53TDEqnENCCAg.wDhs92vnxU-3OqLPzip9TSuPNQWouEGJFMVpwx0T5tgg.JPEG.tigersoomi%2FScreenshot%25A3%25DF20221126%25A3%25AD213739%25A3%25DFGoogle.jpg&type=a340";

            m_listTxtHintWord[0].text = "화";
            m_listTxtHintWord[1].text = "식";
            m_listTxtHintWord[2].text = "노";
            m_listTxtHintWord[3].text = "예";
            m_listTxtHintWord[4].text = "순";
            m_listTxtHintWord[5].text = "무";
            m_listTxtHintWord[6].text = "서";
            m_listTxtHintWord[7].text = "한";
            m_listTxtHintWord[8].text = "대";
            m_listTxtHintWord[9].text = "래";
            m_listTxtHintWord[10].text = "문";
            m_listTxtHintWord[11].text = "소";
            m_listTxtHintWord[12].text = "가";
            m_listTxtHintWord[13].text = "작";
            m_listTxtHintWord[14].text = "승";
            m_listTxtHintWord[15].text = "음";
        }
        else
        {
            Quiz quizData = CQuizData.Instance.GetQuiz("RAT");
            if (m_nQuizIndex == 0)
            {
                m_nRemainTime = quizData.exm_time;
                StartCoroutine("ProcessPlayExam");

                for (int i = 0; i < 5; i++)
                {
                    // TODO
                    //m_listHintURL[i] = quizData.sets[0].questions[0].qst_dics[i].dic_wrd_nm;
                    Debug.Log("RAT Quiz : " + m_listHintURL[i]);
                }
                // TODO
                //string strWordHind = quizData.sets[0].questions[0].qst_brws_cnnt;
                //string[] listWordHint = strWordHind.Split(',');
                //for (int i = 0; i < listWordHint.Length; i++)
                //    m_listTxtHintWord[i].text = listWordHint[i];

                m_txtBtnSendAnswer.text = "다음 문제(1/2)";

            } else
            {
                // TODO
                //for (int i = 0; i < 5; i++)
                //{
                //    m_listHintURL[i] = quizData.sets[1].questions[0].qst_dics[i].dic_wrd_nm;
                //}

                //string strWordHind = quizData.sets[1].questions[0].qst_brws_cnnt;
                //string[] listWordHint = strWordHind.Split(',');
                //for (int i = 0; i < listWordHint.Length; i++)
                //    m_listTxtHintWord[i].text = listWordHint[i];
                m_txtBtnSendAnswer.text = "답변 제출하기";
            }
        }

        StartCoroutine("GetTexture");

        m_listImageHint[3].color = new Color(1, 1, 1, 0);
        m_listImageHint[4].color = new Color(1, 1, 1, 0);
    }

    IEnumerator ProcessPlayExam()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        while (true)
        {
            yield return new WaitForSeconds(1f);

            m_nRemainTime--;

            nMin = (int)(m_nRemainTime / 60);
            nSec = (int)(m_nRemainTime % 60);

            m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

            if (m_nRemainTime == 0)
                break;
        }

        ShowPopupTimeOver();
    }

    //public void ShowTutorialMsg()
    //{
    //    m_goTutorialMsg.SetActive(true);
    //}

    public void ShowTutorialMsg()
    {
        //m_goTutorialMsg.SetActive(true);
        StartCoroutine("ProcessTutorialMsg");
    }

    IEnumerator ProcessTutorialMsg()
    {
        m_goTutorialMsg.SetActive(true);
        yield return new WaitForSeconds(5f);
        HideTutorialMsg();
    }

    public void HideTutorialMsg()
    {
        m_goTutorialMsg.SetActive(false);
    }

    public void OnClickSendAnswer()
    {
        if (CUIsSpaceScreenLeft.Instance.IsRATTutorial())
        {
            if (m_nTutorialStep == 0)
            {
                m_nTutorialStep++;
                ShowTutorialMsg();
            }
            else
            {
                CUIsSpaceScreenLeft.Instance.SetRATTutorial(false);
                InitRATPage();
            }
            return;
        }

        Server.Instance.RequestPUTAnswerSubject(CQuizData.Instance.GetQuiz("RAT").sets[m_nQuizIndex].questions[0].test_qst_idx, m_ifAnswer.text);

        if ( m_nQuizIndex < 1 )
        {
            m_nQuizIndex++;
            InitRATPage();
            return;
        }
        
        ShowPopupSendAnswer();
    }

    public void OnClickHint(int nIndex)
    {
        if(CUIsSpaceScreenLeft.Instance.IsRATTutorial())
            m_listImageHintTutorial[nIndex].SetActive(true);
        m_listImageHint[nIndex].color = new Color(1, 1, 1, 1);
    }

    public void OnClickExit()
    {
        ShowPopupToLobby();
    }

    public void HideAllPopup()
    {
        m_goPopupSendAnswer.SetActive(false);
        m_goPopupTimeover.SetActive(false);
        m_goPopupToLobby.SetActive(false);
        m_goPopupToLobbyOver.SetActive(false);
    }

    public void ShowPopupSendAnswer()
    {
        m_goPopupSendAnswer.SetActive(true);
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
    }

    public void ShowPopupTimeOver()
    {
        m_goPopupTimeover.SetActive(true);
    }

    public void ShowPopupToLobby()
    {
        m_goPopupToLobby.SetActive(true);

        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        m_txtToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
    }

    public void ShowPopupToLobbyOver()
    {
        m_goPopupToLobbyOver.SetActive(true);
    }

    public void OnClickPopupSendAnswerNext()
    {
        CUIsSpaceScreenLeft.Instance.HideAllPages();
        CUIsSpaceScreenLeft.Instance.ShowHPTSPage();
    }

    public void OnClickPopupSendAnswerContinue()
    {
        HideAllPopup();
    }

    public void OnClickPopupToLobby()
    {
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.ScreenActive(false);
    }

    public void OnClickPopupToLobbyContinue()
    {
        HideAllPopup();
    }

    public void OnClickPopupTimeover()
    {
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.ScreenActive(false);
    }
}
