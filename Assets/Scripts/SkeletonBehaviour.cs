using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehaviour : MonoBehaviour
{
	// Definir Hashes de:
	// Parametros (Attack, Dead, Distance)
	static int attackHash = Animator.StringToHash("Attack");
	static int deadHash = Animator.StringToHash("Dead");
	static int distanceHash = Animator.StringToHash("Distance");

	public Animator animComponent = null;
	public BoxCollider box;

	//Variable bool para limitar a 1 ataque cada vez que se realiza la colisión en modo ataque
	private bool singleAttack;

	// Estados (Attack, Idle)
	// TODO

	// Variables auxiliares 
	PlayerBehaviour _player		= null;     //Puntero a Player (establecido por método 'setPlayer')
	bool _dead					= false;	// Indica si ya he sido eliminado
	float _originalColliderZ	= 0;        // Valora original de la posición 'z' del collider
	float _timeToAttack			= 1.0f;        // Periodo de ataque
	float original_z;

	private float lastTimeAttack = 0.0f;

	public void setPlayer(PlayerBehaviour player)
	{
		_player = player;
	}

	void Start ()
	{
		animComponent = GetComponent<Animator>();
		box = GetComponent<BoxCollider>();
		original_z = box.center.z;
		setPlayer(GameObject.Find("Player").GetComponent<PlayerBehaviour>());
	}
	
	void FixedUpdate ()
	{
		// Si estoy muerto ==> No hago nada
		if(!animComponent.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
		{
			if (((_player.transform.position-transform.position).sqrMagnitude<1))
				{
					// Si Player esta a menos de 1m de mi y no estoy muerto:
					// - Le miro
					transform.LookAt(_player.transform);

					// - Si ha pasado 1s o más desde el ultimo ataque ==> attack()
					if (Time.time - lastTimeAttack >= _timeToAttack)
						{
							attack();
							lastTimeAttack = Time.time;
						}
				}
		}

		// Desplazar el collider en 'z' un multiplo del parametro Distance
		box.center = new Vector3(box.center.x, box.center.y, original_z + animComponent.GetFloat("Distance") * 0.3f);
	}

	public void attack()
	{
		// Activo el trigger "Attack"
		animComponent.SetTrigger(attackHash);
	}

	public void kill()
	{
		// Guardo que estoy muerto, disparo trigger "Dead" y desactivo el collider
		// TODO
		animComponent.SetTrigger(deadHash);
		gameObject.GetComponent<Collider>().enabled=false;
		Debug.Log("Enemigo eliminado");

		// Notifico al GameManager que he sido eliminado
		GameManager.instance.notifyEnemyKilled(this.GetComponent<SkeletonBehaviour>());
	}

	// Funcion para resetear el collider (activado por defecto), la variable donde almaceno si he muerto y forzar el estado "Idle" en Animator
	public void reset()
	{
		// TODO
	}

	private void OnCollisionEnter(Collision collision)
	{
		singleAttack = true;
	}

	private void OnCollisionStay(Collision collision)
	{
		// Si el estado es 'Attack' y el parametro Distance es > 0 atacamos a Player (comprobar etiqueta).
		// La Distancia >0 es para acotar el ataque sólo al momento que mueve la espada (no toda la animación).
		if (singleAttack && animComponent.GetCurrentAnimatorStateInfo(0).IsName("Attack") && animComponent.GetFloat(distanceHash) > 0 && collision.gameObject.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerBehaviour>().receiveDamage();
			singleAttack = false;
		}


	}
}
