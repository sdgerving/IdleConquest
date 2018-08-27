using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IdleConquest1._5
{
    public class AnimatedSprite
    {
        Texture2D spriteTexture;
        float timer = 0f;
        float interval = 200f;
        int currentFrame = 0;
        int spriteWidth = 32;
        int spriteHeight = 48;
        int spriteSpeed = 1;
        public static bool stop = true;
           
        Rectangle sourceRect;
         Vector2 position,destination;
        Vector2 origin;
        KeyboardState currentKBState;
        KeyboardState previousKBState;
       
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        public bool Stop
        {
            get { return stop; }
            set { stop = value; }
        }
        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        public Texture2D Texture
        {
            get { return spriteTexture; }
            set { spriteTexture = value; }
        }

        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }
        public AnimatedSprite(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight)
        {
            this.spriteTexture = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;
            
        }
        public void HandleSpriteMovement(GameTime gameTime)
        {
            previousKBState = currentKBState;
            currentKBState = Keyboard.GetState();

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            if (stop==true)
            {
                if (currentFrame > 0 && currentFrame < 4)
                {
                    currentFrame = 0;
                }
                if (currentFrame > 4 && currentFrame < 8)
                {
                    currentFrame = 4;
                }
                if (currentFrame > 8 && currentFrame < 12)
                {
                    currentFrame = 8;
                }
                if (currentFrame > 12 && currentFrame < 16)
                {
                    currentFrame = 12;
                }
            }


            if (destination.X != 0&&Game1.gamestate=="main")
            {
                if (position.X == destination.X)
                {
                    stop = true;
                    
                    Game1.playergenerallist[MouseCord.generalcount].ismoving = false;
                }
                
                else if (position.X < destination.X )
                {
                    if (stop == true)
                    {
                        currentFrame = 8;
                    }
                    stop = false;
                    Game1.playergenerallist[MouseCord.generalcount].ismoving = true;
                    AnimateRight(gameTime);
                    position.X += spriteSpeed;

                }
                else if (position.X > destination.X )
                {
                    if (stop == true)
                    {
                        currentFrame = 4;
                    }
                    stop = false;
                    Game1.playergenerallist[MouseCord.generalcount].ismoving = true;
                    AnimateLeft(gameTime);
                    position.X -= spriteSpeed;
                }
              
             
            }
            if (position.X == destination.X && destination.Y !=0 )
            {
                if (position.Y == destination.Y)
                {
                    Game1.playergenerallist[MouseCord.generalcount].ismoving = false;
                    stop = true;
                }

                else if (position.Y < destination.Y)
                {
                    if (stop == true)
                    {
                        currentFrame = 0;
                    }
                    stop = false;
                    Game1.playergenerallist[MouseCord.generalcount].ismoving = true;
                    AnimateDown(gameTime);
                    position.Y += spriteSpeed;

                }
                else if (position.Y > destination.Y )
                {
                    if (stop == true)
                    {
                        currentFrame = 12;
                    }
                    Game1.playergenerallist[MouseCord.generalcount].ismoving = true;
                    stop = false;
                    AnimateUp(gameTime);
                    position.Y -= spriteSpeed;
                }
            }



            // This check is a little bit I threw in there to allow the character to sprint.



        }

        public void AnimateRight(GameTime gameTime)
        {
           
                if (position.X == destination.X)
                {
                    currentFrame = 8;
                }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
                {
                    currentFrame++;

                    if (currentFrame > 11)
                    {
                        currentFrame = 8;
                    }
                    timer = 0f;
                }
            
        }
        public void AnimateLeft(GameTime gameTime)
        {

            if (position.X == destination.X)
            {
                currentFrame = 5;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 7)
                {
                    currentFrame = 4;
                }
                timer = 0f;
            }

        }
        public void AnimateUp(GameTime gameTime)
        {
            
                if (position.Y == destination.Y)
                {
                    currentFrame = 13;
                }

                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer > interval)
                {
                    currentFrame++;

                    if (currentFrame > 15)
                    {
                        currentFrame = 13;
                    }
                    timer = 0f;
                }
            
        }

        public void AnimateDown(GameTime gameTime)
        {
            
                if (position.Y == destination.Y)
                {
                    currentFrame = 0;
                }

                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timer > interval)
                {
                    currentFrame++;

                    if (currentFrame > 3)
                    {
                        currentFrame = 0;
                    }
                    timer = 0f;
                }
            
        }

        

    }

}
