##Diario Galton Machine Project

####Data : 11 settembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Ho deciso di non utilizzare un converter per rappresentare la pallina perchè complica troppo una cosa semplice e perchè il ViewModel è già un converter di per se, è quindi inutile aggiungere questa funzionalità del converter. Ho comunque implementato la logica griglia<->coordinate ma non sono andato a utilizzare il converter nello xaml. Nel web nessuno consiglia di utilizzare i converter se non per determinati piccoli task. Vedere: [https://stackoverflow.com/questions/1007487/how-can-wpf-converters-be-used-in-an-mvvm-pattern](https://stackoverflow.com/questions/1007487/how-can-wpf-converters-be-used-in-an-mvvm-pattern) <br> [https://groups.google.com/forum/#!msg/wpf-disciples/P-JwzRB_GE8/Tc9GRUvmvIYJ](https://groups.google.com/forum/#!msg/wpf-disciples/P-JwzRB_GE8/Tc9GRUvmvIYJ)
- Ho cambiato quasi totalmente l'algoritmo di disegno per poter ordinare in modo comodo e senza inutili giri le stecche 
- Aggiunto il codice che traspone il tutto nella Grid
- Migliorato il codice
- Modificato leggermente model
- Iniziata stesura HistogramSet (da vedere [http://csharphelper.com/blog/2015/09/draw-a-normal-distribution-curve-in-c/](http://csharphelper.com/blog/2015/09/draw-a-normal-distribution-curve-in-c/))

##Problemi riscontrati e soluzioni adottate

Nessun problema riscontrato

##Punto di situazione del lavoro

Leggermente indietro

##Programma per la prossima volta

- Aggiornare documentazione, continuare HistogramSet, aggiungere controlli