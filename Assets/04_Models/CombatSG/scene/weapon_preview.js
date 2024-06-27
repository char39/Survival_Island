#pragma strict
var pumplist : AnimationClip[];
var target : GameObject;
private var pumpnumber : int = 0;
private var drawed : boolean = true;
private var playerview : boolean = true;

private var zoomIn : boolean = true;
private var zoomStart : boolean;
private var unZoom : boolean;

function Update(){
if (zoomStart){
	camera.fieldOfView = camera.fieldOfView-1;
	if (camera.fieldOfView<31) zoomStart = false;
}

if (unZoom){
	camera.fieldOfView = camera.fieldOfView+1;
	if (camera.fieldOfView>49.1) unZoom = false;

}


}

function OnGUI () {
		if (GUI.Button(Rect(10,10,120,25),"random fire (3)"))
			fire();

		if (GUI.Button(Rect(140,10,70,25),"reload"))
			reload();
		
		if (GUI.Button(Rect(220,10,115,25),"draw/holster "))
			drawholster();	
			
		if (GUI.Button(Rect(345,10,30,25),"run "))
			run();	
			
		if (GUI.Button(Rect(385,10,110,25),"Aim in & out"))
			aimInOut();
			
		if (GUI.Button(Rect(505,10,80,25),"Aim shot"))
			aimshot();
			
		if (GUI.Button(Rect(595,10,130,25),"player/world model"))
			switchview();
	}

function fire () {
camera.fieldOfView = 50;
target.animation.Play("fire");
yield WaitForSeconds(0.3);
target.animation.Play(pumplist[pumpnumber].name);
pumpnumber ++;
if (pumpnumber == 2) pumpnumber=0;
yield WaitForSeconds(0.8);
target.animation.Play("idle");
}

function reload () {
camera.fieldOfView = 50;
target.animation.Play("reloadStart");
target.animation.CrossFade("reloadCycle",0.65);
}

function run () {
camera.fieldOfView = 50;
target.animation.Play("runStart");
target.animation.CrossFade("running",0.5);
}

function aimInOut () {
if (zoomIn && playerview) {
	target.animation.Play("aimIn");
	zoomIn=false;
	yield WaitForSeconds(0.5);
	zoomStart = true;
	animation.Play("camAimIn");
	}
else if(!zoomIn && playerview){
	target.animation.Play("aimOut");
	zoomIn=true;
	unZoom=true;
	animation.Play("camAimOut");
	}

}

function drawholster () {
camera.fieldOfView = 50;
if (drawed==true) {
	target.animation.Play("holster");
	drawed=false;
	}
else {
	target.animation.Play("draw");
	drawed=true;
	yield WaitForSeconds(1.3);
	target.animation.Play("idle");
	}

}
function aimshot () {
if (!zoomIn && playerview){
	target.animation.Play("aimFire");
	target.animation.CrossFade("aimPump",0.2);
}
else if (zoomIn && playerview){
	target.animation.Play("aimIn");
	zoomIn=false;
	yield WaitForSeconds(0.5);
	zoomStart = true;
	animation.Play("camAimIn");
	yield WaitForSeconds(0.75);
	target.animation.Play("aimFire");
	target.animation.CrossFade("aimPump",0.2);
}
}

function switchview () {
	if(playerview){
		gameObject.Find("SG_HP").renderer.enabled=true;
		gameObject.Find("hands").renderer.enabled=false;
		gameObject.Find("shell").renderer.enabled=false;
		gameObject.Find("SGmesh").renderer.enabled=false;
		playerview = false;
	}
	else{
		gameObject.Find("SG_HP").renderer.enabled=false;
		gameObject.Find("hands").renderer.enabled=true;
		gameObject.Find("shell").renderer.enabled=true;
		gameObject.Find("SGmesh").renderer.enabled=true;
		playerview = true;
	}
}