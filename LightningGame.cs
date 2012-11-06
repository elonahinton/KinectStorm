//Geoff Pisarkiewicz and Alex Hinton 2012
//Makes you think you are Zeus by having lightning strike when you move your righthand "quickly"

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
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;
using System.Diagnostics;

using KinectExplorer;

namespace LightningGame
{
    public class LightningGame : KinectGame
    {

        private SpriteBatch spriteBatch;
        private List<Sprite> mySprites = new List<Sprite>();
        private LightningSprite myLightning;
        private IntroSprite myIntro;
        private CloudSprite[] myClouds = new CloudSprite[3];
        private Vector2 lastPosition = new Vector2();
        //Particle Effects. Not part of the Kinect, so it won't be documented here. Just trust the they work.
        //Boolean indicating whether the second player is in the middle of a clap
        
        public LightningGame(GameParameters gParams)
            : base(gParams) { } //Leave this blank!

        public override GameConfig GetConfig()
        {
            //Return the GameConfig
            return new GameConfig("Zeus", "Alex & Geoff", "Use your hand in a swift movement to call the thunder.");
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentLoader content)
        {
            Texture2D lightTx = content.Load<Texture2D>("Lightning");
            int lightX = (graphics.GraphicsDevice.Viewport.Width - lightTx.Width <= 0 ? 100 : ((graphics.GraphicsDevice.Viewport.Width - lightTx.Width + 100) / 2));
            
            Texture2D introTx = content.Load<Texture2D>("intro2");
            int introX = (graphics.GraphicsDevice.Viewport.Width - introTx.Width <= 0 ? 0 : ((graphics.GraphicsDevice.Viewport.Width - introTx.Width)/2));
            int introY = (graphics.GraphicsDevice.Viewport.Height - introTx.Height <= 0 ? 0 : ((graphics.GraphicsDevice.Viewport.Height - introTx.Height) / 2));
            myIntro = new IntroSprite(introTx,
                new Vector2(0, 0),
                new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height));
            for (int i = 0; i < myClouds.Length; i++)
            {
                myClouds[i] = new CloudSprite(content.Load<Texture2D>("cloudX"),
                new Vector2(-i*300, 0), i);
                mySprites.Add(myClouds[i]);
            }
            myLightning = new LightningSprite(lightTx,
                content.Load<SoundEffect>("lightningS"),
                new Vector2(lightX, 0),
                myClouds,
                this);
            mySprites.Add(myIntro);
            mySprites.Add(myLightning);
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        // Looks for the closest point to the kinect (least depth) in a small area around the startPoint
        private Point findShallowestPoint(Point startPoint, int startDepth, out int depthOut)
        {
            //max difference in depth allowed (so we don't count things too far in the foreground)
            int variance = 500;
            //how far away to look
            int range = 120;

            int min = startDepth;
            Point minPoint = startPoint;

            for (int i = -range; i < range; i++)
            {
                for (int j = -range; j < range; j++)
                {
                    int depth = KinectManager.GetDepthAtPixel(startPoint.X + i, startPoint.Y + j, Resolution);
                    int dif = Math.Abs(startDepth - depth);

                    //if the depth is shallower but not past the variance
                    if (dif < variance && depth < min)
                    {
                        //then set a new min
                        min = depth;
                        minPoint = new Point(startPoint.X + i, startPoint.Y + j);
                    }
                }
            }

            depthOut = min;
            return minPoint;
        }

        public override void Update(GameTime gameTime)
        {
            //SkeletonA does the Lightning Strike
            if (SkeletonA != null) 
            {

                //Get the hand's position
                Joint rightHand = SkeletonA.Joints[JointType.HandRight];
                Vector2 handPos = GetJointPosOnScreen(rightHand);

                if (lastPosition != null)
                {
                    float dx = handPos.X - lastPosition.X;
                    float dy = handPos.Y - lastPosition.Y;
                    float dis = (float)Math.Sqrt(dx * dx + dy * dy);
                    if (dis > 100)
                    {
                        myLightning.Strike(handPos);
                    }
                }
                lastPosition = handPos;

            }
            //actualGameTime += gameTime.ElapsedGameTime.TotalSeconds;
            foreach (Sprite s in mySprites)
            {
                s.Update(gameTime.ElapsedGameTime.TotalSeconds);
            }
            
        }

        public override void Draw(GameTime gameTime)
        {
            DrawCamera();

            spriteBatch.Begin();
            foreach (Sprite s in mySprites)
            {
                s.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}

