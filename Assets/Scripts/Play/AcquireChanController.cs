using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*!
 *	----------------------------------------------------------------------
 *	@brief	アクワイアちゃん簡易操作スクリプト
 *	
*/
public class AcquireChanController : MonoBehaviour
{
	// Inspector
	[SerializeField] private float	m_WalkSpeed		= 2.0f;
	[SerializeField] private float	m_RunSpeed		= 3.5f;
	[SerializeField] private float	m_RotateSpeed	= 8.0f;
	[SerializeField] private float	m_JumpForce		= 300.0f;
	[SerializeField] private float	m_RunningStart	= 1.0f;
	[SerializeField] private float playerHp;
	[SerializeField] private new GameObject camera;
    [SerializeField] private AudioClip[] m_AudioClip = null;
	[SerializeField] private AudioSource m_AudioSource = null;


	// member
	private Rigidbody	m_RigidBody	= null;
	private Animator	m_Animator	= null;
	private Slider      m_HpSlider    = null;      
	private float		m_MoveTime	= 0;
	private float		m_MoveSpeed	= 0.0f;
	private bool		m_IsGround	= true;
	private bool        cursorLock  = false;
	private float       m_StartDashSE = 0;
	private float m_Xsensitivity = 3f, m_Ysensitivity = 3f;
	private float speed = 0.1f;
	private float minX = -90f, maxX = 90f;  // 角度制限用
	Quaternion cameraRot, characterRot;
	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	生成
	*/
	private void Awake()
	{
		m_RigidBody = this.GetComponentInChildren<Rigidbody>();
		m_Animator = this.GetComponentInChildren<Animator>();
		m_HpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
		m_MoveSpeed = m_WalkSpeed;
		cameraRot = camera.transform.localRotation;
		characterRot = transform.localRotation;
	}

	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	初期化
	*/
//	private void Start()
//	{
//	}

	/*!
	 *	----------------------------------------------------------------------
	 *	@brief	更新
	*/
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
        {
			cursorLock = false;
        }
		else if(Input.GetMouseButtonDown(2))
        {
			cursorLock = true;
        }

		if (cursorLock)
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		else if (!cursorLock)
		{
			Cursor.lockState = CursorLockMode.None;
		}
        
		float xRot = Input.GetAxis("Mouse X") * m_Ysensitivity;
		float yRot = Input.GetAxis("Mouse Y") * m_Xsensitivity;

		cameraRot *= Quaternion.Euler(-yRot,0,0);
		characterRot *= Quaternion.Euler(0,xRot,0);

		cameraRot = ClampRotation(cameraRot);

		camera.transform.localRotation = cameraRot;
		transform.localRotation = characterRot;

		if( null == m_RigidBody ) return;
		if( null == m_Animator ) return;

		// check ground
		float rayDistance = 0.3f;
		Vector3 rayOrigin = (this.transform.position + (Vector3.up * rayDistance * 0.5f));
		bool ground = Physics.Raycast( rayOrigin, Vector3.down, rayDistance, LayerMask.GetMask( "Default" ) );
		if( ground != m_IsGround )
		{
			m_IsGround = ground;

			// landing
			if( m_IsGround )
			{
				m_Animator.Play( "landing" );
			}
		}

		// input
		Vector3 vel = m_RigidBody.velocity;
		float h = Input.GetAxis( "Horizontal" );
		float v = Input.GetAxis( "Vertical" );
		bool isMove = ((0 != h) || (0 != v));

		m_MoveTime = isMove? (m_MoveTime + Time.deltaTime) : 0;
		bool isRun = (m_RunningStart <= m_MoveTime);

		// move speed (walk / run)
		float moveSpeed = isRun? m_RunSpeed : m_WalkSpeed;
		m_MoveSpeed = isMove? Mathf.Lerp( m_MoveSpeed, moveSpeed, (8.0f * Time.deltaTime) ) : m_WalkSpeed;
		if (moveSpeed == m_WalkSpeed)
		{
			WalkStepSE(m_AudioClip[0]);
		}
		else if (moveSpeed == m_RunSpeed)
		{
			RunStepSE(m_AudioClip[1]);
		}
		StopStepSE();
		//		m_MoveSpeed = moveSpeed;

		Vector3 inputDir = new Vector3(h, 0, v);        
		if( 1.0f < inputDir.magnitude ) inputDir.Normalize();

		if( 0 != h ) vel.x = (inputDir.x * m_MoveSpeed);
		if( 0 != v ) vel.z = (inputDir.z * m_MoveSpeed);

		m_RigidBody.velocity = vel;

		if( isMove )
		{
			// rotation
			float t = (m_RotateSpeed * Time.deltaTime);
			Vector3 forward = Vector3.Slerp( this.transform.forward, inputDir, t );
			this.transform.rotation = Quaternion.LookRotation( forward );	
		}

		m_Animator.SetBool( "isMove", isMove );
		m_Animator.SetBool( "isRun", isRun );


		// jump
		if( Input.GetButtonDown( "Jump" ) && m_IsGround		)
		{
			m_Animator.Play( "jump" );
			m_RigidBody.AddForce( Vector3.up * m_JumpForce );
		}



		// quit
		if( Input.GetKeyDown( KeyCode.Escape ) ) Application.Quit();
	}

	private float x, z;


	private void FixedUpdate()
	{
		x = 0;
		z = 0;

		x = Input.GetAxisRaw("Horizontal") * speed;
		z = Input.GetAxisRaw("Vertical") * speed;

		//transform.position += new Vector3(x, 0, z);
		transform.position += camera.transform.forward * z + camera.transform.right * x;

	}

	private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Goal")
        {
			Debug.Log("Goal");
        }
    }

	public void TakeHit(int damage)
    {
		playerHp -= damage;
		m_HpSlider.value = playerHp;

		if (playerHp <= 0)
        {
			playerHp = 0;
			m_HpSlider.value = playerHp;
			GameState.gameOver = true;
		}
		// playerHp = (int)Mathf.Clamp(playerHp - damage, 0, playerHp);
    }

	public Quaternion ClampRotation(Quaternion q)
    {
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1;

		float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;

		angleX = Mathf.Clamp(angleX, minX, maxX);

		q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);
		return q;
    }

	public void WalkStepSE(AudioClip clip)
    {
		m_AudioSource.loop = true;
		m_AudioSource.pitch = 1;
		m_AudioSource.clip = clip;
		m_AudioSource.Play();
    }

	public void RunStepSE(AudioClip clip)
	{
		m_AudioSource.loop = true;
		m_AudioSource.pitch = 1.3f;
		m_AudioSource.clip = clip;
		m_AudioSource.Play();
	}

	public void StopStepSE()
    {
		m_AudioSource.Stop();
		m_AudioSource.loop = false;
		m_AudioSource.pitch = 1;
	}
}
