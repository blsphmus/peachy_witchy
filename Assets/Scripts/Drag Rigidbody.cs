using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DragRigidbody : MonoBehaviour
{
    public float force = 600;
    public float damping = 6;
    public float distance = 15;

    public LineRenderer lr;
    public Transform lineRenderLocation;

    Transform jointTrans;
    float dragDepth;

<<<<<<< Updated upstream
=======
    // пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ/пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    public float scrollSpeed = 0.5f;  // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    public float minDepth = 1.0f;     // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
    public float maxDepth = 15.0f;   // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ

>>>>>>> Stashed changes
    [Header("Rotation Settings")]
    public float rotationSpeed = 5f;
    private bool isRotating; // Флаг режима вращения
    private Vector3 lastMousePosition;

    [Header("References")]
    public PlayerLook playerLook;


    void OnMouseDown()
    {
<<<<<<< Updated upstream
        HandleInputBegin(Input.mousePosition);
=======
        // пїЅпїЅпїЅпїЅпїЅ LineRendererLocation пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ Player
        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            Transform mainCamera = player.transform.Find("Main Camera");
            if (mainCamera != null)
            {
                Transform wand = mainCamera.Find("wand");
                if (wand != null)
                {
                    Transform foundLocation = wand.Find("LineRendererLocation");
                    if (foundLocation != null)
                    {
                        lineRenderLocation = foundLocation;

                        // пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ LineRenderer пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ
                        LineRenderer foundLR = foundLocation.GetComponent<LineRenderer>();
                        if (foundLR == null)
                        {
                            foundLR = foundLocation.GetComponentInChildren<LineRenderer>();
                        }

                        if (foundLR != null)
                        {
                            lr = foundLR;
                        }
                        //else
                        //{
                        //    Debug.LogWarning("LineRenderer пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ LineRendererLocation пїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅ");
                        //}
                    }
                    //else
                    //{
                    //    Debug.LogWarning("LineRendererLocation пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅ wand");
                    //}
                }
                //else
                //{
                //    Debug.LogWarning("wand пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅ Main Camera");
                //}
            }
            //else
            //{
            //    Debug.LogWarning("Main Camera пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅ Player");
            //}
        }
        //else
        //{
        //    Debug.LogWarning("Player пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ пїЅ пїЅпїЅпїЅпїЅпїЅ");
        //}
>>>>>>> Stashed changes
    }

    void OnMouseUp()
    {
        HandleInputEnd(Input.mousePosition);
    }

    void OnMouseDrag()
    {
        HandleInput(Input.mousePosition);
    }

    public void HandleInputBegin(Vector3 screenPosition)
    {
        var ray = Camera.main.ScreenPointToRay(screenPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Interactive"))
            {
                dragDepth = CameraPlane.CameraToPointDepth(Camera.main, hit.point);
                jointTrans = AttachJoint(hit.rigidbody, hit.point);
            }
        }

        lr.positionCount = 2;
    }

    public void HandleInput(Vector3 screenPosition)
    {
        if (jointTrans == null)
            return;
        var worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        jointTrans.position = CameraPlane.ScreenToWorldPlanePoint(Camera.main, dragDepth, screenPosition);

        DrawRope();
    }

    public void HandleInputEnd(Vector3 screenPosition)
    {
        DestroyRope();
        Destroy(jointTrans.gameObject);
    }

    Transform AttachJoint(Rigidbody rb, Vector3 attachmentPosition)
    {
        GameObject go = new GameObject("Attachment Point");
        go.hideFlags = HideFlags.HideInHierarchy;
        go.transform.position = attachmentPosition;

        var newRb = go.AddComponent<Rigidbody>();
        newRb.isKinematic = true;

        var joint = go.AddComponent<ConfigurableJoint>();
        joint.connectedBody = rb;
        joint.configuredInWorldSpace = true;

        // Устанавливаем движение по всем осям как "Position"
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Limited;

        // Настраиваем приводы без использования устаревшего JointDriveMode
        JointDrive drive = new JointDrive
        {
            positionSpring = force,
            positionDamper = damping,
            maximumForce = Mathf.Infinity
        };

        joint.xDrive = drive;
        joint.yDrive = drive;
        joint.zDrive = drive;
        joint.slerpDrive = drive;
        joint.rotationDriveMode = RotationDriveMode.Slerp;

        return go.transform;
    }

    private void DrawRope()
    {
        if (jointTrans == null)
        {
            return;
        }

        lr.SetPosition(0, lineRenderLocation.position);
        lr.SetPosition(1, this.transform.position);
    }

    private void DestroyRope()
    {
        lr.positionCount = 0;
    }

    void Update()
    {
        if (isRotating)
        {
            HandleRotation();
        }
    }

    void OnMouseOver()
    {
        if (jointTrans == null) return; // Добавить проверку

        if (Input.GetMouseButtonDown(1)) // Убрать лишнюю проверку
        {
            StartRotation();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopRotation();
        }
    }

    void StartRotation()
    {
        if (playerLook == null) // Проверка
        {
            //Debug.LogError("PlayerLook не назначен в инспекторе!");
            return;
        }

        isRotating = true;
        lastMousePosition = Input.mousePosition;

        playerLook.enabled = false; // Используем публичную ссылку
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void StopRotation()
    {
        if (playerLook == null) // Проверка
        {
            //Debug.LogError("PlayerLook не назначен в инспекторе!");
            return;
        }

        isRotating = false;

        playerLook.enabled = true; // Используем публичную ссылку
        Cursor.lockState = CursorLockMode.Locked;
    }

    void HandleRotation()
    {
        Vector3 delta = Input.mousePosition - lastMousePosition;
        lastMousePosition = Input.mousePosition;

        // Вращаем объект
        float rotationX = delta.x * rotationSpeed * Time.deltaTime;
        float rotationY = delta.y * rotationSpeed * Time.deltaTime;

        jointTrans.Rotate(Camera.main.transform.up, -rotationX, Space.World);
        jointTrans.Rotate(Camera.main.transform.right, rotationY, Space.World);
    }
}