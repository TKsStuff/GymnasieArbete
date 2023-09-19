using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControll : MonoBehaviour
{

    public float sensitivity = 2f;
    public float smoothing = 10f;
    public float xMousePos;
    public float smoothMousePos;

    private float currentLooknigPos;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        getInput();
        modifyInput();
        movePlayer();
    }


    void getInput() 
    {

        xMousePos = Input.GetAxisRaw("Mouse X");
    
    }
     void modifyInput()
    {

        xMousePos *= sensitivity * smoothing;
        smoothMousePos = Mathf.Lerp(smoothMousePos, xMousePos, 1f / smoothing);

    }

    void movePlayer() {

        currentLooknigPos += smoothMousePos;
        transform.localRotation = Quaternion.AngleAxis(currentLooknigPos, transform.up);
    
    }


}



