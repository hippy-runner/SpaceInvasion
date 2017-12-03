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
public class Coordinates {
    
    private int _x, _y, _z;
    
    public Coordinates() {
        _x = 0;
        _y = 0;
        _z = 0;
    }
    
    public Coordinates(int x, int y, int z) {
        _x = x;
        _y = y;
        _z = z;
    }
    
    public int getX() {
        return _x;
    }
    
    public int getY() {
        return _y;
    }
    
    public int getZ() {
        return _z;
    }
    
    public void setX(int x) {
        _x = x;
    }
    
    public void setY(int y) {
        _y = y;
    }
    
    public void setZ(int z) {
        _z = z;
    }
}
