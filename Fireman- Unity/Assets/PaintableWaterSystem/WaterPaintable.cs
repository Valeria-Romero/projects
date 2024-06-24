using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPaintables : MonoBehaviour
{
    const int textureSize = 1024;
    public RenderTexture maskRenderTexture;
    public Renderer renderComponent;
    public int maskTextureID = Shader.PropertyToID("_WaterMask");

    [SerializeField] private Texture2D brush;

    void Start()
    {
        maskRenderTexture = new RenderTexture(textureSize, textureSize, 32);
        renderComponent = GetComponent<Renderer>();
        renderComponent.material.SetTexture(maskTextureID, maskRenderTexture);

    }

    public void Paint()
    {

    }
}


//-------------------------------------
// simple ink paintスクリプト
// ColliderとRendererは必要
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class WaterPaintable : MonoBehaviour
{
    // マテリアル(外部からアタッチしているが内部から取ってももちろんOK)
    public Material simpleInkPaintMat;

    private Texture brushTexture; // ブラシテクスチャ
    private Texture mainTexture; // 元となるマテリアルで使用しているテクスチャ
    private RenderTexture renderTexture; // 作業用レンダーテクスチャ(ここに描かれる)

    // shaderのpropertyIDを保持しておく
    private int mainTexturePropertyID;
    private int paintUVPropertyID;
    private int brushTexturePropertyID;
    private int brushScalePropertyID;
    private int brushColorPropertyID;


    // Use this for initialization
    void Start()
    {

        // property idを設定
        mainTexturePropertyID = Shader.PropertyToID("_MainTex");
        paintUVPropertyID = Shader.PropertyToID("_PaintUV");
        brushTexturePropertyID = Shader.PropertyToID("_Brush");
        brushScalePropertyID = Shader.PropertyToID("_BrushScale");
        brushColorPropertyID = Shader.PropertyToID("_ControlColor");

        // マテリアルに設定されているメインテクスチャを取得する
        mainTexture = simpleInkPaintMat.GetTexture(mainTexturePropertyID);
        brushTexture = simpleInkPaintMat.GetTexture(brushTexturePropertyID);

        // renderTextureを生成
        renderTexture = new RenderTexture(mainTexture.width, mainTexture.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);

        // メインテクスチャをレンダーテクスチャにコピー
        Graphics.Blit(mainTexture, renderTexture);

        // マテリアルに設定されていたメインテクスチャをレンダーテクスチャに差し替える
        simpleInkPaintMat.SetTexture(mainTexturePropertyID, renderTexture);
    }

    // これは必須と思ってほしい
    void OnDestroy()
    {
        renderTexture.Release(); // render textureを開放する
        simpleInkPaintMat.SetTexture(mainTexturePropertyID, mainTexture); // 元のテクスチャに戻す
    }


    void Update()
    {

        // タッチした箇所のuvを取得する
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                // そこを塗る
                //Paint(hitInfo.textureCoord);
            }
        }
    }

    //versión mathew
    public void Painting(Vector3 position, Vector3 direction)
    {
        if (Physics.Raycast(position, direction, out RaycastHit hit))
        {
            Vector2 textureCoord = hit.textureCoord;
            Paint(textureCoord);
        }
    }


    /**
     * 実際に塗る
     */
    private void Paint(Vector2 uv)
    {
        // bufferを作成する
        var renderTextureBuffer = RenderTexture.GetTemporary(renderTexture.width, renderTexture.height);
        simpleInkPaintMat.SetVector(paintUVPropertyID, uv);
        simpleInkPaintMat.SetTexture(brushTexturePropertyID, brushTexture);
        simpleInkPaintMat.SetFloat(brushScalePropertyID, 0.05f);
        simpleInkPaintMat.SetVector(brushColorPropertyID, new Vector4(0, 128, 255, 128));

        Graphics.Blit(renderTexture, renderTextureBuffer, simpleInkPaintMat); // マテリアルを適用したレンダーテクスチャをbufferにコピー // 直接は書き込めない
        Graphics.Blit(renderTextureBuffer, renderTexture); // bufferからレンダーテクスチャに書き戻す
        RenderTexture.ReleaseTemporary(renderTextureBuffer); // bufferを開放
    }
}
