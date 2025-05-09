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

        // ”станавливаем движение по всем ос€м как "Position"
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Limited;
        joint.zMotion = ConfigurableJointMotion.Limited;

        // Ќастраиваем приводы без использовани€ устаревшего JointDriveMode
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
}