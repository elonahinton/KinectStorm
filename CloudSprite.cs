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
    class CloudSprite : Sprite
    {

        public CloudSprite(Texture2D texture, Vector2 position, int rMod) :
            base(texture, position)
        {
            myRandom = rMod;
            myTexture = texture;
            myState = new IdleState(this);
        }

        public void Show()
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
                if (sprite.timer >= 0.0)
                {
                    sprite.myState = new ShowState(sprite);
                }
            }

            public void Draw(Sprite sprite, SpriteBatch batch)
            {
            }
        }

        class ShowState : State
        {
            public ShowState(Sprite sprite)
            {
                sprite.timeSinceMove = 0.0;
                sprite.myVelocity = new Vector2(2, 0);
            }

            public void Update(double elapsedTime, Sprite sprite)
            {
                Random rand = new Random();
                int i = rand.Next(0, 100);
                int f = rand.Next(2, 8);
                if (sprite.myRandom != 0)
                    f = rand.Next(sprite.myRandom*2, sprite.myRandom*4);
                if (sprite.timer - sprite.timeSinceMove >= f && sprite.timer >= 6.0)
                {
                    sprite.timeSinceMove = sprite.timer;
                    if (i >= 50)
                    {
                        sprite.myVelocity = new Vector2(-0.5f, 0);
                    }
                    else
                        sprite.myVelocity = new Vector2(0.5f, 0);
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
