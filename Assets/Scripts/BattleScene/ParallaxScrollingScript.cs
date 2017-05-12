using UnityEngine;
using System.Collections;

public class ParallaxScrollingScript : MonoBehaviour 
{
    public bool hasParallax, canScroll;
    public float backgroundSize;
    public float parallaxSpeed;
    public Camera camera;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;

	// Use this for initialization
	void Start () 
    {
        if (camera == null)
            camera = Camera.main;

        cameraTransform = camera.transform;
        viewZone = camera.orthographicSize;
        
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; ++i)
            layers[i] = transform.GetChild(i);

        leftIndex = 0;
        rightIndex = layers.Length - 1;
        
        if (parallaxSpeed > 0)
            hasParallax = true;
	}
	
	// Update is called once per frame
    void Update()
    {
        if (hasParallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpeed);
        }

        lastCameraX = cameraTransform.position.x;

        if (canScroll)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
                ScrollLeft();

            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
                ScrollRight();
        }
        
    } 
	
    private void ScrollLeft()
    {
        //int lastRight = rightIndex; never used
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight()
    {
        //int lastleft = leftIndex; never used
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}

