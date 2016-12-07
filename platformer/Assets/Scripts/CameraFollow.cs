using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public float smoothTimeY = 0.05f;
    public float smoothTimeX = 0.05f;

    public bool bounds;

    public GameObject player;

    private Vector2 velocity;
    public Vector3 minCameraPos = new Vector3 (-100f, 4.43f, -10f);
    public Vector3 maxCameraPos = new Vector3 (1000f, 1000f, -10f);

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
    }
	
    // Update is called once per frame
    void Update ()
    {
	
    }

    void FixedUpdate ()
    {
        float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

        transform.position = new Vector3 (posX, posY, transform.position.z); // z - camera

        if (bounds) {
            transform.position = new Vector3 (
                Mathf.Clamp (transform.position.x, minCameraPos.x, maxCameraPos.x), 
                Mathf.Clamp (transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z)
            );
        }
    }

    public void SetMinPosition ()
    {
        minCameraPos = gameObject.transform.position;
    }

    public void SetMaxPosition ()
    {
        maxCameraPos = gameObject.transform.position;
    }
}
