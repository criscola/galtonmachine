##Diario Galton Machine Project

####Data : 18 settembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Terminato capitolo animazione pallina implementazione + aggiunta MVVM in progettazione 
- Terminata interfaccia grafica
	- Ridefiniti contenitori dei controlli
	- Terminati slider + textbox di xceed IntegerUpDown (sarà magari da aggiungere nella doc dato che uso un componente esterno)
	- Aggiunto bottone di reset che resetta tutto ai valori di default

##Problemi riscontrati e soluzioni adottate

- Ho riscontrato un problema con il thread che rilasciava una exception, poi mi sono ricordato che fare un t.Abort() è come far saltare in aria il thread con la dinamite invece di spegnerlo con dolcezza quindi ho corretto il problema con una t.Join() e il relativo codice in AnimateFallingBall()
- Sto riscontrando un problema con la griglia che non ha le proporzioni giuste tranne quando si ridimensiona l'applicazione

##Punto di situazione del lavoro

In linea

##Programma per la prossima volta

- Sistemare il problema della griglia, documentazione, istogrammi