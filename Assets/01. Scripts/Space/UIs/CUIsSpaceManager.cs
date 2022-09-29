using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsSpaceManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsSpaceManager _instance = null;

    public static CUIsSpaceManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsSpaceManager install null");

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

    public GameObject m_goLeftPage;

    // Start is called before the first frame update
    void Start()
    {
        HideAllPage();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllPage()
    {
        HideLeftPage();
    }

    public void ShowLeftPage()
    {
        m_goLeftPage.SetActive(true);
    }

    public void HideLeftPage()
    {
        m_goLeftPage.SetActive(false);
    }
}
