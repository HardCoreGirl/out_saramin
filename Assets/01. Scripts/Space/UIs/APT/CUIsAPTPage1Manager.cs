using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsAPTPage1Manager : MonoBehaviour
{
    public Text m_txtAPTD1Cnt;
    public Text m_txtAPTD1Time;
    public Text m_txtAPTD2Cnt;
    public Text m_txtAPTD2Time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitAPTPage()
    {
        Quiz quizAPT = CQuizData.Instance.GetQuiz("APTD1");
        m_txtAPTD1Cnt.text = quizAPT.sets.Length.ToString() + " 문항";
        int nMin = quizAPT.exm_time / 60;
        m_txtAPTD1Time.text = nMin.ToString() + " 분";

        quizAPT = CQuizData.Instance.GetQuiz("APTD2");
        m_txtAPTD2Cnt.text = quizAPT.sets.Length.ToString() + " 문항";
        nMin = quizAPT.exm_time / 60;
        m_txtAPTD2Time.text = nMin.ToString() + " 분";
    }

    public void OnClickPlayQuiz()
    {
        CUIsAPTManager.Instance.ShowAPTPage(1);
    }

    public void OnClickExit()
    {
        CUIsSpaceManager.Instance.ScreenActive(false);

        gameObject.SetActive(false);
    }
}
