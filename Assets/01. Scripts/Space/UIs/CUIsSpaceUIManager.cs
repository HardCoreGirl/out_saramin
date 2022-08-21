using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsSpaceUIManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsSpaceUIManager _instance = null;

    public static CUIsSpaceUIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsSpaceUIManager install null");

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

    public GameObject[] m_listScreen = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        HideAllScreen();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScreen(int nIndex)
    {
        HideAllScreen();
        m_listScreen[nIndex].gameObject.SetActive(true);
    }

    public void HideAllScreen()
    {
        for(int i = 0; i < m_listScreen.Length; i++)
        {
            m_listScreen[i].gameObject.SetActive(false);
        }
    }
}
