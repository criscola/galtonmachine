##Diario Galton Machine Project

####Data : 8 novembre 2017 <br> Autore : Cristiano Colangelo <br> Luogo: SAM Trevano

##Lavori svolti

- Continuato sistema di disegno istogrammi

##Problemi riscontrati e soluzioni adottate

Sto riscontrando un problema che mi ha tenuto impegnato tutto il giorno ma non ho ancora trovato una soluzione. Ho necessità di aggiungere dei textblock sopra gli istogrammi in modo che possano mostrare il valore numerico dell'istogramma, per far ciò ho trovato questo link: [https://stackoverflow.com/questions/5473001/itemscontrol-with-multiple-datatemplates-for-a-viewmodel](https://stackoverflow.com/questions/5473001/itemscontrol-with-multiple-datatemplates-for-a-viewmodel "questo link") in cui dice di usare una CompositeCollection. Quindi ho creato una classe che rappresenti il TextBlock chiamata ChartLabel e una ObservableCollection per essa e poi l'ho unita con la ObservableCollection degli istogrammi. Successivamente nello XAML ho scritto il codice

    <ItemsControl ItemsSource="{Binding Path=ChartItemsCollection}">
        <ItemsControl.ItemsPanel>
            <ItemsPanelTemplate>
                <Canvas Focusable="False" Width="{Binding Path=CanvasWidth}" Height="{Binding Path=CanvasHeight}"/>
            </ItemsPanelTemplate>
        </ItemsControl.ItemsPanel>
        <ItemsControl.ItemContainerStyle>
            <Style>
                <Setter Property="Canvas.Left" Value="{Binding Path=X}"/>
                <Setter Property="Canvas.Top" Value="{Binding Path=Y}"/>
            </Style>
        </ItemsControl.ItemContainerStyle>
        <ItemsControl.Resources>
            <DataTemplate DataType="{x:Type model:Histogram}">
                <Rectangle Width="{Binding Path=Width}" Height="{Binding Path=Height}" Fill="Red" />
            </DataTemplate>
            <DataTemplate DataType="{x:Type model:Label}">
                <TextBlock Text="{Binding Path=Text}" Background="Brown"/>
            </DataTemplate>
        </ItemsControl.Resources>
    </ItemsControl>

ma non trovava la classe ChartLabel, leggendo un po' su internet ho trovato che è necessario avere un costruttore senza parametri. Dopo aver corretto ho provato a far funzionare il codice ma tutto ciò che appare è una scritta "(Collection)" in alto a sinistra dello schermo e non riesco a capire dov'è l'errore essendo che ho fatto come nel link, magari c'è qualche problema con il DataContext che ignoro o forse c'entra con il fatto che l'esempio fa vedere che dentro al ObservableCollection è dichiarato un array di oggetti (anche se provando a dichiarare hard-coded un array con dati di test il risultato è lo stesso).

##Punto di situazione del lavoro

Un po' indietro, a quest'ora l'istogramma sarebbe dovuto essere quasi finito ma manca ancora la curva normale, la scala/griglia e l'indicazione numerica del valore dell'istogramma. Dopo aver finito il grafico dovrò documentarlo, e infine completare il requisito aggiuntivo della generazione di un report.
##Programma per la prossima volta

- Risolvere il problema riscontrato e cercare di completare il grafico