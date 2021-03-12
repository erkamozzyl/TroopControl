
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float cameraMovementSpeed;
    public Transform cameraRotate;
    public float cameraRotateSpeed;
    private float h;
    private float v;
   

   
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0,1,1) * (cameraMovementSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0,-1,-1) * (cameraMovementSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * (cameraMovementSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * (cameraMovementSpeed * Time.deltaTime));
        }

        if (Input.GetMouseButton(2))
        {
            h += cameraRotateSpeed * Input.GetAxis("Mouse X");
            cameraRotate.eulerAngles = new Vector3(0, h, 0f);
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f ) 
        {
            cameraRotate.Translate(0, -20 * Time.deltaTime,0);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) 
        {
            cameraRotate.Translate(0, 20 * Time.deltaTime,0);
        }

    }
}
