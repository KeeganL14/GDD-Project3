using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //text
using UnityEngine.SceneManagement; //scene transition 

//Credit to Brackeys for most of the dialogue animation 
public class Tutorial : MonoBehaviour
{
    //dialogue Queue
    private Queue<string> SceneOneDialogue;  

    //master pre-tutorial arrays
    string[] officerStrings; //dialogue     

    //master tutorial array
    string[] tutorialStrings; //dialogue    

    //Text Variables     
    public Text speakerDialogue;          

    //AUDIO 
    public AudioSource clickSkip;        

    //Use this for initialization
    void Start()
    {
        //the dialogue queue
        SceneOneDialogue = new Queue<string>();
                

        //master pre tutorial array
        officerStrings = new string[5];
       

        //master tutorial array
        tutorialStrings = new string[13];
       

        //initialize the master pre-tutorial array:
        officerStrings[0] = "Mr. Dresden, you finally arrived!";
        officerStrings[1] = "We have secured the perimeter to make sure nothing escapes that warehouse!";
        officerStrings[2] = "I am sorry that this situation involves your daughter, as well as your associate.";
        officerStrings[3] = "I'm happy to not be in your situation...";
        officerStrings[4] = "What am I even saying? I'm supposed to briefing you on the intel we have received!";


        //initialize the tutorial array:
        tutorialStrings[0] = "We have received intel from our rooftop snipers that your associate, Mab, is holding your daughter hostage in the middle of the warehouse.";
        tutorialStrings[1] = "We can also confirm, that multiple life forms are guarding the rooms leading to the \"Boss Chamber\".";
        tutorialStrings[2] = "Sadly, since our bullets are ineffective against these life forms, we cannot clear the rooms.";
        tutorialStrings[3] = "Fortunately, you can!";
        tutorialStrings[4] = "We have blasted a hole into one of the warehouse's rooms.";
        tutorialStrings[5] = "Once you enter, you will be immediately swarmed by the guards.";
        tutorialStrings[6] = "To avoid being hit, used the WASD keys to move around the room.";
        tutorialStrings[7] = "While moving is just one piece of the puzzle, you must be able to kill these enemies. Use the arrow keys to launch your projectiles at them.";
        tutorialStrings[8] = "Although we cannot assist with manpower, we can help you out by launching potions into the rooms.";
        tutorialStrings[9] = "These potions will give you bonus effects for a certain period of time. Be wary of the time, though.";
        tutorialStrings[10] = "Finally, once the room has been cleared of all enemies, you will proceed into the next room, and do everything all over again.";

        //final lines
        tutorialStrings[11] = "I know this is a lot of information you have been given, but the time to save your daughter is dwindling!";
        tutorialStrings[12] = "Good luck, Mr. Dresden.";

        //queue the dialogue
        SceneOneDialogue.Enqueue(officerStrings[0]);
        SceneOneDialogue.Enqueue(officerStrings[1]);
        SceneOneDialogue.Enqueue(officerStrings[2]);
        SceneOneDialogue.Enqueue(officerStrings[3]);
        SceneOneDialogue.Enqueue(officerStrings[4]);

        //tutorial dialogue
        SceneOneDialogue.Enqueue(tutorialStrings[0]);
        SceneOneDialogue.Enqueue(tutorialStrings[1]);
        SceneOneDialogue.Enqueue(tutorialStrings[2]);
        SceneOneDialogue.Enqueue(tutorialStrings[3]);
        SceneOneDialogue.Enqueue(tutorialStrings[4]);
        SceneOneDialogue.Enqueue(tutorialStrings[5]);
        SceneOneDialogue.Enqueue(tutorialStrings[6]);
        SceneOneDialogue.Enqueue(tutorialStrings[7]);
        SceneOneDialogue.Enqueue(tutorialStrings[8]);
        SceneOneDialogue.Enqueue(tutorialStrings[9]);
        SceneOneDialogue.Enqueue(tutorialStrings[10]);
        SceneOneDialogue.Enqueue(tutorialStrings[11]);
        SceneOneDialogue.Enqueue(tutorialStrings[12]);       



        //start the first dialogue 
        DisplayDialogue();
    }

    //method for the dialogue 
    public void DisplayDialogue()
    {

        if (SceneOneDialogue.Count == 0)
        {
            //end of scene 
            Debug.Log("No more dialogue. End of Scene.");

            //switch to lvl select
            SceneManager.LoadScene("Level 1");

            //return
            return;

        }
        else
        {            
            //set the dialogue 
            string dialogue = SceneOneDialogue.Dequeue();
            StopAllCoroutines(); //In case player skips
            StartCoroutine(AnimateSentence(dialogue)); // Animates
                        
        }
    }

    //enumerator for making the text appear one by one
    IEnumerator AnimateSentence(string dialogue)
    {
        //start empty
        speakerDialogue.text = "";

        //then add in letter by letter
        foreach (char letter in dialogue.ToCharArray())
        {
            speakerDialogue.text += letter;
            yield return new WaitForSeconds(.05f);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //sfx 
            clickSkip.Play();
            clickSkip.loop = false;

            //dialogue method 
            DisplayDialogue();
        }
    }
}
