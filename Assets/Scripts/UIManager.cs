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
	public GameObject mainMenu			= null; // Panel del menu principal (Primera pantalla en mostrarse)
	public GameObject HUD = null;

	// Sub-menus durante el juego
	public FinalPanelBehaviour endPanel	= null;	// Panel de fin de juego (Dentro de la interfaz del juego)
	public Text scoreText				= null; // Puntuacion del juego

	static int appearHash = Animator.StringToHash("Appear");
	static int resetHash = Animator.StringToHash("Reset");

	public void showMainMenu()
	{
		// Mostrar objeto mainMenu
		mainMenu.SetActive(true);

		// Ocultar endPanel
		// TODO
	}

	public void hideMainMenu()
	{
		// Ocultar objeto mainMenu
		mainMenu.SetActive(false);
	}

	public void showHUD()
	{
		// Mostrar objeto mainMenu
		HUD.SetActive(true);

		// Ocultar endPanel
		// TODO
	}

	public void hideHUD()
	{
		// Ocultar objeto mainMenu
		HUD.SetActive(false);
	}

	public void showEndPanel(bool win)
	{
		endPanel.gameObject.SetActive(true);
		if (win)
		{
			endPanel.gameObject.GetComponentInChildren<Text>().text = "¡Misión cumplida!";
		}

		if (!win)
		{
			endPanel.gameObject.GetComponentInChildren<Text>().text = "¡Has sido derrotado!";
		}
		endPanel.GetComponent<Animator>().SetTrigger(appearHash);
	}

	public void hideEndPanel()
	{
		// Ocultar el panel
		endPanel.gameObject.SetActive(false);
	}

	public void updateScore(int score)
	{
		// Actualizar el 'UI text' con la puntuacion 
		scoreText.text = score.ToString();
	}

}
