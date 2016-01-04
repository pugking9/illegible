using UnityEngine;
using System.Collections;
using System.IO;

public class GameController : MonoBehaviour
{

    public GameObject fadeSphere;
    public Fade fadeScript;

    public GameObject QuestionText;
    public WrapAround wrapScript;

    public GameObject PlayerCam;
    public GameObject Player;
    public RigifbodyWalker PlayerScript;

    public TextAsset Dictionary;

    Ray rayCast;
    RaycastHit rayHit;

    public AudioClip Wrong;

    bool sentanceEnd;
    bool bootedUp = false;

    public string[] strLines;

    public GameObject ScreenControl;
    public Light screenLight;
    public int Questions;
    public int Words;
    public string Sentance;
    public string tempSentance;

    void Start()
    {
        strLines = Dictionary.text.Split("\n"[0]);
        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        if(bootedUp)
        {
            rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(rayCast.origin, rayCast.direction * 3, Color.cyan);
            if (sentanceEnd & Input.GetMouseButtonDown(0) & Physics.Raycast(rayCast,out rayHit,3,1))
            {
                Debug.Log(rayHit.transform.name);
                if (rayHit.transform.name == "Answer1" | rayHit.transform.name == "Answer2")
                {
                    sentanceEnd = false;
                    StartCoroutine(Buzzer());
                }
            }
        }
    }

    IEnumerator Buzzer()
    {
        if (Random.Range(0, 2) == 1)
        {
            AudioSource screenAudio = (AudioSource)ScreenControl.GetComponent(typeof(AudioSource));
            screenAudio.clip = Wrong;
            screenAudio.Play();
            screenLight.color = Color.red;
            Renderer screenRend = (Renderer)ScreenControl.GetComponent(typeof(Renderer));
            screenRend.material.color = new Color(1, 0, 0, 0.686f);
            yield return new WaitForSeconds(1);
            screenLight.color = Color.cyan;
            screenRend.material.color = new Color(1, 1, 1, 0.686f);
        }
        printSentance();
    }

    public void ChangeToScene(string Level)
    {
        Application.LoadLevel(Level);
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            StartCoroutine(StartGame());
        }
    }

    void printSentance()
    {
        if (Questions >= 1)
        {
            Questions -= 1;
            Words = Random.Range(3, 5);
            Sentance = "";
            tempSentance = "";
            for (int I = 1; I <= Words; I++)
            {
                tempSentance += (strLines[Random.Range(0, strLines.Length)] + " ");
            }
            tempSentance = tempSentance.Trim();
            StartCoroutine(slowtypeSentance());
        }
        else
        {
            StartCoroutine(Shrink());
        }
    }

    IEnumerator Shrink()
    {
        while (ScreenControl.transform.localScale.x > 0)
        {
            yield return new WaitForSeconds(0.01f);
            ScreenControl.transform.localScale -= new Vector3(0.01f, 0, 0);
            screenLight.intensity -= 0.375f;
        }
        Destroy(ScreenControl);
        PlayerScript.frozen = false;
    }

    IEnumerator slowtypeSentance()
    {
        for (int I = 1; I <= tempSentance.Length; I++)
        {
            Sentance += tempSentance.Substring(I - 1, 1);
            wrapScript.UnwrappedText = Sentance;
            wrapScript.NeedsLayout = true;
            wrapScript.Start();
            yield return new WaitForSeconds(0.12f);
        }
        sentanceEnd = true;
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1);
        fadeSphere = GameObject.Find("FadeSphere");
        fadeScript = (Fade)fadeSphere.GetComponent(typeof(Fade));

        QuestionText = GameObject.Find("QuestionText");
        wrapScript = (WrapAround)QuestionText.GetComponent(typeof(WrapAround));

        PlayerCam = GameObject.Find("PlayerCam");
        Player = GameObject.Find("RigidbodyPlayer");
        PlayerScript = (RigifbodyWalker)Player.GetComponent(typeof(RigifbodyWalker));

        fadeScript.FadeIn();
        ScreenControl = GameObject.Find("Screen");
        screenLight = (Light)ScreenControl.GetComponent(typeof(Light));

        yield return new WaitForSeconds(3);
        while (ScreenControl.transform.localScale.x < 0.3)
        {
            yield return new WaitForSeconds(0.01f);
            ScreenControl.transform.localScale += new Vector3(0.01f, 0, 0);
            screenLight.intensity += 0.375f;
        }
        Questions = Random.Range(6, 9);
        bootedUp = true;
        printSentance();
    }
}
