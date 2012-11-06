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
using System.Collections;
using System.Diagnostics;

namespace LightningGame
{
    class IntroSprite : Sprite
    {

        protected internal double timeSinceStrike = 0.0;
        public IntroSprite(Texture2D texture, Vector2 position, Vector2 screenSize) :
            base(texture, position)
        {
            myTexture = texture;
            myState = new ShowState(this);
            float Xrat = screenSize.X / texture.Width;
            float Yrat = screenSize.Y / texture.Height;
            myScale.X = Xrat;
            myScale.Y = Yrat;
        }

        public void ShowInstructions()
        {
            myState = new ShowState(this);
        }

        class IdleState : State
        {
            public IdleState(Sprite sprite)
            {
            }

            public void Update(double elapsedTime, Sprite sprite)
            {
            }

            public void Draw(Sprite sprite, SpriteBatch batch)
            {
            }
        }

        class ShowState : State
        {
            public ShowState(Sprite sprite)
            {
                sprite.timer = 0.0;
            }

            public void Update(double elapsedTime, Sprite sprite)
            {
                if (sprite.timer >= 1.0)
                {
                    sprite.myState = new IdleState(sprite);
                }
            }

            public void Draw(Sprite sprite, SpriteBatch batch)
            {
                batch.Draw(sprite.myTexture, sprite.myPosition,
                null, Color.White,
                sprite.myAngle, sprite.myOrigin,
                sprite.myScale, SpriteEffects.None, 0f);
            }
        }


    }
}
