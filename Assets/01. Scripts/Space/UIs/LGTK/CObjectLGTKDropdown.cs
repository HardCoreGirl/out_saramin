using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CObjectLGTKDropdown : MonoBehaviour
{
    public Text m_txtName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitLGTKDropdown(string strName)
    {
        m_txtName.text = strName;
    }
}
