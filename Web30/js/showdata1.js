function BuscarObjeto(Objeto, Documento)
{
	var p,i,x;  
	if(!Documento) Documento=document; if((p=Objeto.indexOf("?"))>0&&parent.frames.length) {
	Documento=parent.frames[Objeto.substring(p+1)].document; Objeto=Objeto.substring(0,p);}
	if(!(x=Documento[Objeto])&&Documento.all) x=Documento.all[Objeto];
	else if (!document.layers)  if (!document.all) x=Documento.getElementById(Objeto);
	for (i=0;!x&&i<Documento.forms.length;i++) x=Documento.forms[i][Objeto];
	for(i=0;!x&&Documento.layers&&i<Documento.layers.length;i++) x=BuscarObjeto(Objeto,Documento.layers[i].document); return x;
}

function omlayer(layer,show){
	var l;
	l=BuscarObjeto(layer);
	if (l==null) {
	    return false;
	}
	if (show==true)
	{
		l.style.display="block";
	}
	else
	{
		l.style.display="none";
	}
}



function chxlayerstate(layer){
	return toggleLayer(layer);
}

function toggleLayer(layer) {
	var l=BuscarObjeto(layer);
	if (l==null) {
	    return;
	}

     l.style.display=(l.style.display=='none'? 'block':'none');
}

function md(nombreCapa,vTexto)
{
	vercapa(nombreCapa);
	recargarCapa(nombreCapa,vTexto);
}

function capashowstate(nombreCapa)
{
		varObjeto=BuscarObjeto(nombreCapa);
		if (varObjeto.style) varObjeto=varObjeto.style;
		return varObjeto.visibility;
}

function vocapa(nombreCapa)
{
		varObjeto=BuscarObjeto(nombreCapa);
		if (varObjeto==null)
		{
			return;
		}
		if (varObjeto.style) varObjeto=varObjeto.style;
		if(document.layers)
		{
			if (varObjeto.visibility=="show")
			{
				varObjeto.display="none";
				varObjeto.visibility="hide";
			}
			else
			{
				varObjeto.display="block";
				varObjeto.visibility="show";
			}
			
		}
		else
		{
			if (varObjeto.visibility=="visible")
			{
				varObjeto.display="none";
				varObjeto.visibility="hidden";
			}
			else
			{
				varObjeto.display="block";
				varObjeto.visibility="visible";
			}
		}
}
	

function vercapa(nombreCapa)
{
		vocapa(nombreCapa);
}

function od(nombreCapa)
{
		varObjeto=BuscarObjeto(nombreCapa);
		if (varObjeto==null)
		{
			return;
		}
		if (varObjeto.style) varObjeto=varObjeto.style;
			varObjeto.display="none";
		if(document.layers)
		{
			varObjeto.visibility="hide";
		}
		else
		{
			varObjeto.visibility="hidden";
		}
}

	function recargarCapa(nombreCapa,vTexto){
		varObjeto=BuscarObjeto(nombreCapa)
		/* Nestcape */
		if (document.layers){
			if(typeof(varObjeto)!="undefined"){
				var capa = eval("document.layers."+nombreCapa);
				capa.document.open();
	 			capa.document.write("<layer top=\"0\" left=\"0\">");
				capa.document.write(vTexto);
				capa.document.write("</layer>");
				capa.document.close();
				capa.visibility="show";
			}
		}
		/* Explorer */
		else if (document.all){
			if(typeof(eval("document.all."+nombreCapa))!="undefined"){
				document.all[nombreCapa].innerHTML=vTexto;
			}
		}
		// Nestcape 6	
		else if (document.getElementById){
			if(typeof(document.getElementById(nombreCapa))!="undefined"){
				document.getElementById(nombreCapa).innerHTML =vTexto;				
			}
		}
	}


