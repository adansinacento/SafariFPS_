using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public int FileCounter = 0;
    public int PhotosLeft = 15;

    private void Awake()
    {
        FileCounter = PlayerPrefs.HasKey("photos") ? PlayerPrefs.GetInt("photos") : 0;
    }

    public bool CamCapture(Camera _Cam)
    {
        if (PhotosLeft < 1) return false;

        Camera Cam = GetComponent<Camera>();
        Cam.fieldOfView = _Cam.fieldOfView;
        Cam.transform.rotation = _Cam.transform.rotation;

        RenderTexture currentRT = RenderTexture.active;
        RenderTexture.active = Cam.targetTexture;

        Cam.Render();

        Texture2D Image = new Texture2D(Cam.targetTexture.width, Cam.targetTexture.height);
        Image.ReadPixels(new Rect(0, 0, Cam.targetTexture.width, Cam.targetTexture.height), 0, 0);
        Image.Apply();
        RenderTexture.active = currentRT;

        var Bytes = Image.EncodeToPNG();
        Destroy(Image);

        File.WriteAllBytes(Application.dataPath + "/Backgrounds/" + FileCounter + ".png", Bytes);
        FileCounter++;

        PlayerPrefs.SetInt("photos", FileCounter);
        PlayerPrefs.Save();

        PhotosLeft--;

        return true;
    }
}
