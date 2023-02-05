using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsCommonPopupFinish : MonoBehaviour
{
    private int m_nPartIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCommonPopupFinish(int nPartIdex)
    {
        m_nPartIndex = nPartIdex;
    }

    public void OnClickOK()
    {
        //Server.Instance.RequestPUTQuestionsStatus(m_nPartIndex, 1);
        CUIsSpaceManager.Instance.ScreenActive(false, true);
        CUIsSpaceManager.Instance.HideCommonPopupsFinish();
    }
}
