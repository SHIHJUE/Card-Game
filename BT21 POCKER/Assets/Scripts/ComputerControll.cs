using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ComputerControll : NP {
	public override void Start ()
	{
		base.Start ();
	}
	public override void Update ()
	{
		base.Update ();
	}
	public override void doPick (float a)
	{
		base.doPick (a);
		if (hascard.Count != 0 && !agame.Stopgame) {
			StartCoroutine (PickCard (a));
			turnarrow.SetActive (true);
		}
	}
	IEnumerator PickCard(float t){
		yield return new WaitForSeconds (t);
		int n = who.hascard.Count;
		int x = Random.Range (0, n);
		who.hascard [x].GetComponent<Image> ().color = new Color32 (255, 150, 200, 255);
		PCard = who.hascard [x];
		who.hascard.Remove (PCard);
		StartCoroutine (RemoveandAdd (1));

	}

}
