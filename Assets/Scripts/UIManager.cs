using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	#region SINGLETON
	protected static UIManager _instance = null;
	public static UIManager instance { get { return _instance; } }
	void Awake() { _instance = this; }
	#endregion

	// Menu principal
	public GameObject mainMenu			= null;	// Panel del menu principal (Primera pantalla en mostrarse)

	// Sub-menus durante el juego
	public FinalPanelBehaviour endPanel	= null;	// Panel de fin de juego (Dentro de la interfaz del juego)
	public Text scoreText				= null;	// Puntuacion del juego


	public void showMainMenu()
	{
		// Mostrar objeto mainMenu
		// TODO

		// Ocultar endPanel
		// TODO
	}

	public void hideMainMenu()
	{
		// Ocultar objeto mainMenu
		// TODO
	}

	public void showEndPanel(bool win)
	{
		// Mostrar panel fin de juego (ganar o perder)
		// TODO
	}

	public void hideEndPanel()
	{
		// Ocultar el panel
		// TODO
	}

	public void updateScore(int score)
	{
		// Actualizar el 'UI text' con la puntuacion 
		// TODO
	}

}
