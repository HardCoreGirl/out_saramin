using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsPopupTimeover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickClose()
    {
        CUIsSpaceScreenLeft.Instance.HideAllPopup();
        CUIsSpaceManager.Instance.HideLeftPage();
    }
}
