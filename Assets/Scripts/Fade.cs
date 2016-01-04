using UnityEngine;
using System.Collections;
using System.Linq;

public class Fade : MonoBehaviour {

    Material fadeMat;
    bool fadeIn = false;

	// Use this for initialization
	void Start () {
        fadeMat = GetComponent<Renderer>().material;
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

    public void FadeIn()
    {
        fadeIn = true;
        if (fadeMat.color.a > 0)
        {
            fadeMat.color = new Color(fadeMat.color.r, fadeMat.color.g, fadeMat.color.b, fadeMat.color.a - 0.005f);
        }
        else
        {
            fadeIn = false;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (fadeIn)
        {
            FadeIn();
        }
	}
}
