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
    class LightningSprite : Sprite
    {

        protected internal SoundEffect mySound;
        protected internal CloudSprite[] myClouds;
        protected internal double timeSinceStrike = 0.0;
        public LightningSprite(Texture2D texture, SoundEffect sound, Vector2 position, CloudSprite[] clouds, LightningGame game) :
            base(texture, position)
        {
            myGame = game;
            myTexture = texture;
            mySound = sound;
            myClouds = clouds;
            myState = new IdleState(this);
        }

        public void Strike(Vector2 handPosition)
        {
            myState = new StrikeState(this);
            myPosition.X = handPosition.X / 2;
            myPosition.Y = handPosition.Y;
        }

        private void PlayThunderCrash()
        {
            mySound.Play();
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

        class StrikeState : State
        {
            public StrikeState(Sprite sprite)
            {
            }

            public void Update(double elapsedTime, Sprite sprite)
            {

                LightningSprite litSpr = (LightningSprite)sprite;
                Random rand = new Random();
                int c = rand.Next(0,3);
                if (sprite.timer - litSpr.timeSinceStrike > 1.0)
                {

                    litSpr.PlayThunderCrash();
                    litSpr.timeSinceStrike = sprite.timer;
                }
                else
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
