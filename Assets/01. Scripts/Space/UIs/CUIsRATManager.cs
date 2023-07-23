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

    public Text m_txtQuizTitle;
    public Image[] m_listImageHint = new Image[5];
    public Text[] m_listTxtHintWord = new Text[16];

    public GameObject[] m_listImageHintTutorial = new GameObject[5];

    public InputField m_ifAnswer;
    public TMPro.TMP_InputField m_ifAnswerTmp;

    public GameObject m_goPopupSendAnswer;
    public Text m_txtSendAnswerRemainTime;

    public GameObject m_goPopupTimeover;

    public GameObject m_goPopupToLobby;
    public Text m_txtToLobbyMsg;
    public Text m_txtToLobbyRemainTime;

    public GameObject m_goPopupToLobbyOver;
    public Text m_txtToLobbyOverMsg;
    public Text m_txtToLobbyOverRemainTime;

    public GameObject m_goPopupToLobbyTutorial;

    private int m_nTutorialStep = 0;
    private int m_nRemainTime = 0;

    private int m_nQuizIndex = 0;

    private string[] m_listHintURL = new string[5];

    private int m_nOpenHint = 0;
    private float m_fScore = 0f;

    private bool m_bIsPlayExam = false;

    private bool m_bIsFirstQuiz = true;

    // Start is called before the first frame update
    void Start()
    {
        //InitRATPage();
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

    IEnumerator ProcessDisplayHintImage()
    {
        Quiz quizData = CQuizData.Instance.GetQuiz("RAT");
        STPacketAnswerDictionaries stPacketAnswerDictonaries = CQuizData.Instance.GetAnswerDictionaries(quizData.sets[m_nQuizIndex].questions[0].qst_dics);

        while (true)
        {
            if (stPacketAnswerDictonaries != null)
                break;

            yield return new WaitForSeconds(0.2f);

            stPacketAnswerDictonaries = CQuizData.Instance.GetAnswerDictionaries(quizData.sets[m_nQuizIndex].questions[0].qst_dics);
        }


        int[] listDicIndex = new int[stPacketAnswerDictonaries.body.Length];

        for (int i = 0; i < listDicIndex.Length; i++)
        {
            listDicIndex[i] = stPacketAnswerDictonaries.body[i].dic_word_no;
        }

        int nHintIndex = 0;
        int nOpenHintIndex = 3;
        for (int i = 0; i < listDicIndex.Length; i++)
        {
            for (int j = 0; j < stPacketAnswerDictonaries.body.Length; j++)
            {
                if (listDicIndex[i] == stPacketAnswerDictonaries.body[j].dic_word_no)
                {
                    Debug.Log("Init Rat Page - Hint Score : " + stPacketAnswerDictonaries.body[j].dic_word_no + ", " + stPacketAnswerDictonaries.body[j].score);
                    if (stPacketAnswerDictonaries.body[j].score < 0)
                    {
                        m_listHintURL[nOpenHintIndex] = Server.Instance.GetCurURL() + stPacketAnswerDictonaries.body[j].word_name;
                        nOpenHintIndex++;
                    }
                    else
                    {
                        m_listHintURL[nHintIndex] = Server.Instance.GetCurURL() + stPacketAnswerDictonaries.body[j].word_name;
                        nHintIndex++;
                    }
                    break;
                }
            }
        }

        StartCoroutine("GetTexture");
    }

    public void PlayExam()
    {
        m_bIsPlayExam = true;
    }

    public void InitRATPage()
    {
        m_goTutorialMsg.SetActive(false);
        HideAllPopup();

        //m_ifAnswer.text = "";

        m_ifAnswerTmp.text = "";

        m_nOpenHint = 0;
        m_fScore = 0f;

        for (int i = 0; i < m_listImageHintTutorial.Length; i++)
        {
            m_listImageHintTutorial[i].SetActive(false);
        }
        Quiz quizData = CQuizData.Instance.GetQuiz("RAT");

        if (m_bIsFirstQuiz)
        {
            if (quizData.exm_time != quizData.progress_time)
            {
                CUIsSpaceScreenLeft.Instance.SetRATTutorial(false);

                if (quizData.sets[0].questions[0].test_answers[0].test_anwr_idx == 0)
                {
                    m_nQuizIndex = 0;
                }
                else
                {
                    m_nQuizIndex = 1;
                }
            }
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

            StartCoroutine("GetTexture");
        }
        else
        {
            //Quiz quizData = CQuizData.Instance.GetQuiz("RAT");
            StopCoroutine("ProcessPlayExam");
            m_txtQuizTitle.text = quizData.sets[m_nQuizIndex].questions[0].qst_cnnt;

            string[] listHintWord = quizData.sets[m_nQuizIndex].qst_brws_cnnt.Split(", ");

            for (int i = 0; i < m_listTxtHintWord.Length; i++)
            {
                if (i >= listHintWord.Length)
                    break;

                m_listTxtHintWord[i].text = listHintWord[i];
            }

            if (m_nQuizIndex == 0)
            {
                //m_nRemainTime = 60;
                //m_nRemainTime = quizData.exm_time;
                m_nRemainTime = quizData.progress_time;
                StartCoroutine("ProcessPlayExam");

                StartCoroutine("ProcessDisplayHintImage");

                //STPacketAnswerDictionaries stPacketAnswerDictonaries = CQuizData.Instance.GetAnswerDictionaries(quizData.sets[m_nQuizIndex].questions[0].qst_dics);

                //int[] listDicIndex = new int[stPacketAnswerDictonaries.body.Length];

                //for(int i = 0; i < listDicIndex.Length; i++)
                //{
                //    listDicIndex[i] = stPacketAnswerDictonaries.body[i].dic_word_no;
                //}

                //int nHintIndex = 0;
                //int nOpenHintIndex = 3;
                //for (int i = 0; i < listDicIndex.Length; i++)
                //{
                //    for(int j = 0; j < stPacketAnswerDictonaries.body.Length; j++)
                //    {
                //        if( listDicIndex[i] == stPacketAnswerDictonaries.body[j].dic_word_no)
                //        {
                //            Debug.Log("Init Rat Page - Hint Score : " + stPacketAnswerDictonaries.body[j].dic_word_no + ", " + stPacketAnswerDictonaries.body[j].score);
                //            if(stPacketAnswerDictonaries.body[j].score < 0)
                //            {
                //                m_listHintURL[nOpenHintIndex] = Server.Instance.GetCurURL() + stPacketAnswerDictonaries.body[j].word_name;
                //                nOpenHintIndex++;
                //            } else
                //            {
                //                m_listHintURL[nHintIndex] = Server.Instance.GetCurURL() + stPacketAnswerDictonaries.body[j].word_name;
                //                nHintIndex++;
                //            }
                //            break;
                //        }
                //    }
                //}



                //for (int i = 0; i < 5; i++)
                //{
                //    if (stPacketAnswerDictonaries.body[i] == null)
                //        break;

                //    m_listHintURL[i] = Server.Instance.GetCurURL() + stPacketAnswerDictonaries.body[i].word_name;

                //    Debug.Log("RAT Quiz 01 : " + m_listHintURL[i]);
                //}
                // TODO
                //string strWordHind = quizData.sets[0].questions[0].qst_brws_cnnt;
                //string[] listWordHint = strWordHind.Split(',');
                //for (int i = 0; i < listWordHint.Length; i++)
                //    m_listTxtHintWord[i].text = listWordHint[i];

                m_txtBtnSendAnswer.text = "다음 문제(1/2)";

            } else
            {
                if( m_bIsFirstQuiz )
                    m_nRemainTime = quizData.progress_time;

                StartCoroutine("ProcessPlayExam");
                StartCoroutine("ProcessDisplayHintImage");
                //STPacketAnswerDictionaries stPacketAnswerDictonaries = CQuizData.Instance.GetAnswerDictionaries(quizData.sets[m_nQuizIndex].questions[0].qst_dics);


                //int[] listDicIndex = new int[stPacketAnswerDictonaries.body.Length];

                //for (int i = 0; i < listDicIndex.Length; i++)
                //{
                //    Debug.Log("InitRATPage Index " + i + ", " + stPacketAnswerDictonaries.body[i].dic_word_no + ", " + stPacketAnswerDictonaries.body[i].score);
                //    listDicIndex[i] = stPacketAnswerDictonaries.body[i].dic_word_no;
                //}

                //int nHintIndex = 0;
                //int nOpenHintIndex = 3;
                //for (int i = 0; i < listDicIndex.Length; i++)
                //{
                //    for (int j = 0; j < stPacketAnswerDictonaries.body.Length; j++)
                //    {
                //        if (listDicIndex[i] == stPacketAnswerDictonaries.body[j].dic_word_no)
                //        {
                //            Debug.Log("Init Rat Page - Hint Score : " + stPacketAnswerDictonaries.body[j].dic_word_no + ", " + stPacketAnswerDictonaries.body[j].score);
                //            if (stPacketAnswerDictonaries.body[j].score < 0)
                //            {
                //                m_listHintURL[nOpenHintIndex] = Server.Instance.GetCurURL() + stPacketAnswerDictonaries.body[j].word_name;
                //                nOpenHintIndex++;
                //            }
                //            else
                //            {
                //                m_listHintURL[nHintIndex] = Server.Instance.GetCurURL() + stPacketAnswerDictonaries.body[j].word_name;
                //                nHintIndex++;
                //            }
                //            break;
                //        }
                //    }
                //}


                //for (int i = 0; i < 5; i++)
                //{
                //    if (stPacketAnswerDictonaries.body[i] == null)
                //        break;

                //    m_listHintURL[i] = Server.Instance.GetCurURL() + stPacketAnswerDictonaries.body[i].word_name;

                //    Debug.Log("RAT Quiz 02 : " + m_listHintURL[i]);
                //}
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

        //StartCoroutine("GetTexture");

        m_listImageHint[3].color = new Color(1, 1, 1, 0);
        m_listImageHint[4].color = new Color(1, 1, 1, 0);
    }

    IEnumerator ProcessPlayExam()
    {
        int nMin = (int)(m_nRemainTime / 60);
        int nSec = (int)(m_nRemainTime % 60);

        int nRequestTimer = 0;

        m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        while (true)
        {
            if (!CUIsSpaceScreenLeft.Instance.IsRightQuizActive())
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                //Debug.Log("ProcessPlayExam !!!");
                yield return new WaitForSeconds(1f);

                m_nRemainTime--;

                nMin = (int)(m_nRemainTime / 60);
                nSec = (int)(m_nRemainTime % 60);

                m_txtRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

                if ((nRequestTimer % 5) == 0)
                {
                    Server.Instance.RequestPOSTPartTimer(CQuizData.Instance.GetQuiz("RAT").part_idx);
                }
                nRequestTimer++;

                if (m_nRemainTime <= 0)
                    break;
            }
        }

        ShowPopupTimeOver();
    }
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

        StopCoroutine("ProcessPlayExam");
        // 상태값 API 호출 -----------------------------
        //Server.Instance.RequestPUTAnswerSubject(CQuizData.Instance.GetQuiz("RAT").sets[m_nQuizIndex].questions[0].test_qst_idx, CQuizData.Instance.GetQuiz("RAT").sets[m_nQuizIndex].questions[0].answers[0].anwr_idx, m_ifAnswer.text);
        // TODO 230428
        Debug.Log("RAT INDEX !!!!!!!!!!!!!!!! : " + m_nQuizIndex);

        Server.Instance.RequestPUTAnswerSubject(CQuizData.Instance.GetQuiz("RAT").sets[m_nQuizIndex].questions[0].test_qst_idx, CQuizData.Instance.GetQuiz("RAT").sets[m_nQuizIndex].questions[0].answers[0].anwr_idx, m_ifAnswerTmp.text, m_fScore);

        if ( m_nQuizIndex < 1 )
        {
            //Quiz quizData = CQuizData.Instance.GetQuiz("RAT");
            //quizData.sets[0].questions[0].test_answers[0].test_anwr_idx = CQuizData.Instance.GetQuiz("RAT").sets[m_nQuizIndex].questions[0].answers[0].anwr_idx;
            m_bIsFirstQuiz = false;
            m_nQuizIndex++;
            InitRATPage();
            return;
        }

        Server.Instance.RequestPUTQuestionsStatus(CQuizData.Instance.GetQuiz("RAT").part_idx, 1);
        if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("WAITING"))
            {
                Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
            }
        }

        ShowPopupSendAnswer();
    }

    public void OnClickHint(int nIndex)
    {
        if(nIndex == 4)
        {
            if (m_nOpenHint == 0)
                return;
        }

        m_nOpenHint = nIndex;

        if (nIndex == 3)
        {
            if( m_fScore > -1f)
                m_fScore = -1f;
        }
        else
        {
            m_fScore = -2f;
        }

        Debug.Log("OnClick Score : "+ m_fScore);

        if(CUIsSpaceScreenLeft.Instance.IsRATTutorial())
            m_listImageHintTutorial[nIndex].SetActive(true);
        m_listImageHint[nIndex].color = new Color(1, 1, 1, 1);
    }

    public void OnClickExit()
    {
        if (CUIsSpaceScreenLeft.Instance.IsRATTutorial())
            ShowPopupToLobbyTutorial();
        else
            ShowPopupToLobby();
    }

    public void HideAllPopup()
    {
        m_goPopupSendAnswer.SetActive(false);
        m_goPopupTimeover.SetActive(false);
        m_goPopupToLobby.SetActive(false);
        m_goPopupToLobbyOver.SetActive(false);
        HidePopupToLobbyTutorial();
    }

    public void ShowPopupSendAnswer()
    {
        m_goPopupSendAnswer.SetActive(true);
        //int nMin = (int)(m_nRemainTime / 60);
        //int nSec = (int)(m_nRemainTime % 60);

        //m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");

        //StartCoroutine("ProcessToLobbySendAnswer");
    }

    //IEnumerator ProcessToLobbySendAnswer()
    //{
    //    while (true)
    //    {
    //        int nRemainTime = m_nRemainTime;
    //        int nMin = (int)(nRemainTime / 60);
    //        int nSec = (int)(nRemainTime % 60);

    //        m_txtSendAnswerRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
    //        yield return new WaitForEndOfFrame();
    //    }
    //}

    public void ShowPopupTimeOver()
    {
        m_goPopupTimeover.SetActive(true);
    }

    public void ShowPopupToLobby()
    {
        if (CQuizData.Instance.GetEnableExitCount() > 0)
        {
            m_goPopupToLobby.SetActive(true);
            m_txtToLobbyMsg.text = "아직 시간이 남아있습니다. 메인 로비로 이동한 후 다시 본 미션을 수행하려면 총 <color=#FF0000>" + CQuizData.Instance.GetEnableExitCount().ToString() + "</color>번의 메인로비 이동 기회 중 1회 차감됨니다.<color=#FF0000>(" + CQuizData.Instance.GetEnableExitCount().ToString() + "/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>";

            int nMin = (int)(m_nRemainTime / 60);
            int nSec = (int)(m_nRemainTime % 60);

            m_txtToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
        }
        else
        {
            m_goPopupToLobbyOver.SetActive(true);
            m_txtToLobbyOverMsg.text = "메인 로비 이동횟수를 모두 사용하셨습니다 <color=#FF0000>(0/" + CQuizData.Instance.GetMaxExitCount().ToString() + ")</color>.본 미션을 완료한 후에 이동할 수 있습니다.";
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
                m_txtToLobbyRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            if (m_goPopupToLobbyOver.activeSelf)
                m_txtToLobbyOverRemainTime.text = nMin.ToString("00") + ":" + nSec.ToString("00");
            yield return new WaitForEndOfFrame();
        }
    }


    public void ShowPopupToLobbyOver()
    {
        m_goPopupToLobbyOver.SetActive(true);
    }

    public void OnClickPopupSendAnswerNext()
    {
        StopCoroutine("ProcessToLobbySendAnswer");
        CUIsSpaceScreenLeft.Instance.HideAllPages();
        CUIsSpaceScreenLeft.Instance.ShowHPTSPage();
    }

    public void OnClickPopupSendAnswerContinue()
    {
        HideAllPopup();
    }

    public void OnClickPopupToLobby()
    {
        StopCoroutine("ProcessToLobbyRemainTime");
        Server.Instance.RequestPUTActionExit();
        //StopCoroutine("ProcessPlayExam");
        //m_bIsPlayExam = false;
        HideAllPopup();
        //CUIsSpaceScreenLeft.Instance.PageFadeOutRightPage();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.FadeOutComputer();
        CUIsSpaceManager.Instance.ScreenActive(false);
    }

    public void OnClickPopupToLobbyContinue()
    {
        HideAllPopup();
    }

    public void OnClickPopupTimeover()
    {
        Server.Instance.RequestPUTAnswerSubject(CQuizData.Instance.GetQuiz("RAT").sets[m_nQuizIndex].questions[0].test_qst_idx, CQuizData.Instance.GetQuiz("RAT").sets[m_nQuizIndex].questions[0].answers[0].anwr_idx, m_ifAnswerTmp.text, m_fScore);

        Server.Instance.RequestPUTQuestionsStatus(CQuizData.Instance.GetQuiz("RAT").part_idx, 1);
        if (!CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        {
            if (CQuizData.Instance.GetExamInfoDetail("HPTS").status.Equals("WAITING"))
            {
                Server.Instance.RequestPOSTPartJoin(CQuizData.Instance.GetExamInfoDetail("HPTS").idx);
            }
        }

        StopCoroutine("ProcessToLobbySendAnswer");
        CUIsSpaceScreenLeft.Instance.HideAllPages();
        CUIsSpaceScreenLeft.Instance.ShowHPTSPage();
        //StopCoroutine("ProcessPlayExam");
        //HideAllPopup();
        //CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        ////CUIsSpaceManager.Instance.ScreenActive(false);

        //CUIsSpaceManager.Instance.ShowCommonPopupsFinish(CQuizData.Instance.GetQuiz("RAT").part_idx, 1);
        //CUIsSpaceScreenLeft.Instance.HideRightAllPage();
    }

    public void ShowPopupToLobbyTutorial()
    {
        m_goPopupToLobbyTutorial.SetActive(true);
    }

    public void HidePopupToLobbyTutorial()
    {
        m_goPopupToLobbyTutorial.SetActive(false);
    }

    public void OnClickPopupToLobbyTutorialToLobby()
    {
        HideAllPopup();
        //CUIsSpaceScreenLeft.Instance.PageFadeOutRightPage();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.FadeOutComputer();
    }

    public void OnClickPopupToLobbyTutorialClose()
    {
        HidePopupToLobbyTutorial();
    }
}
