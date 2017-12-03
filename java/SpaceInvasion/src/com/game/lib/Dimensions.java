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
package com.game.lib;

/**
 *
 * @author hiccup
 */
public class Dimensions {
    private int _width, _height;
    
    public Dimensions() {
        _width = 0;
        _height = 0;
    }
    
    public Dimensions(int width, int height) {
        _width = width;
        _height = height;
    }
    
    public int getWidth() {
        return _width;
    }
    
    public int getHeight() {
        return _height;
    }
    
    public void setWidth(int width) {
        _width = width;
    }
    
    public void setHeight(int height) {
        _height = height;
    }
}
