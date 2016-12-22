//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;





//public class HealthController : MonoBehaviour {

//	public static int LIVES = 5;


//	public RawImage heart1;
//	public RawImage heart2;
//	public RawImage heart3;
//	public RawImage heart4;
//	public RawImage heart5;
//	public RawImage heart6;
//	public RawImage heart7;

//	int schwierigkeitsgrad = 1;


//	// Use this for initialization
//	void Start () {

//	}

//	// Update is called once per frame
//	void Update () {
//		abziehen ();
//		}



//	void abziehen(){
	
//		if (schwierigkeitsgrad == 1) {
//			LIVES = 7;

//			if(LIVES == 7){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = true;
//				heart5.enabled = true;
//				heart6.enabled = true;
//				heart7.enabled = true;
//			}
//			if(LIVES == 6){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = true;
//				heart5.enabled = true;
//				heart6.enabled = true;
//				heart7.enabled = false;
//			}

//			if(LIVES == 5){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = true;
//				heart5.enabled = true;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 4){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = true;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 3){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 2){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = false;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 1){
//				heart1.enabled = true;
//				heart2.enabled = false;
//				heart3.enabled = false;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if (LIVES == 0) {
//			//Game over
//			}
//		}




//		if (schwierigkeitsgrad == 2) {
//			LIVES = 5;

//			if(LIVES == 5){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = true;
//				heart5.enabled = true;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 4){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = true;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 3){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 2){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = false;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 1){
//				heart1.enabled = true;
//				heart2.enabled = false;
//				heart3.enabled = false;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if (LIVES == 0) {
//				//Game over
//			}
//		}


//		if (schwierigkeitsgrad == 3) {
//			LIVES = 3;

//			if(LIVES == 3){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = true;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 2){
//				heart1.enabled = true;
//				heart2.enabled = true;
//				heart3.enabled = false;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if(LIVES == 1){
//				heart1.enabled = true;
//				heart2.enabled = false;
//				heart3.enabled = false;
//				heart4.enabled = false;
//				heart5.enabled = false;
//				heart6.enabled = false;
//				heart7.enabled = false;
//			}

//			if (LIVES == 0) {
//				//Game over
//			}
//		}

	
	
	
	
	
//	}

//	}

