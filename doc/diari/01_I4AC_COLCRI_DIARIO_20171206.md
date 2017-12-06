## Diario Galton Machine Project

#### Data : 06 dicembre 2017 
#### Autore : Cristiano Colangelo
#### Luogo: SAM Trevano

## Lavori svolti

- Refactoring codice
- Adesso contrariamente a come volevo fare faccio accedere il viewmodel direttamente alle observablecollection, in quanto ormai essendo pubblici è inutile incapusarle in metodi. 
- Cercato di risolvere i problemi sotto

## Problemi riscontrati e soluzioni

- Il problema dell'altra volta non era dato da un bug del progetto in se ma da dei nullpointer. Il problema è che l'IDE non ha mostrato un errore comprensibile. Cercando un po' in rete ho trovato questo link https://stackoverflow.com/questions/2519329/why-doesnt-visual-studio-show-an-exception-message-when-my-exception-occurs-in che spiega che è necessario abilitare un'impostazione che ferma il debugging non appena incontra la prima exception, così facendo non fa finire l'esecuzione del programma in punti oscuri di .NET dalla quale è impossibile risalire all'errore originario.
- Non si disegnano le palline cadenti, sto ancora cercando di capire perchè

## Punto di situazione del lavoro

Molto indietro

## Programma per la prossima volta

Continuare riscrittura codice