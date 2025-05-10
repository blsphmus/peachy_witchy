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

    void Awake()
    {
        // Поиск LineRendererLocation по указанному пути
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

                        // Попробуем также найти LineRenderer на этом объекте или его дочерних объектах
                        LineRenderer foundLR = foundLocation.GetComponent<LineRenderer>();
                        if (foundLR == null)
                        {
                            foundLR = foundLocation.GetComponentInChildren<LineRenderer>();
                        }

                        if (foundLR != null)
                        {
                            lr = foundLR;
                        }
                        else
                        {
                            Debug.LogWarning("LineRenderer не найден на объекте LineRendererLocation или его дочерних объектах");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("LineRendererLocation не найден в wand");
                    }
                }
                else
                {
                    Debug.LogWarning("wand не найден в Main Camera");
                }
            }
            else
            {
                Debug.LogWarning("Main Camera не найден в Player");
            }
        }
        else
        {
            Debug.LogWarning("Player не найден в сцене");
        }
    }

    void OnMouseDown()
    {
        HandleInputBegin(Input.mousePosition);
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

        if (lr != null)
        {
            lr.positionCount = 2;
        }
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
        if (jointTrans != null)
        {
            Destroy(jointTrans.gameObject);
        }
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
        if (jointTrans == null || lr == null || lineRenderLocation == null)
        {
            return;
        }

        lr.SetPosition(0, lineRenderLocation.position);
        lr.SetPosition(1, jointTrans.position);
    }

    private void DestroyRope()
    {
        if (lr != null)
        {
            lr.positionCount = 0;
        }
    }
}