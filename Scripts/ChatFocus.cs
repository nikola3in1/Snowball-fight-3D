using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChatFocus : MonoBehaviour {
	public InputField input;
	public GameObject pausemenu;
	private Text da;
	 GameObject player;
	Vector3 chatPlacement;
	Vector3 currPosition;
	// Update is called once per frame
	void Start(){
		player = GameObject.FindGameObjectWithTag ("MyPlayer");	
	}

	void Update () {
		 if(pausemenu.activeSelf == false){
			if (Input.GetKeyDown (KeyCode.Y) && input.text == "" || Input.GetKeyDown (KeyCode.Y) && input.text == "Press y to chat") {
				input.text = "";
				EventSystem.current.SetSelectedGameObject (input.gameObject, null);
				if(input != null)
				input.OnPointerClick (null);


				if (Input.GetKey (KeyCode.Escape)) {
					return;
				}
			}
		}if (pausemenu.activeSelf == true) {
			if (Input.GetKeyDown (KeyCode.Y) && input.text == "" || Input.GetKeyDown (KeyCode.Y) && input.text == "Press y to chat") {
				input.text = "";
				EventSystem.current.SetSelectedGameObject (input.gameObject, null);
				input.OnPointerClick (null);

				if (Input.GetKey (KeyCode.Escape)) {
					return;
				}

			}
		}
	}
	public void endEdit(){
		
		input.text = "Press y to chat";
		EventSystem.current.SetSelectedGameObject (null);
	}
}	
