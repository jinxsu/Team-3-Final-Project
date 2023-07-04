using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating_Object : MonoBehaviour
{
    Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(initPos.x, initPos.y + Mathf.Sin(Time.time) * 3, initPos.z);
    }
}
