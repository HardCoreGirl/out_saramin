using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsRQTTalkChat : MonoBehaviour
{
    public Text m_txtDisc;

    private int m_nIndex;

    private bool m_bIsTutorial;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitObject(int nIndex, string strDisc, bool bIsToturial = false)
    {
        m_nIndex = nIndex;
        m_txtDisc.text = strDisc;
        m_bIsTutorial = bIsToturial;
    }

    public void OnClickTalk()
    {
        Debug.Log("OnClickTalk : " + m_nIndex);
        if (CQuizData.Instance.GetQuiz("RQT").sets[0].questions[0].test_answers[0].test_anwr_idx != 0)
        {
            CUIsSpaceScreenLeft.Instance.SetRQTTutorial(false);

            int nLastQuizIndex = 0;
            for(int i = 0; i < CQuizData.Instance.GetQuiz("RQT").sets.Length; i++)
            {
                if(CQuizData.Instance.GetQuiz("RQT").sets[i].questions[0].test_answers[0].test_anwr_idx == 0)
                {
                    nLastQuizIndex = i;
                    break;
                }
            }

            CUIsSpaceScreenLeft.Instance.InitRQTQuiz(false);
            CUIsSpaceScreenLeft.Instance.ShowQuiz(0, nLastQuizIndex, CUIsSpaceScreenLeft.Instance.IsRQTTutorial());
        } else
        {
            CUIsSpaceScreenLeft.Instance.ShowQuiz(0, 0, CUIsSpaceScreenLeft.Instance.IsRQTTutorial());
        }
    }
}
