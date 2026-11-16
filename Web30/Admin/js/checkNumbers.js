function solonums2(e){
	var s = (document.all) ? e.keyCode : e.which; // 2
	if (s==0)
	{
		return true;
	}
	if (s==46 || s==44 || s==8){
		return true;
	}
	if(s < 48 || s > 57){
		return false;
	}


}


function solonums(){
	var s = window.event.keyCode;
	if (s==0)
	{
		return true;
	}
	if (s==46 || s==44 || s==8){
		return true;
	}
	if(s < 48 || s > 57){
		return false;
	}

}