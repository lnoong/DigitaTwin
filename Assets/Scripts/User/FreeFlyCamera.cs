//===========================================================================//
//                       FreeFlyCamera (Version 1.2)                         //
//                        (c) 2019 Sergey Stafeyev                           //
//===========================================================================//

using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Camera))]
public class FreeFlyCamera : MonoBehaviour
{
    #region UI

    [Space]

    [SerializeField]
    [Tooltip("The script is currently active")]
    private bool _active = true;

    [Space]

    [SerializeField]
    [Tooltip("Camera rotation by mouse movement is active")]
    private bool _enableRotation = true;

    [SerializeField]
    [Tooltip("Sensitivity of mouse rotation")]
    private float _mouseSense = 1.8f;

    [Space]

    [SerializeField]
    [Tooltip("Camera zooming in/out by 'Mouse Scroll Wheel' is active")]
    private bool _enableZoom = true;

    [SerializeField]
    [Tooltip("Velocity of camera zooming in/out")]
    private float _zoomSpeed = 55f;

    [Space]

    [SerializeField]
    [Tooltip("Camera movement by 'W','A','S','D','Q','E' keys is active")]
    private bool _enableMovement = true;

    [SerializeField]
    [Tooltip("Camera movement speed")]
    private float _movementSpeed = 50f;

    [Space]

    [SerializeField]
    [Tooltip("Acceleration at camera movement is active")]
    private bool _enableSpeedAcceleration = true;

    [SerializeField]
    [Tooltip("Rate which is applied during camera movement")]
    private float _speedAccelerationFactor = 1.5f;

    [Space]

    [SerializeField]
    [Tooltip("This keypress will move the camera to initialization position")]

    #endregion UI


    private float _currentIncrease = 1;
    private float _currentIncreaseMem = 0;

    private Vector3 _initPosition;
    private Vector3 _initRotation;

    private void Start()
    {
        _initPosition = transform.position;
        _initRotation = transform.eulerAngles;
    }

    // Apply requested cursor state
    // private void SetCursorState()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         Cursor.lockState = _wantedMode = CursorLockMode.None;
    //     }

    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         _wantedMode = CursorLockMode.Locked;
    //     }

    //     // Apply cursor state
    //     Cursor.lockState = _wantedMode;
    //     // Hide cursor when locking
    //     Cursor.visible = (CursorLockMode.Locked != _wantedMode);
    // }

    private void CalculateCurrentIncrease(bool moving)
    {
        _currentIncrease = Time.deltaTime;

        if (!_enableSpeedAcceleration || _enableSpeedAcceleration && !moving)
        {
            _currentIncreaseMem = 0;
            return;
        }

        _currentIncreaseMem += Time.deltaTime * (_speedAccelerationFactor - 1);
        _currentIncrease = Time.deltaTime + Mathf.Pow(_currentIncreaseMem, 3) * Time.deltaTime;
    }

    private void Update()
    {
        if (!_active)
            return;
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;
        // Zoom
        if (_enableZoom)
        {
            transform.Translate(Vector3.forward * Input.mouseScrollDelta.y * Time.deltaTime * _zoomSpeed);
        }

        // Movement
        if (_enableMovement && Input.GetMouseButton(2))
        {

            Vector3 mouseDelta = new Vector3(-Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"), 0);
    
            // 将鼠标移动增量转换到相机的局部坐标系中
            Vector3 deltaPosition = transform.TransformDirection(mouseDelta) * _mouseSense;
            
            float currentSpeed = _movementSpeed;
            CalculateCurrentIncrease(deltaPosition != Vector3.zero);
            
            transform.position += deltaPosition * currentSpeed * _currentIncrease;

        }

        // Rotation
        if (_enableRotation && Input.GetMouseButton(1))
        {
            // Pitch
            transform.rotation *= Quaternion.AngleAxis(
                -Input.GetAxis("Mouse Y") * _mouseSense,
                Vector3.right
            );

            // Paw
            transform.rotation = Quaternion.Euler(
                transform.eulerAngles.x,
                transform.eulerAngles.y + Input.GetAxis("Mouse X") * _mouseSense,
                transform.eulerAngles.z
            );
        }
    }
}
