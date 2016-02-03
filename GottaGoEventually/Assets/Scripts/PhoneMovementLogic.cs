using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhoneMovementLogic : MonoBehaviour {

    Vector3 topLocation;
    Vector3 bottomLocation;
    public float moveSpeed = .05f;
    public float volumeChange = .05f;

    public GameObject startText;
    public GameObject titleText;

    float loweredFraction = .75f;
    float raisedFraction = .25f;
    AudioSource[] audioSources;

	// Use this for initialization
	void Start () {
        topLocation = new Vector3(52.9f, 0f, -1f);
        bottomLocation = new Vector3(52.9f, -6.3f, -1f);
        transform.position = new Vector3(52.9f, -7f, -1f);

        GameManager.instance.Reset();
        QueueManager.singleton.Reset();
        audioSources = GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!GameManager.instance.gameOver)
            {
                StopAllCoroutines();
                if (!GameManager.instance.gameStarted)
                {
                    StartCoroutine("SlowRaisePhone");
                    StartCoroutine("SlowFadeIn");
                }
                else
                {
                    StartCoroutine("RaisePhone");
                    StartCoroutine("FadeMusicIn");
                }
            }
            else
            {
                Application.LoadLevel(Application.loadedLevelName);
            } 
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!GameManager.instance.gameOver)
            {
                StopAllCoroutines();
                StartCoroutine("LowerPhone");
                StartCoroutine("FadeMusicOut");
            }
        }
    }

    /*IEnumerator FadeOutTitle()
    {
        Color baseColor = startText.GetComponent<Text>().color;
        float alpha = startText.GetComponent<Text>().color.a;
        while (alpha > 0)
        {
            alpha -= 0.025f;
            startText.GetComponent<Text>().color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            titleText.GetComponent<Text>().color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            yield return null;
        }
    }*/

    public void ActivateLowerPhone()
    {
        StopAllCoroutines();
        StartCoroutine("LowerPhone");
    }

    IEnumerator RaisePhone()
    {
        GameManager.instance.player.startPhone();
        while(transform.position.y < topLocation.y)
        {
            transform.Translate(Vector3.forward * -1 * moveSpeed);
            if (transform.position.y > bottomLocation.y * loweredFraction)
            {
                GameManager.instance.phoneDown = false;
                GameManager.instance.player.isMoving = false;
            }
            if (transform.position.y > bottomLocation.y * raisedFraction)
            {
                GameManager.instance.phoneActive = true;
            }
            yield return null;
        }
    }

    IEnumerator SlowRaisePhone()
    {
        GameManager.instance.player.startPhone();
        while (transform.position.y < topLocation.y)
        {
            transform.Translate(Vector3.forward * -1 * moveSpeed/5);
            yield return null;
        }
        titleText.SetActive(false);
        startText.SetActive(false);
        GameManager.instance.phoneActive = true;
        GameManager.instance.setGameStarted();
    }

    IEnumerator LowerPhone()
    {
        GameManager.instance.player.endPhone();
        while (transform.position.y > bottomLocation.y)
        {
            transform.Translate(Vector3.forward * moveSpeed);
            if (transform.position.y < bottomLocation.y * loweredFraction)
            {
                GameManager.instance.phoneDown = true;
                if (GameManager.instance.gameStarted)
                     GameManager.instance.player.isMoving = true;
            }
            if (transform.position.y < bottomLocation.y * raisedFraction)
            {
                GameManager.instance.phoneActive = false;
            }
            yield return null;
        }
    }

    IEnumerator FadeMusicIn()
    {
        while (audioSources[1].volume < 1)
        {
            audioSources[1].volume += volumeChange;
            audioSources[0].volume -= volumeChange / 2;
            yield return null;
        }
    }

    IEnumerator SlowFadeIn()
    {
        while (audioSources[1].volume < 1)
        {
            audioSources[1].volume += volumeChange/10;
            audioSources[0].volume -= volumeChange / 20;
            yield return null;
        }
    }

    IEnumerator FadeMusicOut()
    {
        while (audioSources[0].volume < .5f)
        {
            audioSources[1].volume -= volumeChange;
            audioSources[0].volume += volumeChange / 2;
            yield return null;
        }
    }
}
