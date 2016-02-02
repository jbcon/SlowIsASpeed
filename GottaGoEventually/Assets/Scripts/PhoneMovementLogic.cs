using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhoneMovementLogic : MonoBehaviour {

    Vector3 topLocation;
    Vector3 bottomLocation;
    public float moveSpeed = .5f;

    public GameObject startText;
    public GameObject titleText;

    bool firstTime = true;

	// Use this for initialization
	void Start () {
        topLocation = new Vector3(49.72f, 0f, 9.95f);
        bottomLocation = new Vector3(49.72f, -11f, 9.95f);
        transform.position = bottomLocation;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopAllCoroutines();
            if (firstTime)
            {
                StartCoroutine("FadeOutTitle");
                firstTime = false;
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

    IEnumerator FadeOutTitle()
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
    }

    IEnumerator RaisePhone()
    {
        while(transform.position.y < topLocation.y)
        {
            transform.Translate(Vector3.forward * -1 * moveSpeed);
            if (transform.position.y > bottomLocation.y * .75f)
            {
                GameManager.instance.phoneDown = false;
            }
            yield return null;
        }
        GameManager.instance.phoneActive = true;
    }

    IEnumerator SlowRaisePhone()
    {
        while (transform.position.y < topLocation.y)
        {
            transform.Translate(Vector3.forward * -1 * moveSpeed/5);
            yield return null;
        }
        GameManager.instance.phoneActive = true;
    }

    IEnumerator LowerPhone()
    {
        GameManager.instance.phoneActive = false;
        while (transform.position.y > bottomLocation.y)
        {
            transform.Translate(Vector3.forward * moveSpeed);
            if (transform.position.y < bottomLocation.y * .75f)
            {
                GameManager.instance.phoneDown = true;
            }
            yield return null;
        }
    }
}
