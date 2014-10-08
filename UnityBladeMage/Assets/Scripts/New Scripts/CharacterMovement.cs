using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour 
{
	private PlayerController _input;
	private CharacterInfo _character;
	private BoxCollider2D _boxCollider;
	
	//movement variables
	public bool _facingRight;

	private Vector2 _characterPos;
	private Vector2 _characterGroundPos;
	private Vector2 _moveDirection; //unit vector of the direction the character wants to move in
	private Vector2 _velocity;
	private float _recoveryTimer;

	public ShadowScript _shadow;
	public GameObject _bottomCenter; //TODO: MIght be unnecessary

	//jump velocities
	private bool _jumping;
	private bool _jumpRecovery;
	private float _jumpRecoveryTime;
	private float _gravity;
	private float _jumpTime;
	private Vector2 _jumpVelocity;

	// Use this for initialization
	void Start () 
	{
		_input = GetComponent<PlayerController>();
		_character = GameState._party[0];
		_boxCollider = GetComponent<BoxCollider2D>();

		_characterPos = Vector2.zero;
		_characterGroundPos = Vector2.zero;
		_moveDirection = Vector2.zero;
		_velocity = Vector2.zero;
		_jumpVelocity = Vector2.zero;

		_facingRight = true;
		_jumping = false;
		_jumpRecovery = false;

		_gravity = 270f;
		_jumpTime = 0.0f;
		_jumpRecoveryTime = 0.25f;

		_recoveryTimer = 0.0f;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		ProcessInput();
		UpdateVelocity();

		//Move the character
		_characterGroundPos += _velocity * GameState._velocityLimiter;
		_characterGroundPos.x = Mathf.Clamp(_characterGroundPos.x,  
		                                   BattleManager.Instance._leftBound.position.x + _shadow._width/2, 
		                                   BattleManager.Instance._rightBound.position.x - _shadow._width/2);
		_characterGroundPos.y = Mathf.Clamp(_characterGroundPos.y,
		                              BattleManager.Instance._lowerBound.position.y + _shadow._height/2,
		                              BattleManager.Instance._upperBound.position.y - _shadow._height/2);

		_bottomCenter.transform.position = (Vector3)_characterGroundPos;
		_shadow.transform.position = _bottomCenter.transform.position;

		_characterPos = _characterGroundPos;
		_characterPos += _jumpVelocity * GameState._velocityLimiter;
		_characterPos.y = Mathf.Max(_characterPos.y, _bottomCenter.transform.position.y);

		transform.position = (Vector3)_characterPos + new Vector3(0, _boxCollider.bounds.extents.y, 0);
	}

	void ProcessInput()
	{
		if(_jumpRecovery)
		{
			if(_recoveryTimer > _jumpRecoveryTime)
				_jumpRecovery = false;
			else
				_recoveryTimer += Time.deltaTime * BattleManager.Instance._timeScale;
		}
		else
		{
			#region process directional input
			if(GameSettings._usingController)
			{
				_moveDirection.x = _input._joystick.x;
				_moveDirection.y = _input._joystick.y;

				if(_character._state == CharacterState.idle || _character._state == CharacterState.running)
				{
					_character._state = CharacterState.running;
					if(_input._joystick.x > 0f && _input._joystickPrev.x <= 0f)
					{
						//play the turn animation right here
						_facingRight = true;
					}
					else if(_input._joystick.x < 0f && _input._joystickPrev.x >= 0f)
					{
						_facingRight = false;
					}

				}
			}
			else
			{
				if(_input._rightPressed)
				{
					if(_character._state == CharacterState.idle || _character._state == CharacterState.running)
					{
						_character._state = CharacterState.running;
						if(_moveDirection.x < 0f)
						{
							//play the turn animation right here
							_facingRight = true;
						}
						
						_moveDirection.x = 1f;
					}
					
				}
				if(_input._leftPressed)
				{
					if(_character._state == CharacterState.idle || _character._state == CharacterState.running)
					{
						_character._state = CharacterState.running;
						if(_moveDirection.x > 0f)
						{
							//play the turn animation right here
							_facingRight = false;
						}
						
						_moveDirection.x = -1f;
					}
				}
				if(!_input._rightPressed && !_input._leftPressed)
				{
					_character._state = CharacterState.idle;
					if(_character._state == CharacterState.idle || _character._state == CharacterState.running)
					{
						_moveDirection.x = 0f;
					}
				}

			}
			#endregion

			#region Process Jump Input
			if(GameSettings._usingController)
			{
				if(_character._state == CharacterState.idle || _character._state == CharacterState.running)
				{
					if(_input._jumpPressed == true && _input._jumpPressedPrev == false)
					{
						ProcessJump();
					}
				}
			}
			else
			{

			}
			#endregion

			_moveDirection.Normalize();
		}
	}

	void UpdateVelocity()
	{
		if(!_jumping)
		{
			if(_moveDirection.x > 0 || _moveDirection.x < 0)
			{
				_velocity.x = _moveDirection.x * _character._moveSpeed * _character._moveSpeedModifier;
			}
			else if(_moveDirection.x == 0)
			{
				_velocity.x = 0;
			}

			if(_moveDirection.y > 0 || _moveDirection.y < 0)
			{
				_velocity.y = _moveDirection.y * _character._moveSpeed * _character._moveSpeedModifier * 0.65f;
			}
			else if(_moveDirection.y == 0)
			{
				_velocity.y = 0;
			}
		}
		else
		{
			_jumpVelocity.y += _character._jumpSpeed - _jumpTime * _gravity;
			_jumpTime += Time.deltaTime * BattleManager.Instance._timeScale;

			if(_jumpVelocity.y < 0 && _shadow.transform.position.y == _characterPos.y)
			{
				_moveDirection = Vector2.zero;
				_recoveryTimer = 0.0f;
				_jumpRecovery = true;
				_jumping = false;
			}

		}
	}

	void ProcessJump()
	{
		if(!_jumping)
		{
			_jumping = true;
			_jumpVelocity.y = _character._jumpSpeed;
			_jumpTime = 0.0f;
		}
	}
}
