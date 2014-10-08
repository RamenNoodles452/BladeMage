using UnityEngine;
using System.Collections;

public class CharacterBase : MonoBehaviour 
{
	public InputManager _inputManager;

	private Vector2 _characterOffset;

	//movement variables
	private bool _facingRight;

	public bool _running;
	public float _topSpeed;
	public float _initialSpeed;
	public float _acceleration;
	public float _runningTimeHorizontal;
	public float _runningTimeVertical;
	public float _deceleration;
	private float _currentSpeedHorizontal;
	private float _currentSpeedVertical;

	private Vector2 _shadowPos;
	private Vector2 _characterPos;
	public Vector2 _moveDirection; //unit vector of the direction the character wants to move in
	public Vector2 _velocity;

	private bool _recovering;
	public float _recoveryDuration;
	public float _recoveryTimer;

	//jump vars
	public bool _jumping;
	public float _jumpSpeed;
	public float _gravity;
	private float _jumpTime;

	private Vector2 _jumpVelocity;

	public GameObject _character;
	public ShadowScript _shadow;

	// Use this for initialization
	void Start () 
	{
		_inputManager = GetComponent<InputManager>();

		_characterOffset = new Vector2(0.0f, 2.4f);

		_velocity = Vector2.zero;
		_moveDirection = Vector2.zero;

		_facingRight = true;
		_running = false;
		_initialSpeed = 0.05f;
		_acceleration = 0.7f;
		_topSpeed = 0.225f;
		_runningTimeHorizontal = 0.0f;
		_runningTimeVertical = 0.0f;
		_deceleration = 1.0f;

		_shadowPos = this.transform.position;
		_characterPos = _shadowPos;

		_jumpVelocity = Vector2.zero;
		_jumping = false;
		_jumpSpeed = 0.8f;
		_gravity = 2.5f;
		_jumpTime = 0.0f;

		_recovering = false;
		_recoveryDuration = 0.25f;
		_recoveryTimer = 0.0f;

		//_bottomCenter.transform.position = _pos;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		ProcessInput();

		UpdateVelocity();

		if(_running)
		{
			/*
			_runningTimeHorizontal += Time.deltaTime * BattleManager.Instance._timeScale;
			_velocity.x = _moveDirection.x * (_initialSpeed + _acceleration * _runningTimeHorizontal);

			float maxSpeed = Mathf.Abs(_moveDirection.x) * _topSpeed;
			_velocity.x = Mathf.Clamp(_velocity.x, -maxSpeed, maxSpeed);

			/*
			if(_moveDirection.x > 0)
			{
				_velocity.x = Mathf.Min(_initialSpeed + _acceleration * _runningTime, _topSpeed);
			}
			else if(_moveDirection.x < 0)
			{
				_velocity.x = Mathf.Max(-_initialSpeed - _acceleration * _runningTime, -_topSpeed);
			}
			*/
			/*
			_runningTime += Time.deltaTime * BattleManager.Instance._timeScale;

			//_velocity.x = _moveDirection.x * (_initialSpeed + _acceleration * _runningTime);
			_velocity.x = 0.05f;
			_velocity.x = Mathf.Clamp(_velocity.x, -_topSpeed, _topSpeed);
			*/
		
			//_velocity.y = Mathf.Min(_initialSpeed + _acceleration * _runningTime, _topSpeed) * _moveDirection.y;

			//_velocity.x = Mathf.Min(_initialSpeed + _acceleration * _runningTime, _topSpeed);
		}
		else
		{
			//_velocity.x = 0;
			//if character was in motion, this decelerates the character
			/*
			_runningTime = 0.0f;
			if(_velocity.x > 0)
			{
				_velocity.x = Mathf.Max(_velocity.x - _deceleration * Time.deltaTime * BattleManager.Instance._timeScale, 0.0f);
			}
			else if(_velocity.x < 0)
			{
				_velocity.x = Mathf.Min(_velocity.x + _deceleration * Time.deltaTime * BattleManager.Instance._timeScale, 0.0f);
			}

			if(_velocity.y > 0)
			{
				_velocity.y = Mathf.Max(_velocity.y - _deceleration * Time.deltaTime * BattleManager.Instance._timeScale, 0.0f);
			}
			else if(_velocity.y < 0)
			{
				_velocity.y = Mathf.Min(_velocity.y + _deceleration * Time.deltaTime * BattleManager.Instance._timeScale, 0.0f);
			}
			*/
		}

		/*
		if(_jumping)
		{
			_velocity.y = _jumpSpeed - _jumpTime * _gravity;
		}
		*/
		//bounds checking
		/*
		if(_velocity.x > 0)
		{
			if(_shadow._rightBound.position.x < BattleManager.Instance._rightBound.position.x)
			{
				_shadowPos += _velocity;
			}
		}
		else if(_velocity.x < 0)
		{
			if(_shadow._leftBound.position.x > BattleManager.Instance._leftBound.position.x)
			{
				_shadowPos += _velocity;
			}
		}
		else if(_velocity.y > 0)
		{
			if(_shadow._upperBound.position.y < BattleManager.Instance._upperBound.position.y)
			{
				_shadowPos +=_velocity;
			}
		}
		else if(_velocity.y < 0)
		{
			if(_shadow._lowerBound.position.y > BattleManager.Instance._lowerBound.position.y)
			{
				_shadowPos +=_velocity;
			}
		}
		*/
		_shadowPos += _velocity;
		_characterPos += _velocity + _jumpVelocity;

		#region bounds checking
		_shadowPos.y = Mathf.Clamp(_shadowPos.y,
		                        BattleManager.Instance._lowerBound.position.y + _shadow._height/2,
		                        BattleManager.Instance._upperBound.position.y - _shadow._height/2);

		_shadowPos.x = Mathf.Clamp(_shadowPos.x, 
		                           BattleManager.Instance._leftBound.position.x + _shadow._width/2, 
		                           BattleManager.Instance._rightBound.position.x - _shadow._width/2);

		_characterPos.x = Mathf.Clamp(_characterPos.x, 
		                              BattleManager.Instance._leftBound.position.x + _shadow._width/2, 
		                              BattleManager.Instance._rightBound.position.x - _shadow._width/2);

		_characterPos.y = Mathf.Clamp(_characterPos.y, _shadowPos.y, 1000f);
		#endregion

		
		_character.transform.position = _characterPos + _characterOffset;
		this.transform.position = (Vector3)_shadowPos;
	}

