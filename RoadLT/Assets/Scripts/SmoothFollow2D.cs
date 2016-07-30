using UnityEngine;


public class SmoothFollow2D : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Transform target;
    private Camera _camera;
    
    void Start()
    {
        _camera = GetComponent<Camera>();
        target = FindObjectOfType<WheelJointCarMovement>().gameObject.transform;
    }

	// Update is called once per frame
	void Update ()
    {
	    if (target)
	    {
	        Vector3 point = _camera.WorldToViewportPoint(target.position);
	        Vector3 delta = target.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, point.z));
	        Vector3 destination = transform.position + delta;
	        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0);
	    }
	}
}
