## Diario Galton Machine Project

#### Data : 28 novembre 2017 
#### Autore : Cristiano Colangelo
#### Luogo: SAM Trevano

## Lavori svolti

- Lavorato su palline multiple

Ora le palline si vedono ma hanno un po' di problemi perchè si sovrappongono a volte sulla stessa riga e non dovrebbe succedere. Inoltre non sarebbe male aggiungere un'animazione

- Iniziata riscrittura codice

Siccome il mio progetto ha raggiunto la massa critica (vedasi https://en.wikipedia.org/wiki/Critical_mass_(software_engineering)) ho deciso di riscriverlo da capo. Ho iniziato ad aggiungere le #region in modo da avere un codice ordinato e sto rivedendo alcuni problemi, come la classe BellCurve che non era completamente orientato agli oggetti e ho cambiato la classe HistogramChart in DistributionChart in modo che unisca gli istogrammi e la curva, da ora in poi sarà necessario interagire solo con questa classe "contenitore" che incapsula in modo object-oriented le 2 rappresentazioni, essa propagherà i cambiamenti dei dati alle 2 classi, in modo da non dover interagire direttamente con le strutture dati. Sto inoltre facendo una review delle varie keyword di accesso e normalizzando la struttura delle costanti/variabili ecc. così facendo i membri delle classi saranno proprietà e viceversa variabili solo quando necessario. Sto togliendo le costanti in favore di proprietà di sola lettura, anche se forse adopererò le costanti statiche readonly, ci devo ancora pensare e magari chiedere al commitente. Ho rimosso le classi Cell e Ball e ho aggiunto le classi Circle e Stick. Stick è sottoclasse di Circle. 

- Abbandonata curva

Purtroppo siccome non sono riuscito a trovare una soluzione, ho deciso di concentrarmi sulla qualità del codice esistente come detto il committente settimana scorsa (meno features, più qualità). 

Mi sono dimenticato nello scorso diario di citare la domanda che ho postato su un Q&A di statistica online che purtroppo non ha ricevuto nessuna risposta. https://stats.stackexchange.com/questions/315589/normal-distribution-curve-not-plotting-correctly



## Problemi riscontrati e soluzioni

-

## Punto di situazione del lavoro

Indietro

## Programma per la prossima volta

Continuare riscrittura codice
