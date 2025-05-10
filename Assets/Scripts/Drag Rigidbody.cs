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

    // Новые переменные для управления приближением/отдалением
    public float scrollSpeed = 0.5f;  // Скорость изменения расстояния
    public float minDepth = 5.0f;     // Минимальное расстояние от камеры
    public float maxDepth = 15.0f;   // Максимальное расстояние от камеры

    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // Ось вращения (локальная)
    public float rotationSpeed = 10f;        // Скорость вращения
    public float angularDamping = 0.85f;

    private Vector3 previousMousePosition;

    void Awake()
    {
        // Поиск LineRendererLocation от корневого объекта Player
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

                        // Попытка найти LineRenderer на объекте или его детях
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
                            Debug.LogWarning("LineRenderer не найден на объекте LineRendererLocation или его детях");
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
                Debug.LogWarning("Main Camera не найдена в Player");
            }
        }
        else
        {
            Debug.LogWarning("Player не найден в сцене");
        }
    }

    void FixedUpdate()
    {
        if (jointTrans != null)
        {
            Rigidbody connectedRb = jointTrans.GetComponent<ConfigurableJoint>().connectedBody;

            // Применяем постоянное вращение
            Vector3 torque = connectedRb.transform.TransformDirection(rotationAxis)
                            * rotationSpeed
                            * Time.fixedDeltaTime;

            connectedRb.AddTorque(torque, ForceMode.VelocityChange);

            // Плавное затухание вращения
            connectedRb.angularVelocity *= angularDamping;
        }
    }

    void OnMouseDown() => HandleInputBegin(Input.mousePosition);
    void OnMouseUp() => HandleInputEnd(Input.mousePosition);
    void OnMouseDrag() => HandleInput(Input.mousePosition);

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
            lr.positionCount = 2;
    }

    public void HandleInput(Vector3 screenPosition)
    {
        if (jointTrans == null) return;

        // Обновляем позицию с учетом прокрутки
        dragDepth -= Input.mouseScrollDelta.y * scrollSpeed;
        dragDepth = Mathf.Clamp(dragDepth, minDepth, maxDepth);
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

        // Разрешаем свободное вращение во всех осях
        joint.angularXMotion = ConfigurableJointMotion.Free;
        joint.angularYMotion = ConfigurableJointMotion.Free;
        joint.angularZMotion = ConfigurableJointMotion.Free;

        // Настройка линейных ограничений
        JointDrive drive = new JointDrive
        {
            positionSpring = force,
            positionDamper = damping,
            maximumForce = Mathf.Infinity
        };

        joint.xDrive = drive;
        joint.yDrive = drive;
        joint.zDrive = drive;

        return go.transform;
    }

    private void DrawRope()
    {
        if (jointTrans == null || lr == null || lineRenderLocation == null) return;
        lr.SetPosition(0, lineRenderLocation.position);
        lr.SetPosition(1, jointTrans.position);
    }

    private void DestroyRope()
    {
        if (lr != null)
            lr.positionCount = 0;
    }
}
