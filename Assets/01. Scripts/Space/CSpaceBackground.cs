using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpaceBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ProcessBackground");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ProcessBackground()
    {
        float fTime;

        Vector3 vecOriPoz = transform.localPosition;

        while(true)
        {
            vecOriPoz.x -= (Time.deltaTime * 0.1f);

            transform.localPosition = vecOriPoz;
            yield return new WaitForEndOfFrame();
        }
    }
}
