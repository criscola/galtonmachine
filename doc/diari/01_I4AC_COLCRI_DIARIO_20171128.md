## Diario Galton Machine Project

#### Data : 28 novembre 2017 
#### Autore : Cristiano Colangelo
#### Luogo: SAM Trevano

## Lavori svolti

- Lavorato su palline multiple
Sto facendo in modo di fare che scendano più palline per ogni ciclo della simulazione. Sto anche aggiungendo la possibilità di mettere in pausa, andare avanti e indietro con la simulazione. Sto lasciando perdere la curva normale perchè si è rivelata una grande perdita di tempo. Se avrò ancora tempo alla fine modificherò la scala per fare in modo che i dati in modo che la curva rimanga visibile anche con l'andare della simulazione. Vorrei in alternativa aggiungere un'altra rappresentazione grafica chiamata boxplot. Ho fatto ciò che il mandante mi ha detto di fare ovvero moltiplicare il valore di ogni istogramma per la sua posizione (es. se il primo valore è 10, si fa 1 * 10, se il secondo è 20, si fa 2 * 20, eccetera) ma la il risultato non è cambiato. Comunque ho dovuto modificare un po' la mia architettura perchè all'inizio l'avevo pensata solo con una pallina, quindi ho creato una classe chiamata "Cell" che contiene la pallina e in più la posizione colonna/riga (che prima memorizzavo in 2 variabili nel model).

## Problemi riscontrati e soluzioni

Ho un problema con le palline che cadono soltanto nelle prime 3 posizioni, non ho ancora trovato la soluzione perchè ci sto ancora lavorando.

## Punto di situazione del lavoro

Indietro

## Programma per la prossima volta

Continuare le palline multiple copiando i dati dal model a viewmodel (ora le palline ancora non si vedono)
