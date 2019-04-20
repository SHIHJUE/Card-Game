using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game : MonoBehaviour {
	public bool Stopgame;
	public List <Card> cards;
	public Card acard;
	public List<NP> nps;
	public bool allfinish;

	public int allint;

	public NP Whoplay;

	public GameObject Desimage;
	public List<string> ranklist;

	int x;
	public Text ranktxt;
	// Use this for initialization
	void Start () {
		
		setcardtonp ();
		x = Random.Range (0, 3);
		Whoplay = nps [x];
	}

	// Update is called once per frame
	void Update () {
		if (allint == 4) {
			StartGame ();
		}
		if (ranklist.Count < 2)
			return;
		else if(ranklist.Count == 2) {
			finrank ();
		}
	}
	void setcardtonp(){
		for (int i = 0; i < 14; i++) {
			for (int j = 0; j < 4; j++) {
				Card ac = Instantiate (acard);
				ac.id = i+1;
				ac.classint = j+1;
				ac.Icon = Resources.Load<Sprite> ("Sprites/Pocker Cards/"+ ac.id + "" + ac.classint);
				cards.Add (ac);
			}
		}
		for (int i = 0; i < 14; i++) {
			for (int j = 0; j < 4; j++) {
				int x = Random.Range (0, cards.Count);
				nps [j].GetComponent<NP>().hascard.Add (cards [x].GetComponent<Card>());
				cards [x].gameObject.transform.SetParent (nps[j].transform);
				cards.Remove (cards [x]);
			}
		}

	}
	void StartGame(){
		Desimage.SetActive (false);
		allfinish = true;
		allint = 0;
		setwho ();
		Whoplay.doPick (1);

	}
	public void setwho(){
		for (int i = 0; i < nps.Count; i++) {
			if(i!=nps.Count-1)
				nps [i].who = nps [i + 1];
			else
				nps [i].who = nps [0];
		}

	}
	public void ranking(NP NPcleared){
		ranklist.Add (NPcleared.name);
		nps.Remove (NPcleared);
		setwho ();
	}
	void finrank(){
		Stopgame = true;
		ranktxt.gameObject.SetActive (true);
		if(nps [0].hascard.Count==nps [1].hascard.Count){
			ranklist.Add (nps [0].name+" and "+ nps [1].name);
			nps = new List<NP> ();
		}
		else {
			for (int i = 0; i < nps.Count; i++) {
				if (nps [i].hascard.Count == 3) {
					ranklist.Add (nps [i].name);
					nps.Remove (nps [i]);
				}
			}
			ranklist.Add (nps [0].name);
		}

		for (int r = 0; r < ranklist.Count; r++) {
			int w = r + 1;
			ranktxt.text += w + " : " + ranklist [r] + "\n";
		}
	}
}
