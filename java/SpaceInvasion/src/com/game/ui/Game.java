/*
 * Copyright (C) 2017 Nathan Minor
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
package com.game.ui;

//import com.game.lib;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.event.KeyEvent;
import java.awt.event.KeyListener;
import javax.swing.JFrame;

/**
 *
 * @author hiccup
 */
public class Game extends JFrame implements Runnable {
    
    private final int _WIDTH = 600;
    private final int _HEIGHT = 400;
    private int _dX, _dY;
    private int _heroX, _heroY;
    private boolean _failed;
    private String _errorMsg;
    
    public static void main(String[] args) {
        Game g = new Game();
        
        if (g.failed())
            System.out.println(g.errorMsg());
    }
    
    public Game(){
        
        setSize(_WIDTH, _HEIGHT);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);
        setResizable(false);
        setTitle("Space Invasion");
        setVisible(true);
        
        addKeyListener(new Input());
        
        _heroX = getWidth()/2 - 16;
        _heroY = getHeight()/2 - 16;
        
        try {
            
        } catch (Exception ex) {
            _failed = true;
            _errorMsg = ex.getMessage();
        }
    }
    
    public void setDx(int dX) {
        _dX = dX;
    }

    public void setDy(int dY) {
        _dY = dY;
    }
    
    public boolean failed() {
        return _failed;
    }
    
    public String errorMsg() {
        return _errorMsg;
    }

    public void draw(Graphics buffer_g) {
        
        buffer_g.setColor(Color.GRAY);
        buffer_g.fillRect(0, 0, getWidth(), getHeight());
        
        repaint();
    }
    
    @Override
    public void paint(Graphics buffer_g) {
        Image offScreen_img = createImage(getWidth(), getHeight());
        draw(offScreen_img.getGraphics());
        
        buffer_g.drawImage(offScreen_img, 0, 0, null);
    }
    
    public class Input implements KeyListener{

        @Override
        public void keyPressed(KeyEvent e) {
            int keyCode=e.getKeyCode();

            if(keyCode==KeyEvent.VK_W){
                setDy(-5);
            }
            if(keyCode==KeyEvent.VK_A){
                setDx(-5);
            }
            if(keyCode==KeyEvent.VK_S){
                setDy(5);
            }
            if(keyCode==KeyEvent.VK_D){
                setDx(5);
            }
            if(keyCode==KeyEvent.VK_ESCAPE){
                stop();
            }
        }

        @Override
        public void keyReleased(KeyEvent e) {
            int keyCode=e.getKeyCode();

            if(keyCode==KeyEvent.VK_W){
                setDy(0);
            }
            if(keyCode==KeyEvent.VK_A){
                setDx(0);
            }
            if(keyCode==KeyEvent.VK_S){
                setDy(0);
            }
            if(keyCode==KeyEvent.VK_D){
                setDx(0);
            }
        }

        @Override
        public void keyTyped(KeyEvent e) {

        }

    }
    
    @Override
    public void run() {
        new Thread().start();
    }
    
    public void stop() {
        System.exit(0);
    }
    
}
