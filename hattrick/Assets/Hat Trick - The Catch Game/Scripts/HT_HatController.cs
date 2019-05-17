using UnityEngine;
using System.Collections;

public class HT_HatController : MonoBehaviour {

	public Camera cam;

	[SerializeField]
	private float maxWidth;
	[SerializeField]
	private bool canControl;
	[SerializeField]
	private GameObject Ball; // the ball GameObject.
	[SerializeField]
	private GameObject Hat; //the Hat GameObject.
	[SerializeField]
	private GameObject Bomb; //the bomb GameObject.
	[SerializeField] 
	private Vector3 _hat; // the position of the Hat GameObject.
	[SerializeField] 
	private Vector3 _bomb; // the position of the bomb GameObject.
	[SerializeField] 
	private Vector3 ball; // the position of the Ball GameObject.
	[SerializeField] 
	private Vector3 offset;// desired offset in between the bomb and the player.
	[SerializeField] 
	private bool currentGameObj;

	private float speed = 5f;
 


	// Use this for initialization
	void Start ()
	{
		
		if (cam == null) {
			cam = Camera.main;
		}
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 targetWidth = cam.ScreenToWorldPoint (upperCorner);
		float hatWidth = GetComponent<Renderer>().bounds.extents.x;
		offset = new Vector3(2, 0,0);
		maxWidth = targetWidth.x - hatWidth;
		canControl = false;
		
	}
	
	// Update is called once per physics timestep
	void FixedUpdate () {
		if (canControl)
		{
			Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);
			Vector3 targetPosition = new Vector3(rawPosition.x, 0.0f, 0.0f);
			float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
			targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);
			GetComponent<Rigidbody2D>().MovePosition(targetPosition);
		}	
		Ball = GameObject.FindWithTag("Ball");
		Hat = GameObject.FindWithTag("Hat");
		Bomb = GameObject.FindWithTag("Bomb");
		aicontrols();
	}

	public void ToggleControl (bool toggle)
	{
		toggle = false;
		canControl = toggle;
	}

	public void aicontrols()
	{
		Hat.transform.position = _hat;
		if(Ball)
		{
			ball.x = Ball.transform.position.x;
			_hat.x = ball.x;
		}
		if(Bomb)
		{
			 _bomb.x = Bomb.transform.position.x;
			_hat.x = _bomb.x;
			if (_bomb.x > _hat.x )
			{
				_hat = new Vector3(_bomb.x - 3 ,0,0);
			}
			else
			{
				_hat = new Vector3(_bomb.x + 3 ,0,0);;
			}
		}
			
		
		//_hat.x = _bomb.x + offset.x;	
		
	}
}
