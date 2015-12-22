using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pixelate : MonoBehaviour {

    public Color edgeColor;
    public float numTiles = 8.0f;
    public float threshhold = 1.0f;
    public bool MouseLock = true;

    [HideInInspector]
    public Material blurMat;
    public Shader pixelater;

    RenderTexture TarTex;

    // Use this for initialization
    void Start () {
        if (MouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        blurMat = new Material(pixelater);
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.mouseScrollDelta.y != 0)
            numTiles += Input.mouseScrollDelta.y;
        if (Input.GetKeyDown(KeyCode.Mouse0) && (Cursor.lockState != CursorLockMode.Locked|Cursor.visible == true) && MouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (Cursor.lockState != CursorLockMode.None | Cursor.visible != true) && MouseLock)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    void OnRenderImage (RenderTexture src, RenderTexture dest)
    {
        TarTex = RenderTexture.GetTemporary(Screen.currentResolution.width, Screen.currentResolution.height, 16);
        TarTex.filterMode = FilterMode.Point;
        blurMat.SetTexture("_MainTex", TarTex);
        blurMat.SetColor("_EdgeColor", edgeColor);
        blurMat.SetFloat("_NumTiles", numTiles);
        blurMat.SetFloat("_Threshhold", threshhold);
        Graphics.Blit(src, TarTex, blurMat);
        Graphics.Blit(TarTex, dest);
        RenderTexture.ReleaseTemporary(TarTex);

        //Graphics.Blit(TarTex, blurMat);
    }
}
