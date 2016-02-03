using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhoneMovementLogic : MonoBehaviour {

    Vector3 topLocation;
    Vector3 bottomLocation;
    public float moveSpeed = .005f;

    public GameObject startText;
    public GameObject titleText;

    float loweredFraction = .75f;
    float raisedFraction = .25f;

	// Use this for initialization
	void Start () {
        topLocation = new Vector3(52.9f, 0f, -1f);
        bottomLocation = new Vector3(52.9f, -6.3f, -1f);
        transform.position = new Vector3(52.9f, -7f, -1f);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopAllCoroutines();
            if (!GameManager.instance.gameStarted)
            {
                StartCoroutine("SlowRaisePhone");
            }
            else
            {
                StartCoroutine("RaisePhone");
            } 
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAllCoroutines();
            StartCoroutine("LowerPhone");
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
}
