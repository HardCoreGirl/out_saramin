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

        CUIsSpaceScreenLeft.Instance.ShowQuiz(0, 0, CUIsSpaceScreenLeft.Instance.IsRQTTutorial());

    }
}
