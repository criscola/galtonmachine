## Diario Galton Machine Project

#### Data : 21 novembre 2017 
#### Autore : Cristiano Colangelo
#### Luogo: SAM Trevano

## Lavori svolti

- Debuggata curva normale

Il problema che avevo in precedenza, ovvero che ogni volta che chiamavo il metodo DrawCurve di BellCurve mi lanciava una ArgumentException con matrix.Invert(), era dato dal fatto che una curva non è invertibile se ha degli 0 al suo interno, il problema era quindi dato dai dati di partenza mancanti. Ho fatto in modo che siano necessari almeno 3 dati prima che avvenga il disegno della curva e ho aggiustato la struttura delle classi, ora il problema che ho è che, anche se la curva è scalata correttamente per stare dentro i confini del canvas, devo fare in modo che gli assi rimangano fissi e che si sposti se mai la scala degli assi e la curva. La curva dovrebbe andare a sinistra o a destra a dipendenza della deviazione (se i dati sono distribuiti piu a sx o a dx la curva deve spostarsi conseguentemente).

##Problemi riscontrati e soluzioni

-

##Punto di situazione del lavoro

Un po' indietro con la curva ma proseguo

##Programma per la prossima volta

Finire la curva