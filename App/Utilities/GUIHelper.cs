using UnityEngine;
using System.Collections;

/// <summary>
/// GUI helper.
/// </summary>
public class GUIHelper {

	/// <summary>
	/// If this is called from within a GUI.BeginGroup block, it will return the expected relative
	/// coordinates comparison to test if the mouse is being held over.
	/// </summary>
	/// <returns><c>true</c>, if is over was moused, <c>false</c> otherwise.</returns>
	/// <param name="rect">Rect.</param>
	public static bool MouseIsOver(Rect rect) {
		// http://answers.unity3d.com/questions/139686/how-to-check-if-player-is-hovering-mouse-over-a-gu.html
		return rect.Contains(Event.current.mousePosition);
	}
}
