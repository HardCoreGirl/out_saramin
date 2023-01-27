using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CObjectAPTQuizList : MonoBehaviour
{
    public Text m_txtQuizName;
    public Text m_txtState;

    private int m_nIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAPTQuizList(int nIndex, string strQuizName, string strState)
    {
        m_nIndex = nIndex;
        m_txtQuizName.text = strQuizName; 
        m_txtState.text = strState;
    }

    public void OnClickQuiz()
    {
        Debug.Log("OnClick : " + m_nIndex);
    }
}
