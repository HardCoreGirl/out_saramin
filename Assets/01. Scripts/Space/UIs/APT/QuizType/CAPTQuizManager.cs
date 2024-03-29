using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Networking;

public class CAPTQuizManager : MonoBehaviour
{
    public Image m_imgQuiz;
    public Text m_txtQuiz;
    public Image[] m_listImgAnswer = new Image[4];
    public Text[] m_listTxtAnswer = new Text[4];
    public GameObject[] m_listSelect = new GameObject[4];

    private int m_nType;
    private int m_nIndex;
    private int m_nQuizListIndex;

    private string m_strQuizURL;
    private string[] m_listStrAnswerURL = new string[4];

    private Quiz m_quizInfo;

    private int m_nSelectIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitQuizType(int nType, int nIndex, int nQuizListIndex)
    {
        m_nType = nType;
        m_nIndex = nIndex;
        m_nQuizListIndex = nQuizListIndex;

        if (CUIsAPTPage2Manager.Instance.IsTutorial())
            return;

        string strKey = "APTD1";
        if (m_nType == 1)
            strKey = "APTD2";

        m_quizInfo = CQuizData.Instance.GetQuiz(strKey);


        InitAnswer();

        m_nSelectIndex = -1;
        if( nType != -1 )   // 연습문제가 아니라면
        {
            m_nSelectIndex = CUIsAPTPage2Manager.Instance.GetSelectIndex(nIndex);
        }
        UpdateSelect();


        //quizAPT.sets[nIndex].questions[0].qst_ans_cnt  // 문제 URL

        //if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
        //{
        //    m_strQuizURL = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA5MDlfNTYg%2FMDAxNjYyNjU2NTM3ODcw.FKk4FnNMTz0EMO_-5T4IQLVuzgNqjdp-qABMVspwNL0g.hzITL47Q33xEWKyvgKlib_aTMIeJ5lKM8wTNKTFFFYkg.JPEG.yongminjoe%2Fnewjeans_official_1662627626_1.jpg&type=sc960_832";
        //    for (int i = 0; i < m_quizInfo.sets[nIndex].questions[0].answers.Length; i++)
        //    {
        //        // 정답 URL
        //        //quizAPT.sets[nIndex].questions[0].answers[i].anwr_cnnt
        //        //m_listStrAnswerURL[i] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjEwMDlfMjA4%2FMDAxNjY1Mjg5NjkwNDcw.PU1zLsWkwUFVqasfKdg3isaQrWZWu6tKRbxYcgvtKJ0g.oXT70SvfyeTxN1y_bY2__QQF8tciooCjZMGuzjouCjYg.JPEG.dedoeoh%2FFejY9GNaAAEvJSc.jpeg&type=sc960_832";
        //        m_listStrAnswerURL[i] = "https://rrrrrrrrr.com";
        //    }

        //    m_listStrAnswerURL[1] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjExMDZfMjQw%2FMDAxNjY3NzM3OTA1NjYx.e-Rfc1datklaiYBsOU1pPULpt0RXbzYR_42gjBwX6FUg.JEviHd3VQ7U3mblZ2UlIy2KqyoyXGVbxigWGPE3ib4Qg.JPEG.kjky021%2F%25B4%25D9%25BF%25EE%25B7%25CE%25B5%25E5%25C6%25C4%25C0%25CF%25A3%25DF20221106%25A3%25DF213020.jpg&type=sc960_832";
        //    m_listStrAnswerURL[2] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjEyMTRfMTc3%2FMDAxNjcxMDA1MTQ4NDg1.y8B8R8ELubZ25SN8AcfYZU33KvXn0pQ7PLN0UdZ4o9kg.4gwbh0z1GplmuJU-Yph6dL1eBPN_krk6DjbE37fKEWMg.JPEG.pje4476%2F1._%25B4%25BA%25C1%25F8%25BD%25BA_%25C0%25CE%25BD%25BA%25C5%25B8%25B1%25D7%25B7%25A5_%25282%2529.JPG&type=sc960_832";
        //    m_listStrAnswerURL[3] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA5MjdfNDgg%2FMDAxNjY0MjczNTAyMzE3.VoXWovZzMJxX2O_lV3S6QQD66pefrOfYJgxmNCuICsEg.nkN2lw_cSEsFi3UNzPUJpYjsSnKXA5_FiKbyCoqThMAg.JPEG.spring19790%2F4.jpg&type=sc960_832";
        //}
        //else
        //{
        //    m_strQuizURL = Server.Instance.GetCurURL() + m_quizInfo.sets[nIndex].questions[0].qst_cnnt;

        for (int i = 0; i < m_quizInfo.sets[nIndex].questions[0].answers.Length; i++)
        {
            //Debug.Log("URL : " + CQuizData.Instance.GetQuiz(strKey).sets[nIndex].questions[0].answers[i].anwr_cnnt);
            if (m_quizInfo.sets[nIndex].questions[0].answers[i].anwr_brws_cd.Equals("IMG") && CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
            {
                m_listStrAnswerURL[0] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjEwMDlfMjA4%2FMDAxNjY1Mjg5NjkwNDcw.PU1zLsWkwUFVqasfKdg3isaQrWZWu6tKRbxYcgvtKJ0g.oXT70SvfyeTxN1y_bY2__QQF8tciooCjZMGuzjouCjYg.JPEG.dedoeoh%2FFejY9GNaAAEvJSc.jpeg&type=sc960_832";
                m_listStrAnswerURL[1] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjExMDZfMjQw%2FMDAxNjY3NzM3OTA1NjYx.e-Rfc1datklaiYBsOU1pPULpt0RXbzYR_42gjBwX6FUg.JEviHd3VQ7U3mblZ2UlIy2KqyoyXGVbxigWGPE3ib4Qg.JPEG.kjky021%2F%25B4%25D9%25BF%25EE%25B7%25CE%25B5%25E5%25C6%25C4%25C0%25CF%25A3%25DF20221106%25A3%25DF213020.jpg&type=sc960_832";
                m_listStrAnswerURL[2] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjEyMTRfMTc3%2FMDAxNjcxMDA1MTQ4NDg1.y8B8R8ELubZ25SN8AcfYZU33KvXn0pQ7PLN0UdZ4o9kg.4gwbh0z1GplmuJU-Yph6dL1eBPN_krk6DjbE37fKEWMg.JPEG.pje4476%2F1._%25B4%25BA%25C1%25F8%25BD%25BA_%25C0%25CE%25BD%25BA%25C5%25B8%25B1%25D7%25B7%25A5_%25282%2529.JPG&type=sc960_832";
                m_listStrAnswerURL[3] = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA5MjdfNDgg%2FMDAxNjY0MjczNTAyMzE3.VoXWovZzMJxX2O_lV3S6QQD66pefrOfYJgxmNCuICsEg.nkN2lw_cSEsFi3UNzPUJpYjsSnKXA5_FiKbyCoqThMAg.JPEG.spring19790%2F4.jpg&type=sc960_832";

            }
            else
            {
                m_listStrAnswerURL[i] = Server.Instance.GetCurURL() + m_quizInfo.sets[nIndex].questions[0].answers[i].anwr_cnnt;
            }
            
        }
        //}

        StartCoroutine("ProcessQuiz");
        // CUIsRATManager 스프라이트 참조
    }

    public void InitAnswer()
    {
        if (CUIsAPTPage2Manager.Instance.IsTutorial())
            return;

        StopCoroutine("ProcessQuiz");

        if (!m_quizInfo.sets[m_nIndex].questions[0].qst_exos_cd.Equals("FORM_E"))
        {
            m_imgQuiz.color = new Color(0.8627451f, 0.9176471f, 1);
            m_imgQuiz.sprite = null;
            m_txtQuiz.text = "";
        }

        for (int i = 0; i < 4; i++)
        {
            m_listImgAnswer[i].sprite = null;
            m_listTxtAnswer[i].text = "";
        }

        Button[] listBtn = gameObject.GetComponentsInChildren<Button>();
        for (int i = 0; i < listBtn.Length; i++)
        {
            //listBtn[i].spriteState = new SpriteState;
            listBtn[i].interactable = false;
            listBtn[i].interactable = true;
            //Debug.Log("Button !!!!!!!!!!!!!!!!!!!!!!!!");

        }
    }

    IEnumerator ProcessQuiz()
    {
        if (m_quizInfo.sets[m_nIndex].questions[0].qst_exos_cd.Equals("FORM_E"))
        {
            Debug.Log("ProcessQuiz 01 !!!!");
        }
        else
        {
            if (m_quizInfo.sets[m_nIndex].qst_brws_cd != null)
            {
                if (m_quizInfo.sets[m_nIndex].qst_brws_cd.Equals("IMG"))
                {
                    m_txtQuiz.text = "";

                    string strURL = Server.Instance.GetCurURL() + m_quizInfo.sets[m_nIndex].qst_brws_cnnt;
                    if (CSpaceAppEngine.Instance.GetServerType().Equals("LOCAL"))
                        strURL = "https://search.pstatic.net/common/?src=http%3A%2F%2Fblogfiles.naver.net%2FMjAyMjA5MjdfNDgg%2FMDAxNjY0MjczNTAyMzE3.VoXWovZzMJxX2O_lV3S6QQD66pefrOfYJgxmNCuICsEg.nkN2lw_cSEsFi3UNzPUJpYjsSnKXA5_FiKbyCoqThMAg.JPEG.spring19790%2F4.jpg&type=sc960_832";

                    UnityWebRequest www = UnityWebRequestTexture.GetTexture(strURL);
                    yield return www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(www.error);
                    }
                    else
                    {
                        Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                        Rect rect = new Rect(0, 0, myTexture.width, myTexture.height);
                        m_imgQuiz.sprite = Sprite.Create(myTexture, rect, new Vector2(150, 150));
                        m_imgQuiz.color = Color.white;
                    }
                }
                else
                {
                    m_txtQuiz.text = m_quizInfo.sets[m_nIndex].qst_brws_cnnt;
                    //m_listTxtAnswer[3 - i].text = m_quizInfo.sets[m_nIndex].questions[0].answers[i].anwr_cnnt;
                }
            }
        }

        //if(m_quizInfo.sets[m_nIndex].questions[0].qst_exos_cd.Equals("FORM_C"))
        //{

        //} else
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if(m_quizInfo.sets[m_nIndex].questions[0].answers[i].anwr_brws_cd.Equals("IMG"))
        //        {
        //            m_listTxtAnswer[i].text = "";
        //            Debug.Log("Image URL : " + m_listStrAnswerURL[i]);
        //            UnityWebRequest www = UnityWebRequestTexture.GetTexture(m_listStrAnswerURL[i]);
        //            yield return www.SendWebRequest();

        //            if (www.result != UnityWebRequest.Result.Success)
        //            {
        //                Debug.Log(www.error);
        //            }
        //            else
        //            {
        //                Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        //                Rect rect = new Rect(0, 0, myTexture.width, myTexture.height);
        //                m_listImgAnswer[i].sprite = Sprite.Create(myTexture, rect, new Vector2(150, 150));
        //                //m_listImgAnswer[i].color = new Color(0, 0, 0, 1);
        //            }
        //        } else
        //        {
        //            m_listTxtAnswer[i].text = m_quizInfo.sets[m_nIndex].questions[0].answers[i].anwr_cnnt;
        //        }
        //    }
        //}
        for (int i = 0; i < 4; i++)
        {
            if (m_quizInfo.sets[m_nIndex].questions[0].answers[i].anwr_brws_cd.Equals("IMG"))
            {
                m_listTxtAnswer[i].text = "";
                Debug.Log("Image URL : " + m_listStrAnswerURL[i]);
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(m_listStrAnswerURL[i]);
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    Rect rect = new Rect(0, 0, myTexture.width, myTexture.height);
                    m_listImgAnswer[i].sprite = Sprite.Create(myTexture, rect, new Vector2(150, 150));
                    //m_listImgAnswer[i].color = new Color(0, 0, 0, 1);
                }
            }
            else
            {
                m_listTxtAnswer[i].text = m_quizInfo.sets[m_nIndex].questions[0].answers[i].anwr_cnnt;
            }
        }
    }

    public void OnClickAnswer(int nIndex)
    {
        StopCoroutine("ProcessQuiz");
        m_nSelectIndex = nIndex;
        if (m_nType != -1) CUIsAPTPage2Manager.Instance.SetSelectIndex(m_nIndex, nIndex);
        UpdateSelect();

        if ( CUIsAPTPage2Manager.Instance.IsTutorial() )
        {
            CUIsAPTPage2Manager.Instance.ShowTutorialMsg();
            CUIsAPTPage2Manager.Instance.SetTutorialWait(true);
            return;
        }

        Debug.Log("APT OnClick : " + CUIsAPTPage2Manager.Instance.GetQuizIndex() + ", " + CUIsAPTPage2Manager.Instance.GetAnswerIndex(nIndex));

        // TODO 활동로그 남기기
        CSpaceAppEngine.Instance.SetPage("APT");
        Server.Instance.RequestPostAnswerUpdateTime(CUIsAPTPage2Manager.Instance.GetQuizIndex(), CUIsAPTPage2Manager.Instance.GetRemainTime());

        // TODO 로그 확장
        //Server.Instance.RequestPUTAnswerObject(CUIsAPTPage2Manager.Instance.GetQuizIndex(), CUIsAPTPage2Manager.Instance.GetAnswerIndex(nIndex));
        Server.Instance.RequestPUTAnswerObject(CUIsAPTPage2Manager.Instance.GetQuizIndex(), CUIsAPTPage2Manager.Instance.GetAnswerIndex(nIndex), CUIsAPTPage2Manager.Instance.GetRealQstIndex());



        Debug.Log("OnClickAnswer " + m_nIndex + ", " + m_nQuizListIndex);
        CUIsAPTManager.Instance.SetAnswerState(m_nIndex, 0);
        //CUIsAPTManager.Instance.SetAnswerState(m_nQuizListIndex, 0);
        CUIsAPTPage2Manager.Instance.UpdateQuizList(m_nQuizListIndex);

        CUIsAPTPage2Manager.Instance.UpdateFinishAnswer();

        if (m_nType == 0)
        {
            Debug.Log("SendAnswer 01 : " + m_nQuizListIndex);
            if (m_nQuizListIndex >= 28) return;
        } else if (m_nType == 1)
        {
            Debug.Log("SendAnswer 02 : " + m_nQuizListIndex);
            if (m_nQuizListIndex >= 20) return;
        }
           
        Debug.Log("APT Quiz Index : " + m_nQuizListIndex);

        int nNextQuizIndex = m_nQuizListIndex + 1;
        //if( CUIsAPTManager.Instance.GetAnswerState(nNextQuizIndex - 1) == 2 )
        //    CUIsAPTManager.Instance.SetAnswerState(nNextQuizIndex - 1, 1);
        //CUIsAPTPage2Manager.Instance.UpdateQuizList(nNextQuizIndex + 1);
        //CUIsAPTPage2Manager.Instance.ShowQuiz(nNextQuizIndex);
        if (CUIsAPTManager.Instance.GetAnswerState(m_nIndex + 1) == 2)
            CUIsAPTManager.Instance.SetAnswerState(m_nIndex + 1, 1);
        CUIsAPTPage2Manager.Instance.UpdateQuizList(nNextQuizIndex);
        CUIsAPTPage2Manager.Instance.ShowQuiz(nNextQuizIndex);
    }

    public void UpdateSelect()
    {
        for (int i = 0; i < m_listSelect.Length; i++)
            m_listSelect[i].SetActive(false);

        if (m_nSelectIndex == -1)
            return;

        m_listSelect[m_nSelectIndex].SetActive(true);
    }
}
