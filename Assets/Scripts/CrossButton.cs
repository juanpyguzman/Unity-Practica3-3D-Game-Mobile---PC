using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum InputType {NONE, UP, DOWN, LEFT, RIGHT}

public class CrossButton : UIBehaviour
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

	public void update(bool pushed)
	{
		changeColor(pushed);

		if(pushed)
		{
			if (input1 == InputType.NONE ) input1 = input;
			else if(input2 == InputType.NONE) input2 = input;
		}
		else
		{
			if (input1 == input) input1 = InputType.NONE;
			else if (input2 == input) input2 = InputType.NONE;
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