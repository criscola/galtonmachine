##Diario Galton Machine Project

####Data : 07 settembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Continuato a provare a integrare MonoGame in WPF con scarsi risultati. Infine ho deciso insieme al mandante di iniziare facendo un progetto usando soltanto le classi grafiche di WPF (usando ellipse, transformgroup ecc.)
- Parlato con il mandante dei requisiti e deciso di tenerli poco dettagliati, se caso aumentare il livello di dettaglio dopo aver creato una versione del software funzionante usando MVVM/WPF.

##Problemi riscontrati e soluzioni adottate

- Mi sono spuntati altri errori al momento della compilazione nonostante ieri avessi cambiato da Compile a Page. Andando a guardare la descrizione di tutte le Build Option ho scoperto che dovevo creare uno User Control da Visual Studio invece di fare una classe a parte (era XnaControl) e poi aggiungere tutto il codice C# e XAML là, invece inizialmente pensavo che XnaControl fosse una classe .cs separata e che lo XAML fosse da includere nella MainWindow. 
- Ho un problema con il progetto, ora mi viene fuori un errore a runtime dove dice che non trova il componente System.Runtime.WindowsRuntime. Ho provato a scaricarlo da NuGet ma indica che non ci sono versioni compatibili con il progetto. Ho provato a fare un downgrade della versione sia del progetto che del componente ma niente da fare.
- Use case


##Punto di situazione del lavoro

In linea.

##Programma per la prossima volta

- Fare il progetto MVVM e iniziare a scrivere la logica della simulazione