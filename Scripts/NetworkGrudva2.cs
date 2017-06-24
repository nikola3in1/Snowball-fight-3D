using UnityEngine;
using System.Collections;

public class NetworkGrudva2 : Photon.MonoBehaviour {

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	Vector3 realPositionR = Vector3.zero;
	Vector3 realVelocityR = Vector3.zero;
	Quaternion realRotationR = Quaternion.identity;

	Vector3 realVelocityRA = Vector3.zero;

	void Start(){
		gameObject.GetComponent<Rigidbody> ();
	}	
	
	public void Update () {

		if (!photonView.isMine) 
		{
			///gameObject.GetComponent<PhotonView> ().RPC ("Bacanje", PhotonTargets.All);
			transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
		}
	}


	public void FixedUpdate()
	{
		if (!photonView.isMine) 
		{
			GetComponent<Rigidbody>().position = Vector3.Lerp (GetComponent<Rigidbody>().position, realPositionR, 0.1f);
			GetComponent<Rigidbody>().velocity = Vector3.Lerp (GetComponent<Rigidbody>().velocity, realVelocityR, 0.1f);
			GetComponent<Rigidbody>().rotation = Quaternion.Lerp (GetComponent<Rigidbody>().rotation, realRotationR, 0.1f);
			GetComponent<Rigidbody>().angularVelocity = Vector3.Lerp (GetComponent<Rigidbody>().angularVelocity, realVelocityRA, 0.1f);
		}	
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);

			stream.SendNext (GetComponent<Rigidbody>().position);
			stream.SendNext (GetComponent<Rigidbody>().rotation);
			stream.SendNext (GetComponent<Rigidbody>().velocity);

			stream.SendNext (GetComponent<Rigidbody>().angularVelocity);
		}
		else
		{
			realPosition = (Vector3)stream.ReceiveNext ();
			realRotation = (Quaternion)stream.ReceiveNext ();

			realPositionR = (Vector3)stream.ReceiveNext ();
			realRotationR = (Quaternion)stream.ReceiveNext ();
			realVelocityR = (Vector3)stream.ReceiveNext ();

			realVelocityRA = (Vector3)stream.ReceiveNext ();
		}
	}
		
}
