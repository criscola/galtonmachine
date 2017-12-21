##Diario Galton Machine Project

####Data : 06 settembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Gantt
- Analisi dei mezzi
- Mockup GUI
- Continuato a cercare di integrare (progetto XnaIntoWpf) il progetto creato con Velcro Physics in uno UserControl di WPF in modo da poter utilizzare poi i controlli di WPF e rispettare i requisiti. [Sto seguendo la guida citata nel diario del 31 agosto](https://blogs.msdn.microsoft.com/nicgrave/2010/07/25/rendering-with-xna-framework-4-0-inside-of-a-wpf-application/) ma la strada sembra in salita. Non è semplice anche perchè devo adattare il tutto per Velcro Physics che è costruito a suo modo quindi XNA è più "nascosto". Se ho capito bene quando avrò finito di seguire la guida dovrò prendere il GraphicsDeviceManager istanziato usando la libreria di Velcro e fare in modo di passarlo al GraphicsDeviceService   creato con la guida.

##Problemi riscontrati e soluzioni adottate

- Ho avuto un problema perchè non trovava InitializeComponent() all'interno di XnaControl. Avendo copia-incollato non si è cambiata l'impostazione necessaria in proprietà Build Action da Compile a Page. Cambiando questa proprietà ha funzionato. [Ho seguito questo thread.](https://stackoverflow.com/questions/954861/why-cant-visual-studio-find-my-wpf-initializecomponent-method)

##Punto di situazione del lavoro

In linea.

##Programma per la prossima volta

- Continuare i mockup
- Ottenere più dettagli sul progetto dal mandante
- Continuare a cercare di implementare Velcro in WPF