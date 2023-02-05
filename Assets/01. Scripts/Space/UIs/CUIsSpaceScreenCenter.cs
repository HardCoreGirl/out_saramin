using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsSpaceScreenCenter : MonoBehaviour
{
    public GameObject m_goMain;
    public GameObject m_goDetail;

    public GameObject m_goLGTKMain;
    public GameObject m_goLGTKTalkBox;

    // Start is called before the first frame update
    void Start()
    {
        HideDetail();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTKManager()
    {
        m_goLGTKMain.SetActive(true);
        m_goLGTKMain.GetComponent<CUIsLGTKManager>().InitLGTK();
    }

    public void OnClickExit()
    {
        CUIsSpaceManager.Instance.HideAllPage();
    }

    public void OnClickDetail()
    {
        ShowDetail();
    }

    public void OnClickExitDetail()
    {
        HideDetail();
    }

    public void ShowDetail()
    {
        m_goDetail.SetActive(true);
    }

    public void HideDetail()
    {
        m_goDetail.SetActive(false);
    }

}
