using UnityEngine;

/// <summary>
/// Este script es altamente CPU Demantante
/// No vale la pena usarlo con muchas colisiones simultaneas
/// Quizá tenga otros usos donde hayan menos colisiones al mismo tiempo
/// </summary>

public class WaterExpPaintable : MonoBehaviour
{
    const int textureSize = 1024;
    public int maskTextureID = Shader.PropertyToID("_WaterMask");
    public Renderer renderComponent;
    public Texture2D maskTexture;
    void Start()
    {
        renderComponent = GetComponent<Renderer>();
        CreateMask();
    }

    private void CreateMask()
    {
        Color[] colors = new Color[textureSize * textureSize];
        for (int i = 0; i < colors.Length; i++)
        {
            _ = colors[i] == Color.clear;
        }
        maskTexture = new Texture2D(textureSize, textureSize, UnityEngine.Experimental.Rendering.DefaultFormat.HDR, 0);
        maskTexture.SetPixels(0,0,textureSize,textureSize, colors);
        renderComponent.material.SetTexture(maskTextureID, maskTexture);
    }

    public void Paint(Vector3 position, Vector3 direction, Texture2D splash, Color ? color = null)
    {
        if (Physics.Raycast(position, direction, out RaycastHit hit))
        {
            Vector2 textureCoord = hit.textureCoord;

            int pixelX = (int)(textureCoord.x * maskTexture.width);
            int pixelY = (int)(textureCoord.y * maskTexture.height);
            for (int x = 0; x < splash.width; x++)
            {
                for (int y = 0; y < splash.height; y++)
                {
                    Color splashColor = splash.GetPixel(x, y);
                    Color maskColor = maskTexture.GetPixel(pixelX + x, pixelY + y);
                    maskTexture.SetPixel(pixelX + x,
                        pixelY + y,
                        new Color(0, maskColor.a * splashColor.a, 0));
                }
            }
            maskTexture.Apply();
        }
    }
}
