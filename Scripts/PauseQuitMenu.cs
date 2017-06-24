using UnityEngine;
using System.Collections;

public class PauseQuitMenu : Photon.MonoBehaviour {

	Health h;
	PauseMenu pauseMenu;

	void Start(){
		h = gameObject.GetComponent<Health> ();
		pauseMenu = GameObject.FindGameObjectWithTag ("MyPlayer").GetComponent<PauseMenu>();
	}

	public void QuitGame(){
		pauseMenu.QuitGame ();
	}

	public void BackToMainMenu(){
		pauseMenu.BackToMainMenu ();
		
	}
}
