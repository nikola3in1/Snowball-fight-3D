using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour {

	// The map we're building is going to look like:
	//
	//	LIST OF USERS -> A User -> LIST OF SCORES for that user
	//

	Dictionary< string, Dictionary<string, float> > playerScores;
	string nick;
	public bool spawned;
	int changeCounter = 0;
	float deaths = 0.5f;

	void Start(){
		
	}

	[PunRPC]
	void test(string newString){
		nick = newString;
		ChangeScore (nick, "kills", 1);
	}

	[PunRPC]
	void reciever(string newString){
		nick = newString;
		SetScore (nick, "kills", 0);
		SetScore (nick, "deaths", 0);
		spawned = true;
	}



	[PunRPC]
	void deathsUp(string newString){
		nick = newString;
		ChangeScore (nick, "deaths", deaths);
	}

	[PunRPC]
	void playerLeft(string newString){
		nick = newString;
		if (playerScores.ContainsKey (nick) == true)
			playerScores.Remove (nick);
	}
	[PunRPC]
	void resetScore(string newString){
		nick = newString;
		SetScore (nick, "kills", 0);
		SetScore (nick, "deaths", 0);
		
	}

	void Init() {
		if(playerScores != null)
			return;

		playerScores = new Dictionary<string, Dictionary<string, float>>();
	}

	public void Reset() {
		changeCounter++;
		playerScores = null;
	}

	public float GetScore(string username, string scoreType) {
		Init ();

		if(playerScores.ContainsKey(username) == false) {
			// We have no score record at all for this username
			return 0;
		}

		if(playerScores[username].ContainsKey(scoreType) == false) {
			return 0;
		}

		return playerScores[username][scoreType];
	}

	public void SetScore(string username, string scoreType, float value) {
		Init ();

		changeCounter++;

		if(playerScores.ContainsKey(username) == false) {
			playerScores[username] = new Dictionary<string, float>();
		}

		playerScores[username][scoreType] = value;
	}

	public void ChangeScore(string username, string scoreType, float amount) {
		Init ();
		float currScore = GetScore(username, scoreType);
		SetScore(username, scoreType, currScore + amount);
	}

	public string[] GetPlayerNames() {
		Init ();
		return playerScores.Keys.ToArray();
	}
	
	public string[] GetPlayerNames(string sortingScoreType) {
		Init ();

		return playerScores.Keys.OrderByDescending( n => GetScore(n, sortingScoreType) ).ToArray();
	}

	public float GetChangeCounter() {
		return changeCounter;
	}

	public void DEBUG_ADD_KILL_TO_QUILL() {
		ChangeScore("quill18", "kills", 1);
	}
	
	public void DEBUG_INITIAL_SETUP() {
		SetScore("quill18", "kills", 0);
		SetScore("quill18", "assists", 345);
		
		SetScore("bob", "kills", 1000);
		SetScore("bob", "deaths", 14345);
		
		SetScore("AAAAAA", "kills", 3);
		SetScore("BBBBBB", "kills", 2);
		SetScore("CCCCCC", "kills", 1);
		
		
		Debug.Log (  GetScore("quill18", "kills") );
	}



}
