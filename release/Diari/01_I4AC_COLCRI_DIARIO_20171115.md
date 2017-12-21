##Diario Galton Machine Project

####Data : 15 novembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Continuata la curva normale

##Problemi riscontrati e soluzioni

Inizialmente volevo usare il codice che ho trovato su questo sito [http://csharphelper.com/blog/2015/09/draw-a-normal-distribution-curve-in-c/](http://csharphelper.com/blog/2015/09/draw-a-normal-distribution-curve-in-c/) e questo [http://csharphelper.com/blog/2015/09/draw-a-scaled-normal-distribution-in-c/](http://csharphelper.com/blog/2015/09/draw-a-scaled-normal-distribution-in-c/), ma poi ho cercato di renderlo WPF-style utilizzando una curva di bezier (un elemento di XAML), solo che non ci sono riuscito siccome il codice si basa troppo profondamente su Bitmap, in particolare utilizza un oggetto di tipo Matrix che dovrebbe "trasformare" in qualche modo le coordiante ma non saprei come integrarlo. Allora dopo mezza giornata ho deciso di creare un'immagine png con sfondo trasparente e applicarla sul canvas.

##Punto di situazione del lavoro

Ho un problema che anche cercando per ore non riesco a trovare un esempio. Il canvas del grafico si trova in un ItemsPanelTemplate quindi non posso accederci direttamente per aggiungere l'oggetto Image. Sto cercando di seguire questa guida [https://stackoverflow.com/questions/3131919/wrappanel-as-itempanel-for-itemscontrol](https://stackoverflow.com/questions/3131919/wrappanel-as-itempanel-for-itemscontrol)

##Programma per la prossima volta

Finire la generazione della curva normale.