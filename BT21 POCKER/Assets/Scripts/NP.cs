using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NP : MonoBehaviour {
	public List<Card> hascard;
	public float angle;
	public bool isplayer;
	public Game agame;
	public bool checkfinish;
	public NP who;
	public Card PCard;
	public List<Card> clearcard;

	public GameObject turnarrow;
	Card x;
	Card y;
	float time;
	int s;
	public GameObject pickedcardpos;
	// Use this for initialization
	public virtual void Start () {
		for (int i = 0; i < hascard.Count; i++) {
			hascard[i].GetComponent<RectTransform> ().rotation = Quaternion.Euler (0, 0, angle);

			if (isplayer)
				hascard [i].GetComponent<Image> ().sprite = hascard [i].Icon;
			
		}
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if (!checkfinish) 
			checkcard ();
			
	}
	void checksamecard(int a){
		for (int i = 0; i < hascard.Count; i++) {
			if (hascard [a].classint != hascard [i].classint && hascard [a].id == hascard [i].id&&hascard[a].id!=14) {
				if (!hascard [i].ischecked) {
					hascard [a].GetComponent<Image> ().color = new Color32 (220, 200, 220, 255);
					hascard [a].name = hascard [a].id+"";
					hascard [a].ischecked = true;
					x = hascard [a];
					hascard [i].GetComponent<Image> ().color = new Color32 (220, 200, 220, 255);
					hascard [i].name = hascard [i].id+"";
					hascard [i].ischecked = true;
					y =  hascard [i];
				}
				break;
			}

		}
	
	}

	void checkcard(){
		time += Time.deltaTime;
		if (time > 1) {
			if (s < hascard.Count) {
				checking (s);
			} else if (s > hascard.Count) {
				descard ();
				agame.allint += 1;
				checkfinish = true;
			}
			s += 1;
			time = 0;
		}
	}
	public void checking(int z){
		checksamecard (z);
		if (x != null) {
			clearcard.Add (x);
			hascard.Remove (x);
		}
		if (y != null) {
			clearcard.Add (y);
			hascard.Remove (y);
		}


	}
	public void descard(){
		for (int i = 0; i < clearcard.Count; i++) {
			if (clearcard [i] != null) {
				RectTransform pickedcard = clearcard [i].gameObject.GetComponent<RectTransform> ();
				pickedcard.gameObject.transform.SetParent (pickedcardpos.transform);
				pickedcard.anchoredPosition = new Vector2 (120, -50);
				float randomangle = Random.Range (0, 360);
				pickedcard.rotation = Quaternion.Euler (0, 0, randomangle);
				pickedcard.GetComponent<Image> ().color = Color.white;
				pickedcard.GetComponent<Image> ().sprite = clearcard [i].Icon;

			}
		}
		clearcard = new List<Card> ();
	}

	public virtual void doPick(float a){
		
		if (hascard.Count == 0 && !agame.Stopgame) {
			agame.ranking (this);
            if (agame.ranklist.Count < 2)
                who.doPick(1);
        }
	}
	public IEnumerator RemoveandAdd(float t){
		yield return new WaitForSeconds (t);
		PCard.gameObject.transform.SetParent (gameObject.transform);
		hascard.Add (PCard);
		PCard.GetComponent<RectTransform> ().rotation = Quaternion.Euler (0, 0, angle);
		PCard.GetComponent<Image> ().color = Color.white;
		if (!isplayer)
			PCard.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Pocker Cards/BACK");
		else
			PCard.GetComponent<Image> ().sprite = Resources.Load<Sprite> ("Sprites/Pocker Cards/"+ PCard.id + "" + PCard.classint);
		StartCoroutine (Pickcheck (1));
	}
	IEnumerator Pickcheck(float t){
		yield return new WaitForSeconds (t);
		checking (hascard.Count-1);
		StartCoroutine (pickdes (1f));
	}
	IEnumerator pickdes(float t){
		yield return new WaitForSeconds (t);
		descard ();
		turnarrow.SetActive (false);
		if(hascard.Count==0)
			agame.ranking (this);
		else if(agame.ranklist.Count<2)
            who.doPick(1);
    }
}
