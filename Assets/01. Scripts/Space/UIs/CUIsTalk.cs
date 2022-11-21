using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUIsTalk : MonoBehaviour
{
    public int m_nQuizIndex;

    public GameObject[] m_listSelect = new GameObject[2];

    public GameObject[] m_listSelected = new GameObject[4];

    public GameObject m_goBtnReset;

    private int m_nSelectIndex;

    // Start is called before the first frame update
    void Start()
    {
        InitUIs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitUIs()
    {
        ShowSelect(0);
    }

    public void OnClickSelector(int nIndex)
    {
        m_nSelectIndex = nIndex;
        ShowSelect(1);
        ShowSelected(m_nSelectIndex);

        CUIsSpaceScreenLeft.Instance.ShowQuiz(m_nQuizIndex + 1);

        if (m_nSelectIndex == 1 || m_nSelectIndex == 2)
        {
            m_goBtnReset.transform.localPosition = new Vector3(445, 0, 0);
        }
        else
        {
            m_goBtnReset.transform.localPosition = new Vector3(410, 0, 0);
        }
    }

    public void OnClickReset()
    {
        ShowSelect(0);
    }

    public void HideAllSelect()
    {
        for (int i = 0; i < m_listSelect.Length; i++)
        {
            HideSelect(i);
        }
    }

    public void ShowSelect(int nIndex)
    {
        HideAllSelect();
        m_listSelect[nIndex].SetActive(true);
    }

    public void HideSelect(int nIndex)
    {
        m_listSelect[nIndex].SetActive(false);
    }

    public void HideAllSelected()
    {
        for (int i = 0; i < m_listSelected.Length; i++)
        {
            HideSelected(i);
        }
    }

    public void ShowSelected(int nIndex)
    {
        HideAllSelected();
        m_listSelected[nIndex].SetActive(true);
    }

    public void HideSelected(int nIndex)
    {
        m_listSelected[nIndex].SetActive(false);
    }
}
