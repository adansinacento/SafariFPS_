using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject PausePanel, GalleryGO, PhotoPrefab;
    
    public bool IsPaused;

    void Start()
    {
        if (StaticManager.gameManager == null)
            StaticManager.gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            IsPaused = !IsPaused;
            PausePanel.SetActive(IsPaused);
            Time.timeScale = IsPaused ? 0 : 1;
            StaticManager.player.Pause(IsPaused);

            if (IsPaused) //Load all images
            {
                var imageInfo = StaticManager.myCamera.PhotoCount();
                for (int i = 0; i < imageInfo.Count; i++)
                {
                    var bytes = StaticManager.myCamera.GetImage(imageInfo[i].FullName);
                    var img = Instantiate(PhotoPrefab, GalleryGO.transform);
                    DisplayImage(img, bytes);
                }
            }
            else //destroy all images
            {
                for (int i = GalleryGO.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(GalleryGO.transform.GetChild(i).gameObject);
                }
            }
        }
    }

    void DisplayImage(GameObject imgGo, byte[] content)
    {
        var img = imgGo.GetComponent<UnityEngine.UI.Image>();
        int width = 1024, height = 768;

        Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        texture.filterMode = FilterMode.Trilinear;
        texture.LoadImage(content);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.0f), 1.0f);

        img.sprite = sprite;
    }
}
