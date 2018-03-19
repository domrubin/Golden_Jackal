using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class testScramble : MonoBehaviour {
    public string currentText;
    public string goalText;
    public AudioSource sound;
    public char[] alphabet;
    public int stringLength;
    public int currentTextLength;


    public Font englishFont;
    public Font heiro;
    public Material normalMat;
    TextMeshProUGUI myText;
    GameObject englishWords;

	// Use this for initialization
	void Start () {
        currentText = GetComponent<TextMeshProUGUI>().text;
        //playText = "Play";
        stringLength = goalText.Length;
        currentTextLength = GetComponent<TextMeshProUGUI>().text.Length;
        myText = GetComponent<TextMeshProUGUI>(); 
    }
	
	// Update is called once per frame

    public void ShiftLetter()
    {

        StartCoroutine(ChangeText());
       // englishWords.SetActive(true);
    }

    public void ShiftBack()
    {
        StopAllCoroutines();
        if(sound != null)
            sound.Stop();
        myText.text = currentText;
    }

    

    IEnumerator ChangeText()
    {
        //Debug.Log("HI");


        for (int i = 0; i < stringLength; i++)
        {           

            for (int j = 0; myText.text[i] != goalText[i]; j++)
            {
                if (sound != null & !sound.isPlaying)
                    sound.Play();
                myText.text = myText.text.Remove(i, 1);
                myText.text = myText.text.Insert(i, alphabet[j].ToString());
                yield return null;
            }
        }
        if(sound != null)
            sound.Stop();
        //myText.text = myText.text.Remove(playText.Length, myText.text.Length - playText.Length);
        //GetComponent<Renderer>().material = normalMat; 
        //myText.font = englishFont;

    }


}
