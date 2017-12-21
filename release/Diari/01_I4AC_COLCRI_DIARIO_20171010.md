##Diario Galton Machine Project

####Data : 10 settembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Creata "alternativa" per l'animazione della pallina. In poche parole ho usato la giornata per documentare e scrivere del codice per far animare la pallina, per poi rendermi conto che c'è un modo migliore per farlo (come inizialmente avevo pensato), ovvero adoperare un Converter che mi possa trasformare le coordinate delle celle in coordinate x y. Per implementare questo converter ho modificato la classe QuincuxGrid e ho fatto in modo che le celle contengano i riferimenti agli oggetti Ball, poi ho fatto in modo di copiare le stecche della ObservableCollection nelle celle della griglia. Successivamente seguirò quanto scritto nella documentazione in precedenza (capitolo 1.1 implementazione) e passerò al Converter le coordinate X e Y delle celle, che me trasformerà in coordinate con il metodo già scritto "PlaceBallOnStick". Il lavoro fatto fino ad oggi non è comunque tempo perso perchè fa vedere una alternativa più diretta per fare l'animazione della pallina, dato che è sempre una buona cosa mostrare delle alternative di alcuni approcci. Sicuramente trovo che l'approccio più elaborato con il Converter sia più scalabile/pulito/MVVM style.


##Problemi riscontrati e soluzioni adottate

Nessun problema riscontrato

##Punto di situazione del lavoro

Leggermente indietro

##Programma per la prossima volta

- Implementare il Converter, aggiornare la documentazione