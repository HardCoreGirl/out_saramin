using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CObjectLGTKDropdown : MonoBehaviour
{
    public Text m_txtName;
    public Text m_txtSeurGrd;
    public Text m_txtRegDtm;

    public GameObject m_goContents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTKDropdown(string strName, string strSeurGrd, string strRegDtm)
    {
        m_txtName.text = strName; 
        m_txtSeurGrd.text = strSeurGrd;
        m_txtRegDtm.text = strRegDtm;
    }

    public void OnChangeValue()
    {
        Debug.Log("OnChangeValue");
    }
}
