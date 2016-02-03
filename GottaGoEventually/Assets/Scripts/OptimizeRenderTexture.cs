using UnityEngine;
using System.Collections;

public class OptimizeRenderTexture : MonoBehaviour
{
    public Transform ER_RenderedPlane;
    public Camera renderingCam;
    public Rect rendTextInScreenSpace { get; private set; }

	void Awake()
    {
        Camera cam = GetComponent<Camera>();

        float dist = Vector3.Distance(ER_RenderedPlane.position,transform.position);  //get the distance to the texture this camer is rendering on

        print(dist);

        float rendTextWidth = ER_RenderedPlane.lossyScale.x * 10; //planes are 10 meters across, multiply that by it's scale
        float rendTextHeight = rendTextWidth * .5625f;

        print(cam.fieldOfView);
        float camViewWidth = (dist/Mathf.Sqrt(3)) *2; //(opposite/adjacent) * adjacent * 2 for full width of what the camera can see, dist meters away
        float camViewHeight = camViewWidth * .5625f;
        //float textToScreenRatio = rendTextWidth / camViewWidth;


        print("camviewwidth: "+ camViewWidth + "rendtextwidth: " + rendTextWidth);
        Vector2 bottomLeft = new Vector2(((camViewWidth/2 - rendTextWidth/2)/camViewWidth) * Screen.width,
                                         ((camViewHeight/2 - rendTextHeight /2)/camViewHeight) * Screen.height);
        print(bottomLeft);
        Vector2 rectSize = new Vector2(rendTextWidth, rendTextHeight);

        rendTextInScreenSpace = new Rect(bottomLeft, rectSize);

        if (renderingCam.targetTexture != null)
            renderingCam.targetTexture.Release();

        RenderTexture text = new RenderTexture((int)rendTextInScreenSpace.width, (int)rendTextInScreenSpace.height, 24);
        renderingCam.targetTexture = text;

        ER_RenderedPlane.GetComponent<Renderer>().material.mainTexture = text;
        
    }
}
