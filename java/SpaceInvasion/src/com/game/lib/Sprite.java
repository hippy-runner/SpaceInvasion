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

import java.awt.Image;
import java.awt.image.BufferedImage;
import java.io.File;
import javax.imageio.ImageIO;

/**
 * Abstract class for making sprites and animated sprites
 * @author hiccup
 */
public abstract class Sprite {
    
    // protected variables that all inherited classes need
    protected BufferedImage _image;             // the actual image for the sprite (or set of sub-sprites for animation)
    protected boolean _visible;                 // boolean flag used to determine whether or not to draw sprite 
    protected Coordinates _location;            // (x, y) coordinates of the top left corner of the sprite's location, and z for the layer drawn
    protected Dimensions _dimensions;           // the width and height of the sprite
    protected int _frameCount;                  // for animating sprites through several sub frames of a larger image
    protected Coordinates[] _subSpriteCoords;   // multi-dimensional array for storing the coordinates of sub frames of a larger image
    
    // for error trapping 
    protected Exception _error;
    
    public Sprite(
            String imgFile,
            boolean visible,
            Coordinates location,
            Dimensions dim,
            int frameCount,
            Coordinates[] subSpriteCoords) {
        
        try {
            // attempt to load image from file location provided
            _image = ImageIO.read(new File(imgFile));
            
            // assign inputs appropriately
            _visible = visible;
            _location = location;
            _dimensions = dim;
            _frameCount = frameCount;
            _subSpriteCoords = subSpriteCoords;
            
            // reset error to null, shows success
            _error = null;
            
        } catch (Exception ex) {
            _error = ex;
        }
    }
    
    public Image getImage(int frame) {
        
        // local variable for storing appropriate sub-sprite
        Image subSprite = null;
        
        // attempt to grab sub-sprite
        try {
            // check the sub-sprite coordinate array
            if (_subSpriteCoords != null) {
                
                // grab appropriate sup-sprite when array is not null
                subSprite = _image.getSubimage(_subSpriteCoords[frame].getX(), 
                    _subSpriteCoords[frame].getY(), 
                    _dimensions.getWidth(), 
                    _dimensions.getHeight());
                
            } else {
                
                // otherwise, sprite is not animated
                subSprite = _image.getSubimage(0, 0, _dimensions.getWidth(), _dimensions.getHeight());
            }
            
            // reset error to null, shows success
            _error = null;
            
        } catch (Exception ex) {
            // catch any exceptions, especially out-of-bounds
            _error = ex;
        }
        return subSprite;
    }
    
    public boolean visible() {
        return _visible;
    }
    
    public Coordinates getLocation() {
        return _location;
    }
    
    public Dimensions getDimensions() {
        return _dimensions;
    }
    
    public Exception getError() {
        return _error;
    }
    
    public void setVisibility(boolean visible) {
        _visible = visible;
    }
    
    public void setLocation(Coordinates newLoc) {
        _location = newLoc;
    }
    
    public void setDimension(Dimensions newDim) {
        _dimensions = newDim;
    }
}
