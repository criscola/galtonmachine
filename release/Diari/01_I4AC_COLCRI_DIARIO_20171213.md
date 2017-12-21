## Diario Galton Machine Project

#### Data : 13 dicembre 2017 
#### Autore : Cristiano Colangelo
#### Luogo: SAM Trevano

## Lavori svolti

Changelog
- Spostato GenerateSticks in GaltonSim e impostato su private. La chiamata avviene all'aggiornamento di SimulationSize.
- Spostato PlaceBallOnStick e PlaceBallOnTopStick nella classe GaltonSimulation 
- Rimossa definitivamente la classe QuincunxGrid 
- Ho pensato di togliere i costruttori da GaltonSimulation e inizializzare l'oggetto con un object initializer perchè il costruttore aveva bisogno di molti parametri, ma poi mi sono accorto che tutte le proprietà per essere inizializzate necessitano dell'accessor pubblico... l'oggetto FallingBall non vorrei che venga gestito direttamente dall'utente quindi l'ho lasciato su private e ho riutilizzato il costruttore come prima. Ho pensato di rendere il setter pubblico e lanciare una IllegalOperationException in caso di riassegnazione, ma mi è sembrata una bad practice.
- Modificata DistributionChart
- Altre correzioni

## Problemi riscontrati e soluzioni

Non mi disegna la curva, anzi mi restituisce un errore di tipo ArgumentException che dice 'Must create DependencySource on same Thread as the DependencyObject.' Non riesco a vedere la riga perchè visual studio mi dice che non trova un file .pdb (WindowsBase.pdb not found)...
## Punto di situazione del lavoro

Indietro con la doc sopratutto

## Programma per la prossima volta

Continuare riscrittura codice