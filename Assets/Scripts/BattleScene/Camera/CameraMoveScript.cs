using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour
{
    Vector3 movementLR = new Vector3(1, 0, 0);
    Vector3 movementUD = new Vector3(0, 1, 0);

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            this.transform.position -= movementLR;
        if (Input.GetKey(KeyCode.D))
            this.transform.position += movementLR;

        if (Input.GetKey(KeyCode.W))
            this.transform.position += movementUD;
        if (Input.GetKey(KeyCode.S))
            this.transform.position -= movementUD;
        
    }
}
