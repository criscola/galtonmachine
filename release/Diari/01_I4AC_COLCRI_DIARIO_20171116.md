##Diario Galton Machine Project

####Data : 16 novembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Risolto finalmente il problema di come aggiungere singoli elementi all'ItemsControl

Dopo vari giorni di ricerca sono riuscito a trovare il modo di aggiungere un singolo elemento a un ItemsControl bindato. Questo problema mi ha portato via molta energia perchè non riuscivo a trovare la soluzione corretta online fra la miriade di possibilità che WPF offre. Infine ho trovato un link [https://stackoverflow.com/questions/36378980/wpf-add-extra-item-to-bound-collection?noredirect=1&lq=1](https://stackoverflow.com/questions/36378980/wpf-add-extra-item-to-bound-collection?noredirect=1&lq=1) dove l'autore ritraeva circa lo stesso problema e sono riuscito a trovare la soluzione: 

	<ItemsControl.ItemsSource>
	    <CompositeCollection>
	        <CollectionContainer Collection="{Binding Source={StaticResource SticksList}}"/>
	        <!-- Pallina che cade -->
	        <Ellipse Canvas.Top="{Binding Path=BallY}" Canvas.Left="{Binding Path=BallX}"
	            Width="{Binding Path=BallDiameter}" Height="{Binding Path=BallDiameter}" Fill="Black" />
	    </CompositeCollection>
	</ItemsControl.ItemsSource> 

Come si può vedere la sorgente dei dati dell'ItemsControl va a prendere la collection delle stecche (che è dichiarata come una StaticResource che contiene semplicemente il riferimento all'ObservableCollection delle stecche) e subito sotto il singolo elemento, giustamente non bindato a una struttura dati del tipo ObservableCollection o CompositeCollection (essendo solo 1 elemento).

- Continuata la curva normale

Sto continuando a scrivere il codice per la curva normale. Ho messo su la classe BellCurve che si occupa di generare la curva. Passandogli una List<float> di dati la classe si occupa di computare i suoi dati matematici interni e di conseguenza riaggiornare il suo attributo Image di tipo BitmapImage che contiene il disegno della curva. Il viewmodel aggiorna la lista di istogrammi contenuti nella classe model (GaltonMachine) alla fine di ogni ciclo di simulazione, questi fa in modo di invocare il metodo UpdateData della classe BellCurve, che appunto si occupa di rigenerare la curva. Infine l'immagine di BellCurve viene assegnata all'attributo del viewmodel bindato alla view.

Il disegno della curva è funzionante (vedere misc/lib/RescalingHistograms) e genera un'immagine Bitmap/PNG con sfondo trasparente, ma l'integrazione del tutto nel progetto ufficiale è ancora da mettere in ordine, il metodo UpdateData non viene mai chiamato quindi sospetto che il problema sia nell'assegnazione/aggiornamento dei dati nel model o nella propagazione degli stessi viewmodel -> model -> histogramchart -> curve

##Problemi riscontrati e soluzioni

Vedere lavori svolti

##Punto di situazione del lavoro

Sono a buon punto avendo risolto gran parte dei problemi. Il tempo che rimarrà lo userò per la documentazione, generazione report e piccole modifiche (vedere doc/todo.txt)

##Programma per la prossima volta

Continuare con la curva