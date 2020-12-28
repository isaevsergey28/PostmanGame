using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class СameraController : MonoBehaviour
{
    private Vector3 _offset;
    public GameObject obj;
    void Start()
    {
        _offset = obj.transform.position - transform.position;
    }

    void LateUpdate()   // плавное движение камеры за целью
    {
        Vector3 curPos = transform.position;
        curPos = new Vector3(obj.transform.position.x - _offset.x, obj.transform.position.y - _offset.y, curPos.z);
        curPos.z = Mathf.Lerp(curPos.z, obj.transform.position.z, Time.fixedDeltaTime * 2);
        transform.position = curPos;
    }
}
