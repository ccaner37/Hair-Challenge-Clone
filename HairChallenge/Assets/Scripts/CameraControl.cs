using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target;
    public Transform camTransform;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Offset = camTransform.position - target.position;
    }

    void LateUpdate()
    {
        if (GameManager.Instance.endGame)
        {
            transform.position = new Vector3(3.06f, 1.05f, 95.9f);
            transform.eulerAngles = new Vector3(29.55f, -161f, 0f);
        }
        else
        {
            Vector3 targetPosition = target.position + Offset;
            camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);
        }

    }
}
