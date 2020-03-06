using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum InputType {NONE, UP, DOWN, LEFT, RIGHT, A, R}

public class CrossARButton : UIBehaviour
{
	#region STATIC
	static InputType input1 = InputType.NONE;
	static InputType input2 = InputType.NONE;
	public static bool GetInput(InputType input)
	{
		return input1 == input || input2 == input;
	}
	#endregion

	Image image;
	public InputType input;

	public GameObject Player = null;

	public void update(bool pushed)
	{
		changeColor(pushed);

		if(pushed)
		{
			//Indicamos a PlayerBehaviour que se están usando los botones de pantalla
			// para deshabilitar los Input de teclado o mando
			Player.GetComponent<PlayerBehaviour>().usingScreen = true;

			if (input1 == InputType.NONE ) input1 = input;
			else if(input2 == InputType.NONE) input2 = input;
			
			if (input1 == InputType.UP)
			{
				Player.GetComponent<PlayerBehaviour>().verticalAxis = 1.0f;
			}

			if (input1 == InputType.DOWN)
			{
				Player.GetComponent<PlayerBehaviour>().verticalAxis = -1.0f;
			}

			if (input1 == InputType.RIGHT)
			{
				Player.GetComponent<PlayerBehaviour>().horizontalAxis = 1.0f;
			}

			if (input1 == InputType.LEFT)
			{
				Player.GetComponent<PlayerBehaviour>().horizontalAxis = -1.0f;
			}

			if (input1 == InputType.A)
			{
				Player.GetComponent<PlayerBehaviour>().attack = true;
			}

			if (input1 == InputType.R)
			{
				Player.GetComponent<PlayerBehaviour>().verticalAxis *= Player.GetComponent<PlayerBehaviour>().runSpeed;
			}
		}
		else
		{
			if (input1 == input) input1 = InputType.NONE;
			else if (input2 == input) input2 = InputType.NONE;

			if (input1 == InputType.NONE)
			{
				Player.GetComponent<PlayerBehaviour>().verticalAxis = 0.0f;
				Player.GetComponent<PlayerBehaviour>().horizontalAxis = 0.0f;
				Player.GetComponent<PlayerBehaviour>().attack = false;

				//Indicamos a PlayerBehaviour que no se están usando los botones de pantalla
				// para habilitar los Input de teclado o mando
				Player.GetComponent<PlayerBehaviour>().usingScreen = false;
			}
		}
	}

	void changeColor(bool pushed)
	{
		image.color = pushed ? new Color(0, 0, 0, 1) : new Color(0, 0, 0, 0.3f);
	}

	protected override void Start ()
	{
		image = GetComponent<Image>();
	}
}