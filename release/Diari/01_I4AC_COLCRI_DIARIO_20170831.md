##Diario Galton Machine Project

####Data : 31 agosto 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Continuata analisi del motore Velcro Physics. Ho iniziato a immaginare mentalmente come verrà implementato il progetto. Ho stabilito che il progetto conterrà un motore fisico che si preoccuperà della gestione delle palline, collisioni eccetera. E' sorto un piccolo problema in quanto nel QdC è indicato che la struttura del progetto dovrà essere creata usando il pattern MVVM (quindi presupponendo l'utilizzo di controlli WPF), ma la libreria grafica si appoggia a una [classe che non espone direttamente il puntatore nativo dell'area di disegno](https://msdn.microsoft.com/en-us/library/system.windows.interop.d3dimage(v=vs.110).aspx), mettendo in full screen l'area di disegno senza dare controllo all'utilizzatore della libreria. Sarebbe necessario maggiore controllo su questa classe, cosicchè l'area di disegno sia dimensionata **non in full screen**, lasciando dunque lo spazio necessario per i controlli WPF. [Intanto ho trovato una guida su msdn che spiega in modo abbastanza pulito come poter far ciò](https://blogs.msdn.microsoft.com/nicgrave/2010/07/25/rendering-with-xna-framework-4-0-inside-of-a-wpf-application/), e sto provando a integrare il codice in un esempio funzionante in modo da risolvere il problema, in caso di esito negativo cercherò altri modi, come creare un altro form separato o in ultima istanza procedere alla creazione di controlli interni al motore fisico.
- Intervista con il mandante. E' stato deciso che il progetto si potrà dividere essenzialmente in 4 tappe: creazione simulazione macchina di Galton, creazione gioco del Pachinko sulla base della simulazione, creazione sistema posizionamento aste e fori e creazione sistema di report. E' comunque stato deciso che la direzione nella quale il progetto andrà sarà deciso più in avanti, avendo varie idee per entrambi i possibili rami (simulazione o gioco) e potendo fare una valutazione migliore in futuro. 
- Continuato il Gantt (a buon punto)

##Problemi riscontrati e soluzioni adottate

-

##Punto di situazione del lavoro

In linea.

##Programma per la prossima volta

- Continuare a testare Velcro Physics e la sua integrazione con WPF
- Continuare il Gantt
- Analisi dei requisiti
- Analisi del dominio
- Scopo
- Abstract