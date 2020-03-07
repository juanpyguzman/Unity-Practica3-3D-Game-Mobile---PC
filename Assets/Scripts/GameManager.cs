using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region SINGLETON
	protected static GameManager _instance = null;
	public static GameManager instance { get { return _instance; } }
	void Awake () { _instance = this; }
	#endregion

	// Punteros a player y a todos los enemigos (lista 'enemiesList')
	public PlayerBehaviour player = null;
	public List<SkeletonBehaviour> enemiesList = null;	// No requiere inicializacion, se rellena desde el Inspector

	// Lista con los enemigos que quedan vivos
	List<SkeletonBehaviour> currentEnemiesList = null;

	// Variables internas
	int _score = 0;
	public bool soundEnabled = true;


	void Start ()
	{
		currentEnemiesList = new List<SkeletonBehaviour>();

		UIManager.instance.hideHUD();
		// Reiniciamos el juego

		reset();
		// TODO
	}

	private void reset()		// Funcion para reiniciar el juego
	{
		// Reiniciamos a Player
		player.GetComponent<PlayerBehaviour>().reset();

		// Incializamos la puntuacion a cero
		_score = 0;

		// Rellenamos la lista de enemigos actual.
		currentEnemiesList.Clear();
		foreach (SkeletonBehaviour skeleton in enemiesList)
		{
			skeleton.setPlayer(player);
			skeleton.reset();

			currentEnemiesList.Add(skeleton);
		}
	}

	#region UI EVENTS
	// Evento al pulsar boton 'Start'
	public void onStartGameButton()
	{
		// Ocultamos el menu principal (UIManager)
		UIManager.instance.hideMainMenu();
		UIManager.instance.showHUD();

		// Actualizamos la puntuacion en el panel Score (UIManager)
		UIManager.instance.updateScore(_score);

		// Quitamos la pausa a Player
		player.GetComponent<PlayerBehaviour>().paused = false;
	}

	// Evento al pulsar boton 'Exit'
	public void onExitGameButton()
	{
		// Mostramos el panel principal
		UIManager.instance.showMainMenu();
		UIManager.instance.hideHUD();

		// Reseteamos el juego
		reset();
	}
	#endregion

	#region GAME EVENTS
	// Evento al ser notificado por un enemigo (cuando muere)
	public void notifyEnemyKilled(SkeletonBehaviour enemy)
	{
		// Eliminamos enemigo de la lista actual
		currentEnemiesList.Remove(enemy);

		// Subimos 10 puntos y actualizamos la puntuacion en la UI
		_score += 10;
		UIManager.instance.updateScore(_score);


		// Si no quedan enemmigos
		if (currentEnemiesList.Count == 0)	// KEEP
		{
			// Mostrar panel de 'Misión cumplida' y pausar a Player
			Debug.Log("Misión cumplida");
			UIManager.instance.showEndPanel(true);
			// TODO
		}
	}

	// Evento al ser notificado por player (cuando muere)
	public void notifyPlayerDead()
	{
		// Mostrar panel de 'Mision fallida' y pausar a Player
		UIManager.instance.showEndPanel(false);
		Debug.Log("GAME OVER");
	}
	#endregion
}
