using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeManager : MonoBehaviour {


	public TextMeshProUGUI sentenceText;
	public Animator anim;
	private Queue<string> sentences;
	private bool dialogueOn = false;
	private Dialouge d;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string>();
	}

	void Update() {
		if (dialogueOn) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				DisplayNextSentence(d.triggerAfter);
			}
		}

	}

	public void StartDialouge(Dialouge dialouge) {

		d = dialouge;
		if(anim == null) 
			if(GameObject.Find("DialogueBox") != null)
				anim = GameObject.Find("DialogueBox").GetComponent<Animator>();
		
		anim.SetBool("isDialougeOpen", true);
		dialogueOn = true;
		sentences.Clear();

		foreach (string sentence in dialouge.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence(dialouge.triggerAfter);
	}

	public void DisplayNextSentence(UnityEngine.Events.UnityEvent e) {
		if (sentences.Count == 0) {
			EndDialouge();
			dialogueOn = false;
			e.Invoke();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(typeSentence(sentence));
	}

	IEnumerator typeSentence(string sentence) {
		sentenceText.text = "";
		foreach (char letter in sentence.ToCharArray()) {
			sentenceText.text += letter;
			yield return new WaitForSeconds(.02f);
		}
	}

	void EndDialouge() {
		anim.SetBool("isDialougeOpen", false);
		//Trigger what's after
	}


}
