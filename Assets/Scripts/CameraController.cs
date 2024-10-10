using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera TacticCamera;
    [SerializeField] private bool rotateCam = false;
    [SerializeField] Transform bottomLeftBorder;
    [SerializeField] Transform topRightBorder;
    private float fov = 60;
    private float fovMax = 100;
    private float fovMin = 10;
    public bool alive;
    public bool pausa;


    private void Awake() 
    {
     
    }


   private void Update() 
   {
        //alive = VisionManager.instance.Unit.gameObject.GetComponent<UnitController>().isAlive;
        
        /*
         if (GameManager.gameManager.gameState == GameManager.GameState.Idle)
        {
            return;
        }
        if (GameManager.gameManager.gameState == GameManager.GameState.Pause)
        {
            pausa = true;
        }
        */     
        if(!pausa)
        {
            CameraMovement();
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            rotateCam = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            rotateCam = false;
        }
        if (rotateCam == false)
        {
            CameraZoom();
        }
        if (rotateCam == true)
        {
            CameraRotation();
        }
      
      
    }
    private void CameraMovement() 
    {
        int edgeMoveSize = 30;
        Vector3 inputDir = new Vector3(0,0,0);



        if (GameManager.gameManager.inputMetod == GameManager.InputMetod.Keyboard)
        {
            if(Input.GetKey(KeyCode.W)) inputDir.z = +1f; 
            if(Input.GetKey(KeyCode.S)) inputDir.z = -1f; 
            if(Input.GetKey(KeyCode.A)) inputDir.x = -1f; 
            if(Input.GetKey(KeyCode.D)) inputDir.x = +1f;
        }
   
        if (GameManager.gameManager.inputMetod == GameManager.InputMetod.Mouse)
        {
            if(Input.mousePosition.x < edgeMoveSize) 
            {
                inputDir.x = -1f;
            }
            if(Input.mousePosition.y < edgeMoveSize) 
            {
                inputDir.z = -1f;
            }
            if(Input.mousePosition.x > Screen.width - edgeMoveSize) 
            {
                inputDir.x = +1f;
            }
            if(Input.mousePosition.y > Screen.height - edgeMoveSize) 
            {
                inputDir.z = +1f;
            }

            
        }
        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 10f;
        Vector3 position = transform.position;
        position += moveDir * (moveSpeed * Time.deltaTime);
        position.x = Mathf.Clamp(position.x, bottomLeftBorder.position.x, topRightBorder.position.x);
        position.z = Mathf.Clamp(position.z, bottomLeftBorder.position.z, topRightBorder.position.z);
        transform.position = position;
    }

    private void CameraRotation() //arreglar rotacion, tiene un pumping bastante fuerte
    {
        float rotateSpeed = 120f;
        float rotateDir= 0f;

        if (GameManager.gameManager.inputMetod == GameManager.InputMetod.Mouse)
        {
            if(Input.mouseScrollDelta.y > 0) rotateDir = +1f; 
            if(Input.mouseScrollDelta.y < 0) rotateDir = -1f;
        }

        if (GameManager.gameManager.inputMetod == GameManager.InputMetod.Keyboard)
        {
            if(Input.GetKey(KeyCode.Q)) rotateDir = +1f; 
            if(Input.GetKey(KeyCode.E)) rotateDir = -1f;
        }
         
        transform.eulerAngles += new Vector3(0, rotateDir*rotateSpeed*Time.deltaTime, 0);

        rotateDir= Mathf.Lerp(rotateDir, rotateDir, Time.deltaTime * rotateSpeed);

    }

   private void CameraZoom()
   {
        if (GameManager.gameManager.inputMetod == GameManager.InputMetod.Mouse)
        {
            if(Input.mouseScrollDelta.y > 0)
            {
                fov -= 5;
            }
            if(Input.mouseScrollDelta.y < 0)
            {
                fov += 5;
            }
        }
        
        if (GameManager.gameManager.inputMetod == GameManager.InputMetod.Keyboard)
        {
            if(Input.GetKey(KeyCode.E))
            {
                fov -= 5;
            }
            if(Input.GetKey(KeyCode.Q))
            {
                fov += 5;
            }
        }

        fov = Mathf.Clamp(fov, fovMin,fovMax);

        float zoomSpeed = 10f;

        TacticCamera.m_Lens.FieldOfView = Mathf.Lerp(TacticCamera.m_Lens.FieldOfView, fov, Time.deltaTime * zoomSpeed);
    }
}
