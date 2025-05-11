using UnityEngine;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    [SerializeField] private Camera photoCamera;
    private int width = 1080;
    private int height = 1920;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            TakePhoto();
        }
    }

    private void TakePhoto()
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        photoCamera.targetTexture = rt;

        Texture2D photo = new Texture2D(width, height, TextureFormat.RGB24, false);

        photoCamera.Render();
        RenderTexture.active = rt;
        photo.ReadPixels(new Rect(0,0,width,height), 0, 0);
        photo.Apply();

        photoCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = photo.EncodeToPNG();
        string path = Path.Combine(Application.dataPath, "CapturePhoto.png");
        File.WriteAllBytes(path, bytes);

        Debug.Log("写真を保存しました：" + path);
    }
}
