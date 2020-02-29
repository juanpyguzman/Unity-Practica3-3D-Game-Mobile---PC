using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
	// Definir Hashes de:
	// Parametros (Attack, Dead, Distance)
	static int attackHash = Animator.StringToHash("Attack");
	static int deadHash = Animator.StringToHash("Dead");

	public Animator animComponent = null;
	public BoxCollider box;

	// Estados (Attack, Idle)
	// TODO

	// Variables auxiliares 
	PlayerBehaviour _player		= null;     //Puntero a Player (establecido por método 'setPlayer')
	bool _dead					= false;	// Indica si ya he sido eliminado
	float _originalColliderZ	= 0;        // Valora original de la posición 'z' del collider
	float _timeToAttack			= 0;        // Periodo de ataque
	float original_z;

	public void setPlayer(PlayerBehaviour player)
	{
		_player = player;
	}

	void Start ()
	{
		animComponent = GetComponent<Animator>();
		box = GetComponent<BoxCollider>();
		original_z = box.center.z;
	}
	
	void FixedUpdate ()
	{
		// Si estoy muerto ==> No hago nada
		// TODO

		// Si Player esta a menos de 1m de mi y no estoy muerto:
		// - Le miro
		// - Si ha pasado 1s o más desde el ultimo ataque ==> attack()
		// TODO

		// Desplazar el collider en 'z' un multiplo del parametro Distance
		box.center = new Vector3(box.center.x, box.center.y, original_z + animComponent.GetFloat("Distance") * 0.3f);
	}

	public void attack()
	{
		// Activo el trigger "Attack"
		// TODO
	}

	public void kill()
	{
		// Guardo que estoy muerto, disparo trigger "Dead" y desactivo el collider
		// TODO

		// Notifico al GameManager que he sido eliminado
		// TODO
	}

	// Funcion para resetear el collider (activado por defecto), la variable donde almaceno si he muerto y forzar el estado "Idle" en Animator
	public void reset()
	{
		// TODO
	}

	private void OnCollisionEnter(Collision collision)
	{
		// Obtener el estado actual
		// TODO

		// Si el estado es 'Attack' y el parametro Distance es > 0 atacamos a Player (comprobar etiqueta).
		// La Distancia >0 es para acotar el ataque sólo al momento que mueve la espada (no toda la animación).
		// TODO
	}
}
