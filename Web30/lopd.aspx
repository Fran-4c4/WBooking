<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lopd.aspx.vb" Inherits="lopd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Politica de privacidad</title>
<style  TYPE="text/css">
BODY {
	font-family: Tahoma, Arial;
	font-size: 10px;
	COLOR: #193C7F;
}

</style>    
    
</head>
<body>
    <form id="form1" runat="server">
    <div>

<BODY leftmargin="10" topmargin="10" marginwidth="10" marginheight="10" >
<CENTER><B><%=cfr.Language.getCaption  (283)%> - </B></CENTER>
<BR><BR>

 <% 
     If cfr.Language.CurrentCultureName = "es-ES" Then
 %>


En cumplimiento de lo dispuesto por la Ley Orgánica de Protección de Datos 15/99 (LOPD) y en su normativa de desarrollo, así como en la Ley de Servicios de la Sociedad de la Información y de Comercio Electrónico 34/02,  le informamos de lo siguiente:
<BR><BR>1. Los datos personales recogidos a través de los distintos formularios incluidos en el website van a ser incorporados a unos ficheros automatizados, titularidad de esta empresa, con la finalidad de ofrecerle nuestra mejor atención e informarle de nuestros servicios y promociones y con su previo consentimiento, expresado mediante la aceptación de las condiciones de estos formularios. Los datos recogidos son unicamente y exclusivamente los necesarios para la formalización de la reserva.
<BR><BR>2. El alojamiento no cederá sus datos personales a terceros sin su consentimiento expreso. No obstante, podrá ceder, en su caso, sus datos personales al resto de las empresas necesarias para la formalización de la reserva con las mismas finalidades que se han indicado para la recogida de los datos personales por parte de la empresa, garantizándose en todo caso la confidencialidad en el tratamiento automatizado de los datos de carácter personal y aplicándose lo dispuesto en la presente Política de Protección de Datos de Carácter Personal. 
<BR><BR>3. El usuario se hace responsable de la veracidad de los Datos Personales que facilite al alojamiento y de la comunicación de cualquier alteración en los mismos. 
<BR><BR>4. Si lo desea, puede ejercitar los derechos de acceso, rectificación, cancelación, y oposición previstos en la Ley, dirigiendo un escrito al responsable del fichero, enviando un e-mail con la misma referencia. Para cualquier duda al respecto de la política de confidencialidad y protección de datos de carácter personal seguidos por nuestra empresa, puede dirigirse a la misma dirección. 
 <%
 End if
 
 %>


<% 
    If cfr.Language.CurrentCultureName = "en-US" Then
 %>
In fulfillment of the arranged thing by the Statutory law of Protection of Data 15/99 (LOPD) and in its norm of development, as well as in the Law of Services of the Society of the Information and of Electronic Commerce 34/02, we informed to him into the following thing: 

<BR><BR>1.The gathered personal data through the different forms including in website are going to be incorporated to automated files, titularidad of this company, with the purpose of offering our better attention to him and to inform into our services and promotions to him and with its previous consent, expressed by means of the acceptance of the conditions of these forms.  The gathered data are only and exclusively the necessary ones for the formalization of the reserve.
<BR><BR>2.The lodging will not yield its personal data to third without its express consent.  However, it will be able to yield, in his case, his personal data to the rest of the necessary companies for the formalization of the reserve with the same purposes that have been indicated for the collection of the personal data on the part of the company, being guaranteed in any case the confidentiality in the automated treatment of the personal character data and being applied the arranged thing in the present Policy of Protection of Personal Character data. 

<BR><BR>3.The user becomes person in charge of the veracity of the Personal Data that he facilitates to the lodging and of the communication of any alteration in such. 
<BR><BR>4.If it wishes it, it can exercise the rights of access, rectification, cancellation, and opposition anticipated in the Law, directing a writing to the person in charge of the file, sending an email with the same reference.  For any doubt on the matter of the policy of confidentiality and protection of personal character data followed by our company, it can go to the same direction. 
 <%
 End if
  %> 

<% 
    If cfr.Language.CurrentCultureName = "de-DE" Then
 %>
In der Erfüllung der geordneten Sache durch das gesetzliche Gesetz des Schutzes von Daten 15/99 (LOPD) und in seiner Norm der Entwicklung, sowie im Gesetz der Services der Gesellschaft von den Informationen und elektronischen Handels 34/02, informierten uns wir zu ihm in die folgende Sache: 

<BR><BR>1.Die erfaßten persönlichen Daten durch die unterschiedlichen Formen, die im website umfassen, worden werden zu automatisierten Akten, titularidad enthalten werden dieser Firma, mit dem Zweck des Antrags unserer besseren Aufmerksamkeit zu ihm und sich in unsere Dienstleistungen und in Förderungen zu ihm informieren und mit seiner vorhergehenden Zustimmung, ausgedrückt mittels der Annahme der Zustände dieser Formen.  Die erfaßten Daten sind nur und ausschließlich die notwendigen für die Formalisierung der Reserve. 
<BR><BR>2.Die Unterkunft erbringt seine persönlichen Daten nicht zum Third ohne seine ausdrückliche Zustimmung.  JedochIST es in der Lage, in seinem Argument, seine persönlichen Daten zu erbringen zum Rest der notwendigen Firmen für die Formalisierung der Reserve mit den gleichen Zwecken, die für die Ansammlung der persönlichen Daten von seiten der Firma angezeigt worden sind, die in jedem möglichem Fall die Vertraulichkeit in der automatisierten Behandlung der persönlichen Textdaten garantiert werden und der geordneten Sache in der anwesenden Politik des Schutzes der persönlichen Textdaten zugetroffen sind.

<BR><BR>3.Der Benutzer wird Person verantwortlich für das veracity der persönlichen Daten, die er zur Unterkunft und von der Kommunikation jeder möglicher Änderung in so erleichtert.
<BR><BR>4.Wenn er ihn wünscht, er kann die Rechte des Zuganges, der Korrektur, der Annullierung und der Opposition ausüben, die im Gesetz vorweggenommen wird und ein Schreiben auf die Person verantwortlich für die Akte verweisen und ein email mit dem gleichen Hinweis schicken.  Für jeden möglichen Zweifel bezüglich der Politik der Vertraulichkeit und des Schutzes der persönlichen Textdaten, die von unserer Firma gefolgt werden, kann er zur gleichen Richtung gehen. 


 <%
 End if
  %> 


<BR><BR>
    </div>
    </form>
</body>
</html>
