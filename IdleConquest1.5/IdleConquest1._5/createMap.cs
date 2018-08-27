using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdleConquest1._5
{
    class createMap
    {
        public static Rectangle currentRect;
        Rectangle[,] gameGrid = new Rectangle[100, 100];
        //Rectangle[,] mappiece = new Rectangle[100,100];
        int[,] mapkey = new int[100, 100];
        public static int width, height;
        public static int Locx, Locy,Gridsize;
        int numtemp;
        public static int castlex,castley;
        public static bool mark;
        public static int numend,numend2;
        Random random = new Random();
      
        public void drawgrid(int locx, int locy, int gridsize)
        {
            Locx = locx;
            Locy = locy;
            Gridsize = gridsize;
            if(mark==false)
            {
                numend = Convert.ToInt32(randomnumber(1, Convert.ToInt32(randomnumber(1, 10))));
                numend2 = numend;
                //createmap(width, height);
                width = randomnumber(10, 20);
                height = randomnumber(10, 20);
                 castlex = randomnumber(0, width-1);
                 castley = randomnumber(0, height-1);
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                       
                          
                            Game1.opponents[x, y] = randomnumber(1, Convert.ToInt32(randomnumber(1, 20)));
                        Game1.encounters[x, y] = new HostileEncounter();
                       
                            if (Game1.opponents[x, y] == 10)
                            {
                                Game1.Enemy.createlocation(50, 100, 5, 20, 5, 20, 10, 25, 1, 3, 95, 100, 1, 5, x, y, 50);
                                Game1.encounters[x, y].Encounterlife = Game1.Enemy.encounterlife;
                                Game1.encounters[x, y].Encounteroffense = Game1.Enemy.encounteroffense;
                                Game1.encounters[x, y].Encounterdefense = Game1.Enemy.encounterdefense;
                                Game1.encounters[x, y].Encounterdamage = Game1.Enemy.encounterdamage;
                                Game1.encounters[x, y].EncounterNOA = Game1.Enemy.encounterNOA;
                                Game1.encounters[x, y].Encountercritrange = Game1.Enemy.encountercritrange;
                                Game1.encounters[x, y].Encountercritmod = Game1.Enemy.encountercritmod;
                                Game1.encounters[x, y].Encounterarmy = Game1.Enemy.encounterarmy;

                            }
                            else { Game1.Enemy.createlocation(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, x, y, 0); }
                        }
                Game1.opponents[castlex, castley] = 55;
                for (int i = 0; i < Game1.playergenerallist.Count; i++)
                {

                    Game1.sprite[i].Destination = (new Vector2(0, 0));
                    Game1.sprite[i].Position = (new Vector2(600 + (32 * createMap.castlex), 50 + (32 * createMap.castley)));


                }
                mark = true;
            }
            


            for (int x = 0; x < width ; x++)
                for (int y = 0; y <height ; y++)
                {
                    gameGrid[x, y] = new Rectangle(locx + (x * gridsize), locy + (y * gridsize), gridsize, gridsize);
                    Game1.spriteBatch.Draw(Game1.map,gameGrid[x,y],Color.White);
                    if (Game1.opponents[x,y]==10)
                    {
                        Game1.spriteBatch.Draw(Game1.shieldstandard, gameGrid[x, y], Color.White);
                        
                    }
                    if(Game1.opponents[x,y]==55)
                    {
                        Game1.spriteBatch.Draw(Game1.castle, gameGrid[x, y], Color.White);
                    }
                    
              }
        }
        public int randomnumber(int minnum, int maxnum)
        {

            numtemp = (int)Math.Round(random.NextDouble() * (maxnum - minnum) + minnum);
            return numtemp;
        }
        public void checkgrid()
        {
            MouseState currentMouseState = Mouse.GetState();

            foreach (Rectangle grid in gameGrid)
            {
                if (grid.Contains(new Point(currentMouseState.X, currentMouseState.Y)))
                {
                    currentRect = grid;
                    DrawBorder(grid, 1, Color.Red);
                }



                

            }
            if (Game1.gamestate == "main" && MouseCord.mousetf == true && Game1.mouse.m_mousePos.Y >= 50 && Game1.mouse.m_mousePos.Y <= Locy + (height * Gridsize) && Game1.mouse.m_mousePos.X >= 600 && Game1.mouse.m_mousePos.X <= Locx + (width * Gridsize))
            {

                if (Game1.playergenerallist[MouseCord.generalcount].unitalive != false )
                {
                    Game1.playergenerallist[MouseCord.generalcount].destinationx = currentRect.X;
                    Game1.playergenerallist[MouseCord.generalcount].destinationy = currentRect.Y;
                  
                    Game1.sprite[MouseCord.generalcount].Destination = (new Vector2(Convert.ToInt32(Game1.playergenerallist[MouseCord.generalcount].destinationx), Convert.ToInt32(Game1.playergenerallist[MouseCord.generalcount].destinationy)));
                   
                }

            }
            if (Game1.sprite[MouseCord.generalcount].Position.X == Game1.playergenerallist[MouseCord.generalcount].destinationx && Game1.sprite[MouseCord.generalcount].Position.Y == Game1.playergenerallist[MouseCord.generalcount].destinationy)
            {
                Game1.tempi = Convert.ToInt32((Game1.playergenerallist[MouseCord.generalcount].destinationx - 600) / 32);
                Game1.tempj = Convert.ToInt32((Game1.playergenerallist[MouseCord.generalcount].destinationy - 50) / 32);
                
                
                Game1.playergenerallist[MouseCord.generalcount].encounter = Game1.opponents[Game1.tempi, Game1.tempj];
                Game1.playergenerallist[MouseCord.generalcount].encounterarmy = Game1.encounters[Game1.tempi, Game1.tempj].Encounterarmy;
                if (Game1.opponents[Game1.tempi, Game1.tempj] == 55)
                {
                    Game1.playergenerallist[MouseCord.generalcount].army = 100;
                }
            }
            MouseCord.mousetf = false;
        }
        public static void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
           Game1.spriteBatch.Draw(Game1.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);
            // Draw left line
            Game1.spriteBatch.Draw(Game1.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            // Draw right line
            Game1.spriteBatch.Draw(Game1.pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            // Draw bottom line
            Game1.spriteBatch.Draw(Game1.pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), borderColor);
        }
        
    }
}
