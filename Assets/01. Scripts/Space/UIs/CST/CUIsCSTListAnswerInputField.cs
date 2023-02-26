using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class CUIsCSTListAnswerInputField : MonoBehaviour, ISelectHandler
{
    public GameObject m_goSelectTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        //m_goSelectTarget.GetComponent<CUIsCSTListAnswer>().OnSelectAnswer();
        m_goSelectTarget.GetComponent<CUIsCSTListAnswerTmp>().OnSelectAnswer();
    }
}
