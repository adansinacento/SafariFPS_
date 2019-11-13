using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    Image filter;
    Camera cam;
    [SerializeField]
    float fov_og, fov_zoomed;

    void Start()
    {
        if (cam == null)
        {
            cam = GetComponentInChildren<Camera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            filter.enabled = !filter.enabled;
            cam.fieldOfView = filter.enabled ? fov_zoomed : fov_og;
        }
    }
}
