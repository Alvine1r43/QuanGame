using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = Camera.main.WorldToScreenPoint(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) //按住鼠标左键
        {
            Vector3 m_MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
            transform.position = Camera.main.ScreenToWorldPoint(m_MousePos);
        }
    }
}
