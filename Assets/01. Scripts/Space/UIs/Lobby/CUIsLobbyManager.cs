using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsLobbyManager : MonoBehaviour
{
    #region SingleTon
    public static CUIsLobbyManager _instance = null;

    public static CUIsLobbyManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("CUIsLobbyManager install null");

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

    public GameObject[] m_listComputer = new GameObject[3];
    public GameObject[] m_listComOutline = new GameObject[3];

    private int m_nPlayIntroOutlineIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideIntroOutlineAll()
    {
        StopCoroutine("ProcessIntroOutline");
        for (int i = 0; i < m_listComputer.Length; i++)
        {
            m_listComputer[i].SetActive(false);
        }
    }

    public void PlayIntroOutline(int nIndex)
    {
        StopCoroutine("ProcessIntroOutline");

        m_nPlayIntroOutlineIndex = nIndex;
        
        for (int i = 0; i < m_listComputer.Length; i++)
        {
            if(i == m_nPlayIntroOutlineIndex)
                m_listComputer[i].SetActive(true);
            else
                m_listComputer[i].SetActive(false);
        }

        StartCoroutine("ProcessIntroOutline");
    }

    IEnumerator ProcessIntroOutline()
    {
        while(true)
        {
            m_listComOutline[m_nPlayIntroOutlineIndex].SetActive(true);
            yield return new WaitForSeconds(0.3f);
            m_listComOutline[m_nPlayIntroOutlineIndex].SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
