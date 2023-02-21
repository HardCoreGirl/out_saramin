using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using UnityEngine.EventSystems;

public class CUIsAPTBtnListManager : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Text[] m_listTxtMsg;
    private Color m_clrBase;
    // Start is called before the first frame update
    void Start()
    {
        //m_listTxtMsg = gameObject.GetComponentsInChildren<Text>();
        //m_clrBase = m_listTxtMsg[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("OnPointerEnter");
        //for (int i = 0; i < m_listTxtMsg.Length; i++)
        //    m_listTxtMsg[i].color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("OnPointerExit");
        //for (int i = 0; i < m_listTxtMsg.Length; i++)
        //    m_listTxtMsg[i].color = m_clrBase;
            
    }

    public void OnSelect(BaseEventData eventData)
    {
        //Debug.Log(this.gameObject.name + "was selected");
        //Debug.Log(eventData.ToString());
    }
}
