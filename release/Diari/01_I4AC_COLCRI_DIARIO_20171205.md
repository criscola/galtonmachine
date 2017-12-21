## Diario Galton Machine Project

#### Data : 05 dicembre 2017 
#### Autore : Cristiano Colangelo
#### Luogo: SAM Trevano

## Lavori svolti

- Refactoring codice
- Cercato di risolvere il problema spiegato sotto

## Problemi riscontrati e soluzioni

- Ho riscontrato un problema di progettazione dato da C#. Tutti i miei oggetti grafici necessitano obbligatoriamente della struttura dati ObservableCollection per poter propagare i cambiamenti alla GUI. Purtroppo, esponendo pubblicamente i riferimenti alle Collection, il programmatore potrebbe potenzialmente modificare (sbagliando) i valori contenuti nella Collection. In C# non esiste un meccanismo come ad esempio C# che prevede la possibilità di passare dei riferimenti in sola lettura (usando const) ma propone 
1. Usare una struttura dati Immutable
2. Usare una struttura dati del tipo ReadOnlyCollection
3. Fare una copia

    Siccome le prime 2 opzioni non sono possibili e la terza è troppo scomoda e anche ascoltando il consiglio del docente responsabile che ha detto di esporre pubblicamente la Collection ed evitarmi molti problemi, farò in questo modo, anche se so che sarebbe meglio non lasciare la possibilità al programmatore di agire sugli item della Collection. A questo punto

- Mi si è buggato il progetto e ho dovuto ricrearlo perchè mi usciva fuori un errore "Cannot find or open the PDB file" ho visto questo link ma non mi ha aiutato https://www.codeproject.com/Questions/392518/Cannot-find-or-open-the-PDB-file
Sto ancora cercando di capire come fixarlo perchè anche rifacendo da capo il progetto non va...
## Punto di situazione del lavoro

Indietro

## Programma per la prossima volta

Continuare riscrittura codice