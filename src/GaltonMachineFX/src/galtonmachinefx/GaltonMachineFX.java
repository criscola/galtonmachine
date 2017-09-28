/*
 * The MIT License
 *
 * Copyright 2017 A4XX-COLCRI.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
package galtonmachinefx;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import javafx.scene.canvas.*;
import javafx.scene.paint.Color;
import galtonmachinefx.model.*;
import galtonmachinefx.graphics.*;
import java.util.ArrayList;

/**
 *
 * @author A4XX-COLCRI
 */
public class GaltonMachineFX extends Application {
    
    public static final int GRID_SIZE = 13;
    
    private Canvas canvas;
    private GraphicsContext gc;
    private ArrayList<BallRenderer> sticks = new ArrayList<>();
    
    @Override
    public void start(Stage stage) throws Exception {
        // Inizializzazione
        Parent root = FXMLLoader.load(getClass().getResource("MainView.fxml"));
        
        Scene scene = new Scene(root);
        
        stage.setScene(scene);
        stage.show();
        
        Ball ball = new Ball();
        QuincunxGrid grid = new QuincunxGrid(GRID_SIZE);
        
        // Get del canvas
        canvas = (Canvas) scene.lookup("#simulationArea");
        gc = canvas.getGraphicsContext2D();
        gc.strokeRect(0, 0, canvas.getWidth(), canvas.getHeight());
        
        // Disegno stecche
        gc.setFill(Color.RED);
        
        // Grandezza della base
        double n = grid.getSize();
        // Distanza fra le stecche
        double dx = (canvas.getWidth() - BallRenderer.DIAMETER) / (n - 1);
        double dy = (canvas.getHeight() - BallRenderer.DIAMETER) / (n - 1);
        // Coordinate x y delle stecche iniziali
        double x = 0;
        //double y = (n - 1) * d;
        double y = canvas.getHeight() - BallRenderer.DIAMETER;
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n - i; j++) {
                BallRenderer currentBall = new BallRenderer(BallRenderer.DIAMETER, BallRenderer.DIAMETER);
                sticks.add(currentBall);
                
                gc.fillOval(
                    x,
                    y,
                    currentBall.getWidth(), 
                    currentBall.getHeight());
                x += dx;
            }   
            x = dx / 2 * (i + 1);
            y -= dy;
        }
    }

    public void repaint(GraphicsContext gc) {
        
    }
    
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        launch(args);
    }
    
}
