using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdleConquest1._5
{
    public class MouseCord
    {
       public Vector2 m_mousePos;
        public List<Rectangle> clickbox = new List<Rectangle>();
        public string hover ="null",clickstate = "null";
        public static MouseState currentMouseState;
        public static MouseState previousMouseState;
        public static int generalcount=1;
        public static bool mousetf;
        public void  myMouse(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            m_mousePos.X = mouseState.X;
            m_mousePos.Y = mouseState.Y;
            MouseState currentMouseState = Mouse.GetState(); //Get the state
            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                previousMouseState.LeftButton == ButtonState.Released) //Will be true only if the user is currently clicking, but wasn't on the previous call.
            {
                mousetf = !mousetf; //Toggle the state between true and false.
            }
            previousMouseState = currentMouseState;
        }
        public void checkmouse(string gamestate, Rectangle rollbutton,Rectangle confirmbutton,Rectangle previousbutton, Rectangle nextbutton,Rectangle buygeneralbutton)
        {
            foreach (Rectangle rect in clickbox)
            {
               
                Point p = new Point(Convert.ToInt32(m_mousePos.X), Convert.ToInt32(m_mousePos.Y));

                if (gamestate == "kingmaker" && rollbutton.Contains(p))
                {
                    hover = "roll";
                    if (mousetf == true)
                    {
                        clickstate = "roll";

                    }
                }
                else if (gamestate == "kingmaker" && confirmbutton.Contains(p) || gamestate == "generalmaker" && confirmbutton.Contains(p))
                {
                    hover = "confirm";
                    if (mousetf == true)
                    {
                        clickstate = "confirm";

                    }
                }
                else if (gamestate == "generalmaker" && previousbutton.Contains(p) || gamestate == "buygeneral" && previousbutton.Contains(p) || gamestate == "main" && previousbutton.Contains(p))
                {
                    hover = "previous";
                    if (mousetf == true)
                    {
                        clickstate = "previous";
                        generalcount -= 1;
                        if (generalcount < 0)
                        {
                            if(gamestate=="generalmaker")
                            {
                                generalcount = Game1.generallist.Count - 1;
                            }
                            else if(gamestate=="buygeneral")
                            {
                                generalcount = Game1.hiregenerallist.Count - 1;
                            }
                            else if(gamestate =="main")
                            {
                                generalcount = Game1.playergenerallist.Count - 1;
                            }
                           
                        }
                        mousetf = false;
                    }
                }
                else if (gamestate == "generalmaker" && nextbutton.Contains(p) || gamestate == "buygeneral" && nextbutton.Contains(p) ||gamestate=="main" && nextbutton.Contains(p))
                {
                    hover = "next";
                    if (mousetf == true)
                    {
                        clickstate = "next";
                        if (mousetf == true)
                        {
                            generalcount += 1;

                            if (generalcount >= Game1.generallist.Count && gamestate =="generalmaker" )
                            {
                                generalcount = 0;
                            }
                            else if (generalcount >= Game1.playergenerallist.Count && gamestate == "main")
                            {
                                generalcount = 0;
                            }
                            else if (generalcount >= Game1.hiregenerallist.Count && gamestate == "buygeneral")
                            {
                                generalcount = 0;
                            }
                            
                        }

                        mousetf = false;
                    }
                }
                else if (gamestate =="main" && confirmbutton.Contains(p))
                {
                    hover = "confirm";
                    if (mousetf == true)
                    {
                       
                        clickstate="confirm";
                    }
                    mousetf = false;

                }
                else if (gamestate == "main" && buygeneralbutton.Contains(p)|| gamestate == "buygeneral" && buygeneralbutton.Contains(p) )
                {
                    hover = "buygeneral";
                    if (mousetf == true)
                    {
                        if (gamestate == "main")
                        {
                            clickstate = "buygeneral";
                        }
                        else clickstate = "main";
                        
                    }
                    mousetf = false;

                }
                else if (gamestate =="main")
                {
                    hover = "!";
                }
                else
                {
                 hover = "null";
                    mousetf = false;
                }
            }
        }
    }
}
