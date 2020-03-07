using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerBehaviour : MonoBehaviour
{

	// Definir Hashes de:
	// Parametros (Speed, Attack, Damage, Dead)
	static int speedHash = Animator.StringToHash("Speed");
	static int attackHash = Animator.StringToHash("Attack");
	static int damageHash = Animator.StringToHash("Damage");
	static int distanceHash = Animator.StringToHash("Distance");
	static int deadHash = Animator.StringToHash("Dead");

	public Animator animComponent = null;
	public Rigidbody rigidBody = null;
	public BoxCollider box;

	public bool usingScreen;

	// Estados (Base Layer.Idle, Attack Layer.Idle, Attack Layer.Attack)
	// TODO

	public float walkSpeed = 1;     // Parametro que define la velocidad de "caminar"
	public float runSpeed = 2.1f;      // Parametro que define la velocidad de "correr"
	public float rotateSpeed = 0.4f;        // Parametro que define la velocidad de "girar"

	// Variables auxiliares
	float _angularSpeed = 0;        // Velocidad de giro actual
	float _speed = 0;			  // Velocidad de traslacion actual
	float original_z;			  // Valora original de la posición 'z' del collider
	public float verticalAxis;
	public float horizontalAxis;


	// Variables internas:
	int _lifes = 3;                         // Vidas restantes
	public bool paused = false;             // Indica si el player esta pausado (congelado). Que no responde al Input
	public bool attack = false;

	//Variable bool para limitar a 1 ataque cada vez que se realiza la colisión en modo ataque
	private bool singleAttack;

	//Sonidos
	public AudioClip attackAudio = null;
	public AudioClip damageAudio = null;
	public AudioClip deadAudio = null;
	public AudioClip stepsAudio = null;

	public AudioSource source = null;

	void Start()
	{
		// Obtener los componentes Animator, Rigidbody y el valor original center.z del BoxCollider
		animComponent = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody>();
		box = GetComponent<BoxCollider>();
		original_z = box.center.z;

		source = GetComponent<AudioSource>();
	}

	// Aqui moveremos y giraremos la araña en funcion del Input
	void FixedUpdate()
	{
		// Si estoy en pausa no hacer nada (no moverme ni atacar)
		if (paused) return;

		// Cálculo de velocidad lineal (_speed) y angular (_angularSpeed) en función del Input
		//Comprobamos que no se están usando los controles de pantalla
		if (!usingScreen)
		{
			verticalAxis = Input.GetAxis("Vertical");
			horizontalAxis = Input.GetAxis("Horizontal");
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				verticalAxis *= runSpeed;
			}

		}

		// Si camino/corro hacia delante delante: _speed = walkSpeed   /  _speed = runSpeed
		if (verticalAxis > 0.1f)
		{
			_speed = walkSpeed;
		}

		if (verticalAxis > 2.0f)
		{
			_speed = runSpeed;
		}

		// Si camino/corro hacia delante detras: _speed = -walkSpeed
		if (verticalAxis < -0.1f)
		{
			_speed = -walkSpeed;
		}

		// Si no me muevo: _speed = 0
		if (verticalAxis > -0.1f && verticalAxis < 0.1f)
		{
			_speed = 0;
		}

		// Si giro izquierda: _angularSpeed = -rotateSpeed;
		if (horizontalAxis < -0.1f)
		{
			_angularSpeed = -rotateSpeed;
		}

		// Si giro derecha: _angularSpeed = rotateSpeed;
		if (horizontalAxis > 0.1f)
		{
			_angularSpeed = rotateSpeed;
		}

		// Si no me muevo: _speed = 0
		if (horizontalAxis > -0.1f && horizontalAxis < 0.1f)
		{
			_angularSpeed = 0;
		}

		// Actualizamos el parámetro "Speed" en función de _speed. Para activar la animación de caminar/correr
		animComponent.SetFloat(speedHash, verticalAxis);

		// Movemos y rotamos el rigidbody (MovePosition y MoveRotation) en función de "_speed" y "_angularSpeed"
		rigidBody.AddRelativeForce(Vector3.forward * _speed, ForceMode.VelocityChange);
		rigidBody.AddTorque(Vector3.up * _angularSpeed, ForceMode.VelocityChange);

		if(_speed!=0.0f && !source.isPlaying)
		{
			source.clip = stepsAudio;
			source.Play();
		}

		if (_speed == 0.0f && source.clip == stepsAudio)
		{
			source.Stop();
		}

		// Mover el collider en función del parámetro "Distance" (necesario cuando atacamos)
		box.center = new Vector3(box.center.x, box.center.y, original_z + animComponent.GetFloat("Distance") * 10.0f);
	}

	// En este bucle solamente comprobaremos si el Input nos indica "atacar" y activaremos el trigger "Attack"
	private void Update()
	{
		// Si estoy en pausa no hacer nada (no moverme ni atacar)
		if (paused) return;

		if (Input.GetKeyDown(KeyCode.Space) && !usingScreen)
		{
			attackAction(true);
		}

		if (usingScreen)
		{
			attackAction(attack);
		}
	}

	//Función attack
	private void attackAction(bool attack)
	{
		if (attack)
		{
			animComponent.SetTrigger(attackHash);
			source.clip = attackAudio;
			source.PlayDelayed(0.5f);

		}
		else
		{
			animComponent.ResetTrigger(attackHash);
		}
	}


	// Función para resetear el Player
	public void reset()
	{
		//Reiniciar el numero de vidas
		_lifes = 3;
		UIManager.instance.hideEndPanel();

		// Pausamos a Player
		paused = true;

		// Forzar estado Idle en las dos capas (Base Layer y Attack Layer): función Play() de Animator
		animComponent.Play("Idle",0);
		animComponent.Play("Idle", 1);

		// Reseteo todos los triggers (Attack y Dead)
		animComponent.ResetTrigger(attackHash);
		animComponent.ResetTrigger(deadHash);

		// Posicionar el jugador en el (0,0,0) y rotación nula (Quaternion.identity)
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
	}

	// Funcion recibir daño
	public void receiveDamage()
	{
		// Restar una vida
		_lifes--;
		Debug.Log("Vidas: " + _lifes);

		// Si no me quedan vidas notificar al GameManager (notifyPlayerDead) y disparar trigger "Dead"
		if (_lifes == 0)
		{
			animComponent.SetTrigger(deadHash);
			source.clip = deadAudio;
			source.PlayDelayed(1.0f);

			//NOTIFICAR FIN DE JUEGO
			GameManager.instance.notifyPlayerDead();
		}

		// Si aun me quedan vidas dispara el trigger TakeDamage
		if (_lifes > 0)
		{
			animComponent.SetTrigger(damageHash);
			source.clip = damageAudio;
			source.PlayDelayed(0.5f);
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		singleAttack = true;
	}
	private void OnCollisionStay(Collision collision)
	{
		// Si el estado es 'Attack' matamos al enemigo (mirar etiqueta)
		if (singleAttack && animComponent.GetCurrentAnimatorStateInfo(1).IsName("Attack") && animComponent.GetFloat(distanceHash) > 0 && collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.GetComponent<SkeletonBehaviour>().kill();
			singleAttack = false;
		}


	}
}
