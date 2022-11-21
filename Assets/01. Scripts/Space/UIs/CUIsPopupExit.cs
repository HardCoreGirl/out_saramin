using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsPopupExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickFinish()
    {
        CUIsSpaceScreenLeft.Instance.HideAllPopup();
        CUIsSpaceManager.Instance.HideLeftPage();
    }

    public void OnClickClose()
    {
        Debug.Log("OnClickClose");
        CUIsSpaceScreenLeft.Instance.HidePopupFinish();
    }
}
