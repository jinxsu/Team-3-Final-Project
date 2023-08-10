using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class StaticEffectScript : MonoBehaviour
{
    public bool isStatic;
    public Camera cam;
    public UnityEngine.Rendering.Universal.UniversalAdditionalCameraData additionalCameraData;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isStatic)
        {
            additionalCameraData.SetRenderer(1);
        }
        else
        {
            additionalCameraData.SetRenderer(0);
        }
    }

}
