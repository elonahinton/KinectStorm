using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Kinect;
using System.Diagnostics;
using System.Collections;

namespace LightningGame
{
    // This is the base class for all Sprites.  I will put all variables and methods
    // that are common to sprites here.  In particular, Sprites should at least be able
    // to update and draw.

    class Sprite
    {
        // These are the basic state variables for sprites.
        // They are "protected" so they are accessible by the subclasses
        // They are "internal" so they are accessible by the internal classes of the 
        //    subclasses - namely the states.
        protected internal int myRandom;
        protected internal LightningGame myGame;
        protected internal double timeSinceMove = 0.0;
        protected internal double timer;
        protected internal Texture2D myTexture;
        protected internal Vector2 myPosition;
        protected internal Vector2 myVelocity = new Vector2(0, 0);
        protected internal float myAngle = 0f;
        protected internal float myAngularVelocity = 0f;
        protected internal Vector2 myScreenSize = new Vector2(0, 0);
        protected internal Vector2 myOrigin = new Vector2(0, 0);
        protected internal Vector2 myScale = new Vector2(1, 1);
        protected internal Vector2 myScaleVelocity = new Vector2(0, 0);
        protected internal State myState;

        // The base constructor.
        public Sprite(Texture2D texture, Vector2 position)
        {
            myTexture = texture;
            myPosition = position;
        }

        // This method is virtual because it can be overridden by the subclasses.  
        // In this method I will do the basic updating of my variables based on their
        // velocities.
        public virtual void Update(double elapsedTime)
        {
            timer += elapsedTime;
            myPosition += myVelocity;
            myAngle += myAngularVelocity;
            myScale += myScaleVelocity;
            // Let the state do its updating as well.
            myState.Update(elapsedTime, this);
        }

        public virtual void Draw(SpriteBatch batch)
        {
            // How the sprite draws depends on the state the sprite is in.
            myState.Draw(this, batch);
        }

        // This is a basic test for whether or not a sprite is off screen.
        public bool OutOfBounds()
        {
            return (myPosition.X + myTexture.Width > myScreenSize.X) ||
                   (myPosition.X < 0) ||
                   (myPosition.Y + myTexture.Height > myScreenSize.Y) ||
                   (myPosition.Y < 0);
        }
    }
}
