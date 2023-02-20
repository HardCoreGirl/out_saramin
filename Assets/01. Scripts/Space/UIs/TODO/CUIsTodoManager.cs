using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CUIsTodoManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsTodoManager _instance = null;

    public static CUIsTodoManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log(" CUIsTodoManager install null");

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            _instance = null;
        }
    }
    #endregion

    public Text m_txtDummy;

    // Start is called before the first frame update
    void Start()
    {
        m_txtDummy.text = "111111\n22222\n33333";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDummyTodo()
    {
        string strMsg = "";
        for(int i = 0; i < CQuizData.Instance.GetInfoMission().body.Length; i++)
        {
            strMsg = strMsg + CQuizData.Instance.GetInfoMission().body[i].title + "\n" + CQuizData.Instance.GetInfoMission().body[i].content + "\n";
        }

        m_txtDummy.text = strMsg;   
    }
}
