using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	void OnTriggerEnter(Collider other)
    {
        if(other.name == "RigidbodyPlayer")
        {
            StartCoroutine(Trapped());
        }
    }

    IEnumerator Trapped()
    {
        GameObject fadeSphere = GameObject.Find("FadeSphere");
        Renderer fadeRend = (Renderer)fadeSphere.GetComponent(typeof(Renderer));
        AudioSource audioSphere = (AudioSource)fadeSphere.GetComponent(typeof(AudioSource));
        GameObject player = GameObject.Find("RigidbodyPlayer");
        RigifbodyWalker playerController = (RigifbodyWalker)player.GetComponent(typeof(RigifbodyWalker));
        playerController.frozen = true;
        audioSphere.Play();
        fadeRend.material.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(5);
        Application.LoadLevel(1);
    }
}
