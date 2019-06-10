using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    private int nextCamera; //value of randomly generated camera
    private bool hasCameraChange; //if player has acquired ability to change cameras
    private bool isMainActive; //if main camera is the active camera or not
    private bool isReady; //if ability is ready to use
    private Camera mainCamera; //pointer to the main camera
    private Camera[] allCams; //list of all cameras in the level
    private CameraChangeCollectable change; //collectable with camera switch ability
    private float cooldownLeft;

    public Text cooldownText;
    public float cooldownTime;

    // Start is called before the first frame update
    void Start()
    {
        hasCameraChange = false;
        isMainActive = true;
        allCams = Camera.allCameras;
        mainCamera = Camera.main;
        foreach (Camera i in allCams)
        {
            i.enabled = false;
        }
        mainCamera.enabled = true;
        change = GameObject.FindGameObjectWithTag("Collectable").
            GetComponent<CameraChangeCollectable>();
        isReady = true;
        cooldownLeft = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Cooldown(Time.deltaTime);
        SwitchCameraAbility();
    }

    private void Cooldown(float DeltaTime)
    {
        if(!isReady)
        {
            cooldownLeft -= DeltaTime;
            cooldownText.text = "Camera Switch: " + Math.Round(cooldownLeft, 2);
            if(cooldownLeft <= 0)
            {
                isReady = true;
                cooldownLeft = 0;
                cooldownText.text = "Camera Switch: Q";
            }
        }
    }

    //switch camera ability
    private void SwitchCameraAbility()
    {
        if (!change.GetIsActive() && Input.GetKeyDown(KeyCode.Q))
        {
            if (!hasCameraChange)
            {
                hasCameraChange = true;
            }
            if (isReady && isMainActive)
            {

                print("Switch Cameras");
                SwitchRandomCameras();
            }

            else
            {
                print("Switch To Main");
                SwitchToMainCamera();
            }
        }
    }

    private void SwitchRandomCameras()
    {
        nextCamera = UnityEngine.Random.Range(1, allCams.Length);
        allCams[nextCamera].enabled = true;
        Camera.main.enabled = false;
        isMainActive = false;
        cooldownText.text = "Press Q to go back";
        print(nextCamera);
    }

    private void SwitchToMainCamera()
    {
        isReady = false;
        cooldownLeft = cooldownTime;
        allCams[nextCamera].enabled = false;
        mainCamera.enabled = true;
        isMainActive = true;
    }
}
