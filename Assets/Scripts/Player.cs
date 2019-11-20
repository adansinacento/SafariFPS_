using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    [SerializeField]
    Image filter, target;
    Camera cam;
    [SerializeField]
    float fov_og, fov_zoomed;
    public bool IsZoomed = false;
    [SerializeField]
    FirstPersonController fpc;
    [SerializeField]
    MyCamera m_PhotograficCam;
    

    void Start()
    {
        if (cam == null)
        {
            cam = GetComponentsInChildren<Camera>()[0];
        }

        if (fpc == null)
        {
            fpc = GetComponent<FirstPersonController>();
        }

        if (m_PhotograficCam == null)
        {
            m_PhotograficCam = GetComponent<MyCamera>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsZoomed = !IsZoomed;
            filter.enabled = IsZoomed;
            target.enabled = IsZoomed;
            cam.fieldOfView = IsZoomed ? fov_zoomed : fov_og;
            fpc.AdjustMouseSensitivity(IsZoomed);
        }

        if (IsZoomed)
        {
            target.color = Color.white;
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit))
            {
                //pokemon centrado
                if (hit.collider.gameObject.CompareTag("Pokemon"))
                {
                    target.color = Color.red;
                }

                //foto
                if (Input.GetMouseButtonDown(0))
                {
                    m_PhotograficCam.CamCapture(cam);
                }
            }
        }
    }
}
