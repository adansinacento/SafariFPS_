using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    [SerializeField]
    Image filter, target, indicator, imgBG;
    [SerializeField]
    Text PhotosLeft_Text;
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
        if (StaticManager.player == null)
            StaticManager.player = this;

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

        PhotosLeft_Text.text = m_PhotograficCam.PhotosLeft.ToString();


    }

    internal void Damage()
    {
        m_PhotograficCam.Ammo(-5);
        PhotosLeft_Text.text = m_PhotograficCam.PhotosLeft.ToString();
    }

    public void Pause(bool isPaused)
    {
        fpc.SetCursor(isPaused);
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
                    if (StaticManager.gameManager.IsPaused) return;
                    if (m_PhotograficCam.CamCapture(cam)) // si sí hay rollo
                    {
                        StartCoroutine("FlashAnimation", hit);
                    } 
                    else
                        StartCoroutine("NoRollLeftAnimation");

                    PhotosLeft_Text.text = m_PhotograficCam.PhotosLeft.ToString();
                }
            }
        }
    }

    IEnumerator NoRollLeftAnimation()
    {
        imgBG.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        imgBG.color = Color.white;
    }

    IEnumerator FlashAnimation(RaycastHit p)
    {
        indicator.enabled = true;

        if (p.collider.gameObject.CompareTag("Pokemon"))
        {
            m_PhotograficCam.Ammo(Random.Range(3, 11)); //Si destruyes a uno, reload

            //que ridicula es esta llamada
            StaticManager.spawner.NotifyKill(p.collider.gameObject.transform.parent.gameObject.GetComponent<Pokemon>().PokemonName);
            Destroy(p.collider.gameObject.transform.parent.gameObject);
        }

        yield return new WaitForSeconds(0.1f);
        indicator.enabled = false;

        
    }
}