	void ProcessInput()
	{
		#region process input
		if(!_recovering)
		{
			if(_inputManager._rightPressed)
			{
				_running = true;
				if(_moveDirection.x < 0f)
				{
					//play the turn animation right here
					_runningTimeHorizontal = 0.0f;
					_facingRight = true;
				}
				
				_moveDirection.x = 1f;
				
			}
			if(_inputManager._leftPressed)
			{
				_running = true;
				if(_moveDirection.x > 0f)
				{
					//play the turn animation right here
					_runningTimeHorizontal = 0.0f;
					_facingRight = false;
				}
				
				_moveDirection.x = -1f;
			}
			if(!_inputManager._rightPressed && !_inputManager._leftPressed)
			{
				_moveDirection.x = 0f;
			}
			
			if(_inputManager._upPressed)
			{
				_running = true;
				if(_moveDirection.y < 0f)
				{
					//play the turn animation right here
					_runningTimeVertical = 0.0f;
				}
				_moveDirection.y = 1f;
			}
			if(_inputManager._downPressed)
			{
				_running = true;
				if(_moveDirection.y > 0f)
				{
					//play the turn animation right here
					_runningTimeVertical = 0.0f;
				}
				_moveDirection.y = -1f;
			}
			if(!_inputManager._upPressed && !_inputManager._downPressed)
			{
				_moveDirection.y = 0f;
			}
			
			_moveDirection.Normalize();
			
			//stops the running
			if(!_inputManager._upPressed && !_inputManager._downPressed && !_inputManager._leftPressed && !_inputManager._rightPressed)
			{
				_moveDirection = Vector2.zero;
				
				_running = false;
				_runningTimeHorizontal = 0.0f; //might be unecessary
				_runningTimeVertical = 0.0f; //might be unecessary
			}
			
			if(_inputManager._jumpPressed)
			{
				if(!_jumping)
				{
					_jumping = true;
					_jumpVelocity.y = _jumpSpeed;
					_jumpTime = 0.0f;
					
					_currentSpeedHorizontal = _velocity.x;
					_currentSpeedVertical = _velocity.y;
				}
			}
		}
		//if you're revoering, don't process any input
		else
		{
			_recoveryTimer += Time.deltaTime * BattleManager.Instance._timeScale;
			if(_recoveryTimer >= _recoveryDuration)
			{
				_recoveryTimer = 0.0f;
				_recovering = false;
			}
		}
		#endregion
	}

	void UpdateVelocity()
	{
		#region Update Velocity
		//update velocity 
		if(!_jumping)
		{
			if(_moveDirection.x > 0 || _moveDirection.x < 0)
			{
				_runningTimeHorizontal += Time.deltaTime * BattleManager.Instance._timeScale;
				_velocity.x = _moveDirection.x * (_initialSpeed + _acceleration * _runningTimeHorizontal);
				
				float maxSpeed = Mathf.Abs(_moveDirection.x) * _topSpeed;
				_velocity.x = Mathf.Clamp(_velocity.x, -maxSpeed, maxSpeed);
			}
			else if(_moveDirection.x == 0)
			{
				//movespeed decay
				if(_velocity.x > 0)
				{
					_velocity.x = Mathf.Max(_velocity.x - _deceleration * Time.deltaTime * BattleManager.Instance._timeScale, 0.0f);
				}
				else if(_velocity.x < 0)
				{
					_velocity.x = Mathf.Min(_velocity.x + _deceleration * Time.deltaTime * BattleManager.Instance._timeScale, 0.0f);
				}
			}
			
			if(_moveDirection.y > 0 || _moveDirection.y < 0)
			{
				_runningTimeVertical += Time.deltaTime * BattleManager.Instance._timeScale;
				_velocity.y = _moveDirection.y * (_initialSpeed + _acceleration * _runningTimeVertical);
				
				float maxSpeed = Mathf.Abs(_moveDirection.y) * _topSpeed * 0.75f;
				_velocity.y = Mathf.Clamp(_velocity.y, -maxSpeed, maxSpeed);
			}
			else if(_moveDirection.y == 0)
			{
				//move speed decay
				if(_velocity.y > 0)
				{
					_velocity.y = Mathf.Max(_velocity.y - _deceleration * Time.deltaTime * BattleManager.Instance._timeScale, 0.0f);
				}
				else if(_velocity.y < 0)
				{
					_velocity.y = Mathf.Min(_velocity.y + _deceleration * Time.deltaTime * BattleManager.Instance._timeScale, 0.0f);
				}
			}
		}
		else
		{
			_jumpVelocity.y = _jumpSpeed - _jumpTime * _gravity;
			_jumpTime += Time.deltaTime * BattleManager.Instance._timeScale;
			
			if(_jumpVelocity.y < 0 && _shadowPos.y == _characterPos.y)
			{
				_moveDirection = Vector2.zero;
				_recovering = true;
				_jumping = false;
			}
			
		}
		#endregion

	}
}
