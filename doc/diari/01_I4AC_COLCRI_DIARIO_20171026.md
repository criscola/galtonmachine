##Diario Galton Machine Project

####Data : 26 settembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Sistemato algoritmo per disegnare gli istogrammi, ObservableCollection lancia l'evento CollectionChanged quando un elemento viene aggiunto/rimosso dalla collezione, non quando lo stato di un suo oggetto viene modificato. Per fare ciò ho fatto implementare a Histogram BindableBase e aggiunto i vari PropertyChanged alle sue proprietà
- Sistemata la fine della simulazione, ora quando la simulazione finisce i controlli si abilitano/disabilitano di conseguenza (Start si abilita e Stop si disabilita, prima bisognava premere Stop e poi Start), avevo un problema perchè quando cercavo di lanciare RaiseCanExecuteChanged mi dava un InvalidOperationException. Ho scoperto che non è possibile agire sui controlli della UI invocando comandi delegati da un thread che non è il thread principale. Ho quindi trovato che è necessario usare il Dispatcher dell'applicazione per invocare il RaiseCanExecuteChanged, in modo che passi l'invocazione al thread principale
- Implementata label che dice iterazione corrente / tot. iterazioni
- Aggiornato Gantt

##Problemi riscontrati e soluzioni adottate

Nessun problema riscontrato

##Punto di situazione del lavoro

Leggermente indietro

##Programma per la prossima volta

- Continuare con gli istogrammi