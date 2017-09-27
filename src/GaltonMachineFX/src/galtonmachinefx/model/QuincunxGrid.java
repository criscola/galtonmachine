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
package galtonmachinefx.model;

/**
 *
 * @author A4XX-COLCRI
 */
public class QuincunxGrid {

    private int[][] grid;
    private int size;

    public int getSize() {
        return size;
    }
    
    public QuincunxGrid(int size) {
        if (size > 2) {
            int count = 1;
            this.size = size;
            // Crea un int con colonne
            grid = new int[size][];
            for (int i = 0; i < size; i++) {
                grid[i] = new int[count];
                // Popola l'array con int di valore 0
                for (int j = 0; j < grid[i].length - 1; j++) {
                    grid[i][j] = 0;
                }
                count++;
            }
        } else {
            System.out.println("Specified width must be >= 2.");
        }
    }
    
    public int getCell(int x, int y) {
        return grid[x][y];
    }
    
    public void setCell(int x, int y, int value) {
        grid[x][y] = value;
    }
    
    public void IncrementCell(int x, int y) {
        grid[x][y] = grid[x][y] + 1;
    } 
    
    public int[] getResults() {
        int[] lastRow = new int[size];
        
        for (int i = 0; i < size; i++) {
            lastRow[i] = grid[size - 1][i];
        }
        return lastRow;
    }
    
}
