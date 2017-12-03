using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
	// Definir Hashes de:
	// Parametros (Speed, Attack, Damage, Dead)
	// Estados (Base Layer.Idle, Attack Layer.Idle, Attack Layer.Attack)
	// TODO

	public float walkSpeed		= 1;		// Parametro que define la velocidad de "caminar"
	public float runSpeed		= 1;		// Parametro que define la velocidad de "correr"
	public float rotateSpeed	= 160;		// Parametro que define la velocidad de "girar"

	// Variables auxiliares
	float _angularSpeed			= 0;		// Velocidad de giro actual
	float _speed				= 0;		// Velocidad de traslacion actual
	float _originalColliderZ	= 0;		// Valora original de la posición 'z' del collider

	// Variables internas:
	int _lives = 3;							// Vidas restantes
	public bool paused = false;				// Indica si el player esta pausado (congelado). Que no responde al Input

	void Start()
	{
		// Obtener los componentes Animator, Rigidbody y el valor original center.z del BoxCollider
		// TODO
	}

	// Aqui moveremos y giraremos la araña en funcion del Input
	void FixedUpdate()
	{
		// Si estoy en pausa no hacer nada (no moverme ni atacar)
		if (paused) return;

		// Calculo de velocidad lineal (_speed) y angular (_angularSpeed) en función del Input
		// Si camino/corro hacia delante delante: _speed = walkSpeed   /  _speed = runSpeed
		// TODO

		// Si camino/corro hacia delante detras: _speed = -walkSpeed   /  _speed = -runSpeed
		// TODO

		// Si no me muevo: _speed = 0
		// TODO

		// Si giro izquierda: _angularSpeed = -rotateSpeed;
		// TODO

		// Si giro derecha: _angularSpeed = rotateSpeed;
		// TODO

		// Si no giro : _angularSpeed = 0;
		// TODO

		// Actualizamos el parámetro "Speed" en función de _speed. Para activar la anicación de caminar/correr
		// TODO

		// Movemov y rotamos el rigidbody (MovePosition y MoveRotation) en función de "_speed" y "_angularSpeed"
		// TODO

		// Mover el collider en función del parámetro "Distance" (necesario cuando atacamos)
		// TODO
	}

	// En este bucle solamente comprobaremos si el Input nos indica "atacar" y activaremos el trigger "Attack"
	private void Update()
	{
		// Si estoy en pausa no hacer nada (no moverme ni atacar)
		// TODO

		// Si detecto Input tecla/boton ataque ==> Activo disparados 'Attack'
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
	public void recieveDamage()
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
