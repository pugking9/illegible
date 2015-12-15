using UnityEngine;
using System.Collections;

public class Pixelate : MonoBehaviour {

    public Color edgeColor;
    public float numTiles = 8.0f;
    public float threshhold = 1.0f;
    public Camera renderTexCam;

    [HideInInspector]
    public Material blurMat;
    public Shader pixelater;

    RenderTexture TarTex;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        TarTex = new RenderTexture(Screen.currentResolution.width, Screen.currentResolution.height, 16);

        blurMat = new Material(pixelater);

        renderTexCam.targetTexture = TarTex;
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        if (Input.mouseScrollDelta.y != 0)
            numTiles += Input.mouseScrollDelta.y;
        if (Input.GetKeyDown(KeyCode.Mouse0) && (Cursor.lockState != CursorLockMode.Locked|Cursor.visible == true))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && (Cursor.lockState != CursorLockMode.None | Cursor.visible != true))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    void OnPostRender ()
    {
        blurMat.SetTexture("_MainTex", TarTex);
        blurMat.SetColor("_EdgeColor", edgeColor);
        blurMat.SetFloat("_NumTiles", numTiles);
        blurMat.SetFloat("_Threshhold", threshhold);
        Graphics.Blit(TarTex, blurMat);
    }
}
