using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

	// Definir Hashes de:
	// Parametros (Speed, Attack, Damage, Dead)
	static int speedHash = Animator.StringToHash("Speed");
	static int attackHash = Animator.StringToHash("Attack");
	static int damageHash = Animator.StringToHash("Damage");
	static int deadHash = Animator.StringToHash("Dead");

	public Animator animComponent = null;
	public Rigidbody rigidBody = null;
	public BoxCollider box;

	public bool usingScreen;

	// Estados (Base Layer.Idle, Attack Layer.Idle, Attack Layer.Attack)
	// TODO

	public float walkSpeed = 1;     // Parametro que define la velocidad de "caminar"
	public float runSpeed = 2;      // Parametro que define la velocidad de "correr"
	public float rotateSpeed = 0.4f;        // Parametro que define la velocidad de "girar"

	// Variables auxiliares
	float _angularSpeed = 0;        // Velocidad de giro actual
	float _speed = 0;       // Velocidad de traslacion actual
	float _originalColliderZ = 0;        // Valora original de la posición 'z' del collider
	public float verticalAxis;
	public float horizontalAxis;
	float original_z;

	// Variables internas:
	int _lives = 3;                         // Vidas restantes
	public bool paused = false;             // Indica si el player esta pausado (congelado). Que no responde al Input
	public bool attack = false;

	void Start()
	{
		// Obtener los componentes Animator, Rigidbody y el valor original center.z del BoxCollider
		animComponent = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody>();
		box = GetComponent<BoxCollider>();
		original_z = box.center.z;
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


		// Mover el collider en función del parámetro "Distance" (necesario cuando atacamos)
		box.center = new Vector3(box.center.x, box.center.y, original_z + animComponent.GetFloat("Distance") * 10.0f);
	}

	// En este bucle solamente comprobaremos si el Input nos indica "atacar" y activaremos el trigger "Attack"
	private void Update()
	{
		// Si estoy en pausa no hacer nada (no moverme ni atacar)
		// TODO

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
		// TODO

		// Pausamos a Player
		// TODO

		// Forzar estado Idle en las dos capas (Base Layer y Attack Layer): función Play() de Animator
		// TODO

		// Reseteo todos los triggers (Attack y Dead)
		// TODO

		// Posicionar el jugador en el (0,0,0) y rotación nula (Quaternion.identity)
		// TODO
	}

	// Funcion recibir daño
	public void receiveDamage()
	{
		// Restar una vida
		// Si no me quedan vidas notificar al GameManager (notifyPlayerDead) y disparar trigger "Dead"
		// TODO

		// Si aun me quedan vidas dispara el trigger TakeDamage
		// TODO
	}

	private void OnCollisionEnter(Collision collision)
	{
		// Obtener estado actual de la capa Attack Layer
		// TODO

		// Si el estado es 'Attack' matamos al enemigo (mirar etiqueta)
		// TODO
	}
}
