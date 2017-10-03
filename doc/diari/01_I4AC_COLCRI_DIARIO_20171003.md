##Diario Galton Machine Project

####Data : 03 settembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Spostato codice di disegno da GaltonMachineFX a GaltonMachineWPF (principale)
- Trovato come creare dinamicamente degli elementi e aggiungerli alla view in modo da utilizzare i bindings e quindi adottando correttamente il pattern MVVM. Ho utilizzato ItemsControl seguendo questo esempio: [https://stackoverflow.com/questions/22324359/add-n-rectangles-to-canvas-with-mvvm-in-wpf/22325266#22325266](https://stackoverflow.com/questions/22324359/add-n-rectangles-to-canvas-with-mvvm-in-wpf/22325266#22325266)
- Cercato di risolvere alcuni problemi grafici

##Problemi riscontrati e soluzioni adottate
- Il problema che dovevo risolvere era inerente alla creazione degli Ellipse programmaticamente. Districandomi fra vari esempi infine ho trovato quello perfetto per me e che spiegava in modo decente come fare ciò, grazie a un oggetto che rappresenta astrattamente l'Ellipse (Ball) ho fatto dei bindings fra i suoi attributi della classe e l'oggetto Ellipse vero e proprio nel XAML. Inoltre ho avuto il problema che dovevo fare i bindings alle proprietà di Canvas (Canvas.Top, Canvas.Left etc.) ma si è risolto allo stesso tempo perchè l'esempio includeva anche questo.
- Sto avendo alcuni problemi grafici in quanto sto cercando un modo per definire width e height della mainwindow e dello usercontrol che sia pulito. Comunque anche mettendo hard-coded i valori lo usercontrol dove è situato il canvas e la mainwindow non sono conguenti bene, quindi immagino che i bordi che aggiunge Windows sono incluse nel conto in pixel.

##Punto di situazione del lavoro

Leggermente indietro

##Programma per la prossima volta

- Aggiungere documentazione per quanto riguarda l'ItemsControl 
- Sistemare le incongruenze grafiche e pulizia del codice
- Sistemare la pallina che si muove