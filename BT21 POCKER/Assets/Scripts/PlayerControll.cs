using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerControll : NP {
	public GameObject pickarrow;
	bool isPlayerturn;
	public int w;

	// Use this for initialization
	public override void Start ()
	{
		base.Start ();
	}
	public override void Update ()
	{
		base.Update ();
		if (isPlayerturn) {
			if (Input.GetKeyDown(KeyCode.A)) {
				w += 1;
				if (w == who.hascard.Count)
					w = 0;
			}
            if (agame.ranklist.Count < 2)
                PlayerTurn();
        }

	}
	public override void doPick (float a)
	{
		base.doPick (a);
		if (isplayer)
			isPlayerturn = true;
		
	}
	void PlayerTurn(){
		turnarrow.SetActive (true);
		pickarrow.SetActive (true);
		pickarrow.transform.SetParent (who.hascard [w].gameObject.transform);
		pickarrow.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (-75, -250);
		PCard = who.hascard [w];
	}	
	public void PickCard(){
		if (isPlayerturn) {
			pickarrow.SetActive (false);
			pickarrow.transform.SetParent (gameObject.transform);
			who.hascard [w].GetComponent<Image> ().color = new Color32 (255, 150, 200, 255);
			who.hascard.Remove (PCard);
			StartCoroutine (RemoveandAdd (1));
			isPlayerturn = false;
			w = 0;
		}
	}


}
