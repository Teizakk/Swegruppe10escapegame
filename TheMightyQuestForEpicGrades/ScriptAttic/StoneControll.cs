//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class StoneControll : MonoBehaviour {


//	int panzahl;
//	int hanzahl;

//	public RawImage p1;
//	public RawImage p2;
//	public RawImage p3;
//	public RawImage h1;
//	public RawImage h2;
//	public RawImage h3;
//	public RawImage h4;
//	public RawImage h5;

//	// Use this for initialization
//	void Start () {
//		pabziehen ();
//		habziehen ();
	
//	}
	
//	// Update is called once per frame
//	void Update () {
	
//	}

//	void pabziehen(){
//		if(panzahl == 3){


//			p1.enabled = true;
//			p2.enabled = true;
//			p3.enabled = true;
//		}

//		if(panzahl == 2){


//			p1.enabled = true;
//			p2.enabled = true;
//			p3.enabled = false;
//		}

//		if(panzahl == 1){


//			p1.enabled = true;
//			p2.enabled = false;
//			p3.enabled = false;
//		}

//		if(panzahl == 0){


//			p1.enabled = false;
//			p2.enabled = false;
//			p3.enabled = false;
//		}

//	}

//	void habziehen(){
//		if(hanzahl == 5){


//			h1.enabled = true;
//			h2.enabled = true;
//			h3.enabled = true;
//			h4.enabled = true;
//			h5.enabled = true;
//		}

//		if(hanzahl == 4){


//			h1.enabled = true;
//			h2.enabled = true;
//			h3.enabled = true;
//			h4.enabled = true;
//			h5.enabled = false;
//		}

//		if(hanzahl == 3){


//			h1.enabled = true;
//			h2.enabled = true;
//			h3.enabled = true;
//			h4.enabled = false;
//			h5.enabled = false;
//		}

//		if(hanzahl == 2){


//			h1.enabled = true;
//			h2.enabled = true;
//			h3.enabled = false;
//			h4.enabled = false;
//			h5.enabled = false;
//		}

//		if(hanzahl == 1){


//			h1.enabled = true;
//			h2.enabled = false;
//			h3.enabled = false;
//			h4.enabled = false;
//			h5.enabled = false;
//		}

//		if(hanzahl == 0){


//			h1.enabled = false;
//			h2.enabled = false;
//			h3.enabled = false;
//			h4.enabled = false;
//			h5.enabled = false;
//		}
//	}
//}
