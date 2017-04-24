using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour
{
    Vector3 movement;
    void Start()
    {
        movement.x = 1;
        movement.y = 0;
        movement.z = 0;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            this.transform.position -= movement;

        if (Input.GetKey(KeyCode.D))
            this.transform.position += movement;
    }
}
