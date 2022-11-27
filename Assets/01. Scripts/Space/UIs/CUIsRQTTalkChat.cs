using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsRQTTalkChat : MonoBehaviour
{
    public Text m_txtDisc;

    private int m_nIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitObject(int nIndex, string strDisc)
    {
        m_nIndex = nIndex;
        m_txtDisc.text = strDisc;
    }

    public void OnClickTalk()
    {
        Debug.Log("OnClickTalk : " + m_nIndex);
    }
}
