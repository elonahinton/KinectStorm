using System;
using System.Collections.Generic;
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

namespace LightningGame
{
    // State is an interface because it defines needed methods, with no 
    // instance variables.  The states all need to know how to Update and Draw.
    // We will not use elapsedTime right now, but it will be very useful soon, so
    // it is left here for future updates.
    interface State
    {
        void Update(double elapsedTime, Sprite sprite);
        void Draw(Sprite sprite, SpriteBatch batch);
    }

}
