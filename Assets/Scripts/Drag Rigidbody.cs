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

    // пїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ/пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    public float scrollSpeed = 0.5f;  // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ
    public float minDepth = 1.0f;     // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ
    public float maxDepth = 15.0f;   // пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅпїЅ пїЅпїЅ пїЅпїЅпїЅпїЅпїЅпїЅ

    [Header("Rotation Settings")]
    public Vector3 rotationAxis = Vector3.up; // ��� �������� (���������)
    public float rotationSpeed = 10f;        // �������� ��������
    public float angularDamping = 0.85f;

    private Vector3 previousMousePosition;

    [Header("References")]
    public PlayerLook playerLook;

    void Start()
    {
        AutoAssignReferences();
    }

    

    private void AutoAssignReferences()
    {
        // »щем PlayerLook
        if (playerLook == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                playerLook = player.GetComponent<PlayerLook>();

                // ≈сли не нашли, попробуем найти в дочерних объектах
                if (playerLook == null)
                    playerLook = player.GetComponentInChildren<PlayerLook>();
            }
        }

        // »щем LineRendererLocation
        if (lineRenderLocation == null || lr == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player != null)
            {
                Transform mainCamera = player.transform.Find("Main Camera");
                if (mainCamera != null)
                {
                    Transform wand = mainCamera.Find("wand");
                    if (wand != null)
                    {
                        // ѕоиск LineRendererLocation
                        if (lineRenderLocation == null)
                            lineRenderLocation = wand.Find("LineRendererLocation");

                        // ѕоиск LineRenderer
                        if (lr == null && lineRenderLocation != null)
                        {
                            lr = lineRenderLocation.GetComponent<LineRenderer>();
                            if (lr == null)
                                lr = lineRenderLocation.GetComponentInChildren<LineRenderer>();
                        }
                    }
                }
            }
        }

    }

    void FixedUpdate()
    {
        if (jointTrans != null)
        {
            Rigidbody connectedRb = jointTrans.GetComponent<ConfigurableJoint>().connectedBody;

            // ��������� ���������� ��������
            Vector3 torque = connectedRb.transform.TransformDirection(rotationAxis)
                            * rotationSpeed
                            * Time.fixedDeltaTime;

            connectedRb.AddTorque(torque, ForceMode.VelocityChange);

            // ������� ��������� ��������
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

        // ��������� ������� � ������ ���������
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

        // ��������� ��������� �������� �� ���� ����
        joint.angularXMotion = ConfigurableJointMotion.Free;
        joint.angularYMotion = ConfigurableJointMotion.Free;
        joint.angularZMotion = ConfigurableJointMotion.Free;

        // ��������� �������� �����������
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