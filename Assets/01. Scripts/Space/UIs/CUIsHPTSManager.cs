using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsHPTSManager : MonoBehaviour
{
    public GameObject m_goTutorialMsg;

    public Text m_txtBtnSendAnswer;
    public Text m_txtRemainTime;

    public GameObject m_goContents;

    private int m_nTutorialStep = 0;
    private int m_nRemainTime = 0;

    private int m_nQuizIndex = 0;

    public GameObject m_goPopupSendAnswer;
    public Text m_txtSendAnswerRemainTime;

    public GameObject m_goPopupTimeover;

    public GameObject m_goPopupToLobby;
    public Text m_txtToLobbyRemainTime;

    public GameObject m_goPopupToLobbyOver;

    // Start is called before the first frame update
    void Start()
    {
        InitHPTSPage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitHPTSPage()
    {
        HideTutorialMsg();

        DelListAnswers();

        if (CUIsSpaceScreenLeft.Instance.IsHPTSTutorial())
        {
            m_txtBtnSendAnswer.text = "�� ���� �����ϱ�";
            m_txtRemainTime.text = "������";

            GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
            goQuiz.transform.parent = m_goContents.transform;
            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz("��� ����� ���� �ϴ� ����� �׷��� ���� �������", "", "", "", "");

            goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
            goQuiz.transform.parent = m_goContents.transform;
            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz("������ ������", "����", "����", "", "���̴�. ������ ���� ���ݷ��� �Դ´ٰ� ������ ��,");

            goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
            goQuiz.transform.parent = m_goContents.transform;
            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz("���ݷ��� �����ϴ� ������ ������ �翡 ������", "�� ���̴�", "���� ���� ���̴�", "", ". �ſ� ���� �����");

            goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
            goQuiz.transform.parent = m_goContents.transform;
            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz("�ſ� ��� ����� ������ ��ġ�� ������ ��, �� ����� ������ ��", "�ִ�", "����", "", "");
        }
        else
        {
            Quiz quizData = CQuizData.Instance.GetQuiz("HPTS");
            if (m_nQuizIndex == 0)
            {
                m_txtBtnSendAnswer.text = "�������� (1/2)";
                m_nRemainTime = quizData.exm_time;
                StartCoroutine("ProcessPlayExam");

                for(int i = 0; i < quizData.sets[0].questions.Length; i++)
                {
                    string[] listQuiz = quizData.sets[0].questions[i].qst_cnnt.Split('\n');
                    if( listQuiz.Length > 1 )
                    {
                        for(int j = 0; j < listQuiz.Length; j++)
                        {
                            GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                            goQuiz.transform.parent = m_goContents.transform;

                            if ( listQuiz[j].Contains("{{answers}}") )
                            {
                                string[] listAnswer = listQuiz[j].Split("{{answers}}");
                                string[] listSelector = new string[3];
                                for (int k = 0; k < listSelector.Length; k++)
                                {
                                    listSelector[k] = "";
                                }
                                for (int k = 0; k < quizData.sets[0].questions[i].answers.Length; k++)
                                {
                                    listSelector[k] = quizData.sets[0].questions[i].answers[k].anwr_cnnt;
                                }

                                if ( listAnswer.Length > 1 )
                                {
                                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                                } else
                                {
                                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listAnswer[0], "", "", "", "");
                                }
                            }
                            else
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listQuiz[j], "", "", "", "");
                            }
                        }
                    } else
                    {
                        GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                        goQuiz.transform.parent = m_goContents.transform;

                        if (listQuiz[0].Contains("{{answers}}"))
                        {
                            string[] listAnswer = listQuiz[0].Split("{{answers}}");
                            string[] listSelector = new string[3];
                            for (int k = 0; k < listSelector.Length; k++)
                            {
                                listSelector[k] = "";
                            }
                            for (int k = 0; k < quizData.sets[0].questions[i].answers.Length; k++)
                            {
                                listSelector[k] = quizData.sets[0].questions[i].answers[k].anwr_cnnt;
                            }

                            if (listAnswer.Length > 1)
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                            }
                            else
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listAnswer[0], "", "", "", "");
                            }
                        }
                        else
                        {
                            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listQuiz[0], "", "", "", "");
                        }
                    }
                }
            }
            else
            {
                m_txtBtnSendAnswer.text = "�亯 �����ϱ�";
                for (int i = 0; i < quizData.sets[1].questions.Length; i++)
                {
                    string[] listQuiz = quizData.sets[0].questions[i].qst_cnnt.Split('\n');
                    if (listQuiz.Length > 1)
                    {
                        for (int j = 0; j < listQuiz.Length; j++)
                        {
                            GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                            goQuiz.transform.parent = m_goContents.transform;

                            if (listQuiz[j].Contains("{{answers}}"))
                            {
                                string[] listAnswer = listQuiz[j].Split("{{answers}}");
                                string[] listSelector = new string[3];
                                for (int k = 0; k < listSelector.Length; k++)
                                {
                                    listSelector[k] = "";
                                }
                                for (int k = 0; k < quizData.sets[0].questions[i].answers.Length; k++)
                                {
                                    listSelector[k] = quizData.sets[0].questions[i].answers[k].anwr_cnnt;
                                }

                                if (listAnswer.Length > 1)
                                {
                                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                                }
                                else
                                {
                                    goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listAnswer[0], "", "", "", "");
                                }
                            }
                            else
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listQuiz[j], "", "", "", "");
                            }
                        }
                    }
                    else
                    {
                        GameObject goQuiz = Instantiate(Resources.Load("Prefabs/quizHPTS") as GameObject);
                        goQuiz.transform.parent = m_goContents.transform;

                        if (listQuiz[0].Contains("{{answers}}"))
                        {
                            string[] listAnswer = listQuiz[0].Split("{{answers}}");
                            string[] listSelector = new string[3];
                            for (int k = 0; k < listSelector.Length; k++)
                            {
                                listSelector[k] = "";
                            }
                            for (int k = 0; k < quizData.sets[0].questions[i].answers.Length; k++)
                            {
                                listSelector[k] = quizData.sets[0].questions[i].answers[k].anwr_cnnt;
                            }

                            if (listAnswer.Length > 1)
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listAnswer[0], listSelector[0], listSelector[1], listSelector[2], listAnswer[1]);
                            }
                            else
                            {
                                goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listAnswer[0], "", "", "", "");
                            }
                        }
                        else
                        {
                            goQuiz.GetComponent<CUIsHPTSQuiz>().InitHPTSQuiz(listQuiz[0], "", "", "", "");
                        }
                    }
                }
            }
        }
    }

    public void DelListAnswers()
    {
        Component[] listChilds = m_goContents.GetComponentsInChildren<Component>();

        foreach (Component iter in listChilds)
        {
            if (iter.transform != m_goContents.transform)
            {
                Destroy(iter.gameObject);
            }
        }

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

    public void OnClickExit()
    {
        ShowPopupToLobby();
    }

    public void ShowTutorialMsg()
    {
        m_goTutorialMsg.SetActive(true);
    }

    public void HideTutorialMsg()
    {
        m_goTutorialMsg.SetActive(false);
    }

    public void OnClickSendAnswer()
    {
        if (CUIsSpaceScreenLeft.Instance.IsHPTSTutorial())
        {
            if (m_nTutorialStep == 0)
            {
                m_nTutorialStep++;
                ShowTutorialMsg();
            }
            else
            {
                CUIsSpaceScreenLeft.Instance.SetHPTSTutorial(false);
                InitHPTSPage();
            }
            return;
        }

        if (m_nQuizIndex < 1)
        {
            m_nQuizIndex++;
            InitHPTSPage();
            return;
        }

        ShowPopupSendAnswer();
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
        StopCoroutine("ProcessPlayExam");
        HideAllPopup();
        CUIsSpaceScreenLeft.Instance.HideRightAllPage();
        CUIsSpaceManager.Instance.ScreenActive(false);
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
