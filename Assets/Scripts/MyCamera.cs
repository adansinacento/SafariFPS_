using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    public int FileCounter = 0;
    public int PhotosLeft = 15;
    string path;


    private void Awake()
    {
        if (StaticManager.myCamera == null)
            StaticManager.myCamera = this;

        path = Application.dataPath + "/Backgrounds/";
        FileCounter = PlayerPrefs.HasKey("photos") ? PlayerPrefs.GetInt("photos") : 0;
        
        // si no existe el directorio lo creamos
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
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

        File.WriteAllBytes(path + FileCounter + ".png", Bytes);
        FileCounter++;

        PlayerPrefs.SetInt("photos", FileCounter);
        PlayerPrefs.Save();

        PhotosLeft--;

        return true;
    }

    public void Ammo(int n)
    {
        PhotosLeft += n;

        if (PhotosLeft >= 100)
            PhotosLeft = 100;

        if (PhotosLeft <= 0)
            PhotosLeft = 0;
    }

    public List<FileInfo> PhotoCount()
    {
        DirectoryInfo d = new DirectoryInfo(path);
        // Add file sizes.
        FileInfo[] fis = d.GetFiles();
        List<FileInfo> r = new List<FileInfo>();
        foreach (FileInfo fi in fis)
        {
            if (fi.Extension.Contains("png"))
                r.Add(fi);
        }
        return r;
    }

    public byte[] GetImage(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);
        return bytes;
    }
}
