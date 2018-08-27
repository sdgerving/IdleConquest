using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;

namespace IdleConquest1._5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        int temparmy = 0;
        string mark="null";
        int casualties = 0, kills=0,marks=0;
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static int[,] opponents = new int[25, 25];
        public static string gamestate = "kingmaker", test;
        public Vector2 m_mousePos2;
        public static SpriteFont MSGothic31, Corsiva8, Corsiva15, Corsiva20, Corsiva30, Corsiva40;
        public static float  numtemp, unitlife, unitoffense, unitdefense, unitdamage, unitNOA, unitcritrange, unitcritmod, generalmod,modroll,totalattack,totaldefense,empirebonus,totaldamage,critrolltotal;
        Timer sec3 = new Timer();
        Timer attackrate = new Timer();
        Rectangle[,] gameGrid = new Rectangle[32, 32];
        List<Rectangle> clickbox = new List<Rectangle>();
         public static MouseCord mouse = new MouseCord();
        double combatperc;
        public static Texture2D pixel, bear, snake, spider, bear32, snake32, spider32, smleft, smright, lrgleft, lrgright, shieldstandard, map,castle;
        Point rollbox, confirmbox;
        
        public static Rectangle rollbutton, confirmbutton, empireRec, previousbutton, nextbutton,buygeneralbutton;
        public static int tempi=0, tempj=0;
        int tempstate = 0;
        Ruler one = new Ruler("Tippy", "Tippy Kingdom", 0, 0, 0, 0, 0, 0);
        public static List<General> generallist = new List<General>();
        public static List<General> playergenerallist = new List<General>();
       public static List<General> hiregenerallist = new List<General>();
        public static Vector2 Mousexlabelpos2 = new Vector2(1405, 10);
        Random random = new Random();
        public static List<AnimatedSprite> sprite = new List<AnimatedSprite>();
        public static List<HostileEncounter> encounterlist = new List<HostileEncounter>();
        public static HostileEncounter Enemy = new HostileEncounter();
        public static HostileEncounter[,] encounters = new HostileEncounter[25,25];
        string attacker;
        
        float playerinitiativeroll, opponentinitiativeroll, playerNOA, opponentNOA;
        createMap gamemap = new createMap();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1800;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = 980;   // set this value to the desired height of your window
            graphics.IsFullScreen = false;
        }

        protected override void Initialize()
        {

            // TODO: Add your initialization logic here

            this.IsMouseVisible = true;

            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            MSGothic31 = Content.Load<SpriteFont>("MSGothic31");
            Corsiva8 = Content.Load<SpriteFont>("Corsiva8");
            Corsiva15 = Content.Load<SpriteFont>("Corsiva15");
            Corsiva20 = Content.Load<SpriteFont>("Corsiva20");
            Corsiva30 = Content.Load<SpriteFont>("Corsiva30");
            Corsiva40 = Content.Load<SpriteFont>("Corsiva40");

            castle = Content.Load<Texture2D>("castle");
            shieldstandard = Content.Load<Texture2D>("ShieldStandard");
            map = Content.Load<Texture2D>("map");
            smright = Content.Load<Texture2D>("smright");
            smleft = Content.Load<Texture2D>("smleft");
            bear = Content.Load<Texture2D>("knightlong");
            lrgright = Content.Load<Texture2D>("lrgright");
            lrgleft = Content.Load<Texture2D>("lrgleft");
            
            General Warrior = new General("Korvic", "Warrior", bear, bear, 1, 5, 20, 10, 1, 85, 2, 100, 100, new Vector2(600, 50), 0,60f,80f,98f,590,45,true,0,false);
            General Barbarrian = new General("TOR", "Barbarrian", bear, bear, 1, 5, 3, 10, 1, 85, 2, 100, 100, new Vector2(600, 85), 0, 10f, 30f, 50f, 590, 45, true,0, false);
            General Soldier = new General("Alvinius", "Soldier", bear, bear, 1, 5, 3, 10, 1, 85, 2, 100, 100, new Vector2(600, 150), 0, 60f, 80f, 99f, 590, 45, true,0, false);
            General bagger = new General("teabagger", "bag boy", bear, bear, 1, 30,15 , 5, 1, 85, 2, 100, 100, new Vector2(600, 250), 0, 60f, 80f, 99f, 590, 45, true,0, false);

            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            rollbox = new Point(50, 50);
            confirmbox = new Point(100, 100);

            mouse.clickbox.Add(rollbutton);
            mouse.clickbox.Add(confirmbutton);
            mouse.clickbox.Add(previousbutton);
            mouse.clickbox.Add(nextbutton);
            mouse.clickbox.Add(buygeneralbutton);

            generallist.Add(Warrior);
            generallist.Add(Barbarrian);
            generallist.Add(Soldier);
            generallist.Add(bagger);
            for ( int i=0;i<generallist.Count();i++)
            {
                sprite.Add( new AnimatedSprite(generallist[i].spritename32, 1, 32, 32));
                sprite[i].Position = new Vector2(generallist[i].spritePosition.X, generallist[i].spritePosition.Y);
            }
            
            hiregenerallist.AddRange(generallist);
            


            
            


            pixel.SetData(new[] { Color.White });

        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            mouse.myMouse(gameTime);
            sec3.startTimer(gameTime, 30);
            mouse.checkmouse(gamestate, rollbutton, confirmbutton, previousbutton, nextbutton,buygeneralbutton);
            for(int i=0;i<sprite.Count;i++)
            {
                sprite[i].HandleSpriteMovement(gameTime);
            }

            
            base.Update(gameTime);
        }
        public void checkmap()
        {
            
        }
        public void Initiative()
        {

            int x = 1400;
            int y = 420;
            foreach (General general in playergenerallist)
            {

                spriteBatch.DrawString(Corsiva30, general.generalname.ToString(), new Vector2(x, y), Color.Red);
                y += 30;

            }
            for (int i = 0; i < playergenerallist.Count; i++)
            {
                if (playergenerallist[i].encounter == 10 && playergenerallist[i].ismoving != true && playergenerallist[i].army >= 1 && encounters[tempi, tempj].encounterarmy >= 1)
                {

                    if (playergenerallist[i].unitalive == false)
                    {
                        modroll = randomnumber(1, 100);
                        if (modroll <= playergenerallist[i].modmin)
                        {
                            generalmod = 1;
                            generateunit();
                            Game1.playergenerallist[i].unitalive = true;
                        }
                        if (modroll > playergenerallist[i].modmin && modroll <= playergenerallist[i].modmid)
                        {
                            generalmod = 1.5f;
                            generateunit();
                            Game1.playergenerallist[i].unitalive = true;
                        }
                        if (modroll > playergenerallist[i].modmid && modroll <= playergenerallist[i].modmax)
                        {
                            generalmod = 2;
                            generateunit();
                            Game1.playergenerallist[i].unitalive = true;
                        }
                        if (modroll > playergenerallist[i].modmax)
                        {
                            generalmod = 3;
                            generateunit();
                            Game1.playergenerallist[i].unitalive = true;
                        }
                    }

                    playerinitiativeroll = randomnumber(1, 10) + playergenerallist[i].speed;
                    opponentinitiativeroll = randomnumber(1, 10);
                    playerNOA = unitNOA;
                    opponentNOA = encounters[tempi, tempj].encounterNOA;
                    if (playerNOA != 0 || opponentNOA != 0)
                    {
                        int pcount = i;
                        if (playerinitiativeroll > opponentinitiativeroll)
                        {
                            
                            attacker = "player";
                            Combat(pcount);
                        }
                        else if (playerinitiativeroll < opponentinitiativeroll)
                        {
                            
                            attacker = "opponent";
                            Combat(pcount);
                        }
                        else Combat(pcount);

                    }





                }
            }


            }
        public void Combat(int pcount)
        {
            
            if(attacker =="player")
            {
                mark = "player";
                if(playerNOA<=0)
                {
                    attacker = "opponent";
                }
                totalattack = randomnumber(1, 100) + unitoffense + empirebonus;
                totaldefense = randomnumber(1, 100) + encounters[tempi, tempj].encounteroffense;
                if (totalattack >= totaldefense)
                {
                    float critrolltotal = randomnumber(1, 100) + unitcritmod;
                    if (critrolltotal >= 90f)
                    {
                        totaldamage = unitdamage * 2;
                        
                    }
                    else { totaldamage = unitdamage; }
                    encounters[tempi, tempj].encounterlife -= totaldamage;
                    playerNOA -= 1;
                    attacker = "opponent";
                    marks += 1;
                }
                else
                {
                    marks += 1;
                    playerNOA -= 1;
                    attacker = "opponent";
                }
            }
            if(attacker =="opponent")
            {
                
                if(opponentNOA<=1)
                {
                    attacker = "player";
                }
                totalattack = randomnumber(1, 100);
                totaldefense = randomnumber(1, 100) + unitdefense+empirebonus;
                if (totalattack >= totaldefense)
                {
                    mark = "opponent";
                    float critrolltotal = randomnumber(1, 100);
                    if (critrolltotal >= 90f)
                    {
                        totaldamage = encounters[tempi,tempj].encounterdamage * 2;
                    }
                    else { totaldamage = unitdamage; }
                    unitlife -= totaldamage;
                    opponentNOA -= 1;
                    attacker = "player";
                    marks += 1;
                }
                else
                {
                    opponentNOA -= 1;
                    attacker = "player";
                    marks += 1;
                }
            }
            if(unitlife<=1)
            {
                playergenerallist[pcount].army-=1;
                if(playergenerallist[pcount].army==0)
                {
                    for (int i = 0; i < playergenerallist.Count; i++)
                    {
                       
                        sprite[i].Destination = (new Vector2(0, 0));
                        sprite[i].Position = (new Vector2(600 + (32 * createMap.castlex), 50 + (32 * createMap.castley)));


                    }
                    attacker = "null";
                }
                casualties += 1;
                Game1.playergenerallist[pcount].unitalive = false;
            }
           if(encounters[tempi, tempj].encounterlife<=1)
            {
                temparmy=encounters[tempi, tempj].Encounterarmy-1;
                playergenerallist[pcount].encounterarmy -= 1;
                kills += 1;
                if (temparmy <= 0)
                {
                    opponents[tempi, tempj] = 0;
                    attacker = "null";
                }
                Enemy.createlocation(50, 150, 5, 20, 5, 20, 10, 25, 1, 3, 95, 100, 1, 5, tempi, tempj,temparmy);
                encounters[tempi, tempj].Encounterlife = Enemy.encounterlife;
                encounters[tempi, tempj].Encounteroffense = Enemy.encounteroffense;
                encounters[tempi, tempj].Encounterdefense = Enemy.encounterdefense;
                encounters[tempi, tempj].Encounterdamage = Enemy.encounterdamage;
                encounters[tempi, tempj].EncounterNOA = Enemy.encounterNOA;
                encounters[tempi, tempj].Encountercritrange = Enemy.encountercritrange;
                encounters[tempi, tempj].Encountercritmod = Enemy.encountercritmod;
                encounters[tempi, tempj].Encounterarmy = Enemy.encounterarmy;
            }
        }
        public void generateunit()
        {
            unitlife = (playergenerallist[MouseCord.generalcount].life + (one.commerce * .01f)) * generalmod;
            unitoffense = (playergenerallist[MouseCord.generalcount].offense + ((one.military + one.commerce) * .03f)) * generalmod;
            unitdefense = (playergenerallist[MouseCord.generalcount].defense + ((one.military + one.commerce) * .01f)) * generalmod;
            unitdamage = (playergenerallist[MouseCord.generalcount].damage * (one.military * .01f)) * generalmod;
            unitNOA = playergenerallist[MouseCord.generalcount].noa + generalmod;
            unitcritrange = playergenerallist[MouseCord.generalcount].critrange;
            unitcritmod = playergenerallist[MouseCord.generalcount].critmod;
        }
       
        
       
        public float randomnumber(int minnum, int maxnum)
        {
            
            numtemp = (float)Math.Round(random.NextDouble() * (maxnum - minnum) + minnum, 2);
            return numtemp;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            if (playergenerallist.Count != 0 )
            {
                
                spriteBatch.DrawString(MSGothic31, sec3.timer.ToString(), new Vector2(1400, 230), Color.Red); //timer string
                spriteBatch.DrawString(MSGothic31, "mouse x:" + mouse.m_mousePos.X.ToString(), new Vector2(1400, 25), Color.Red);
                spriteBatch.DrawString(MSGothic31, "mouse y:" + mouse.m_mousePos.Y.ToString(), new Vector2(1400, 55), Color.Red);
                spriteBatch.DrawString(Corsiva20, "sprite Position.X: " + sprite[MouseCord.generalcount].Position.X, new Vector2(1400, 100), Color.Red);
                spriteBatch.DrawString(Corsiva30, "sprite Position.Y: " + sprite[MouseCord.generalcount].Position.Y, new Vector2(1400, 120), Color.Red);
                spriteBatch.DrawString(MSGothic31, "clickstate: " + mouse.clickstate.ToString(), new Vector2(1400, 140), Color.Red);
                spriteBatch.DrawString(Corsiva30, "castleX: " + createMap.castlex, new Vector2(1390, 170), Color.Red);
                spriteBatch.DrawString(Corsiva30, "castleY: " +createMap.castley, new Vector2(1390, 200), Color.Red);
                spriteBatch.DrawString(Corsiva30, "General Count: " + MouseCord.generalcount, new Vector2(1390, 260), Color.Red);
                spriteBatch.DrawString(Corsiva30, "spriteX: " + sprite[0].Destination.X, new Vector2(1390, 330), Color.Red);
                spriteBatch.DrawString(Corsiva30, "spriteY: " + sprite[0].Destination.Y, new Vector2(1390, 370), Color.Red);
            }
                //DrawBorder(new Rectangle(Convert.ToInt32(mouse.m_mousePos.X) - 25, Convert.ToInt32(mouse.m_mousePos.Y) - 25, 50, 50), 3, Color.Yellow);
                if (gamestate == "kingmaker")
            {


                if (one.social != 0)
                {
                    DrawEmpire(700, 300);
                    drawconfirmbtn(650, 600);
                    drawrollbtn(960, 600);
                }
                else
                {
                    DrawEmpire(700, 300);
                    drawrollbtn(800, 600);
                }

            }
            if (gamestate == "generalmaker")
            {
                //drawrollbtn(300, 600);
                
                DrawEmpire(50, 50);
                DrawGeneral(600, 100);
                DrawPrevButton(600, 700);
                DrawNextButton(1050, 700);
                drawconfirmbtn(780, 700);
            }
            if (gamestate == "main")
            {
                
                //drawrollbtn(300, 600);
                DrawEmpire(50, 50);
                DrawGeneral(50, 330);
                gamemap.drawgrid(600, 50, 32);
                gamemap.checkgrid();
                drawconfirmbtn(50, 820);
               
                for (int i = 0; i < playergenerallist.Count; i++)
                {
                    spriteBatch.Draw(sprite[i].Texture, sprite[i].Position, sprite[i].SourceRect, Color.White, 0f, sprite[i].Origin, 1.0f, SpriteEffects.None, 0);
                    Initiative();
                }
                    if ( hiregenerallist.Count>1)
                {
                    Drawbuygeneral(300, 300);
                }

                



                if (playergenerallist.Count>1)
                {

                    DrawPrevButton(40, 730);
                    DrawNextButton(190, 725);
                }

                spriteBatch.DrawString(Corsiva30, "Opponents Army: " + encounters[tempi, tempj].encounterarmy, new Vector2(1390, 280), Color.Red);
                spriteBatch.DrawString(Corsiva30, " Enemy life: " + encounters[tempi, tempj].Encounterlife, Mousexlabelpos2 = new Vector2(1405, 500), Color.Red);
                spriteBatch.DrawString(Corsiva30, " Offense: " + encounters[tempi, tempj].encounterdefense.ToString(), Mousexlabelpos2 = new Vector2(1405, 530), Color.Red);
                spriteBatch.DrawString(Corsiva30, " Defense: " + encounters[tempi, tempj].encounterdefense.ToString(), Mousexlabelpos2 = new Vector2(1405, 560), Color.Red);
                spriteBatch.DrawString(Corsiva30, " Damage: " + encounters[tempi, tempj].encounterdamage.ToString(), Mousexlabelpos2 = new Vector2(1405, 590), Color.Red);
                spriteBatch.DrawString(Corsiva30, " NOA: " + encounters[tempi, tempj].encounterNOA.ToString(), Mousexlabelpos2 = new Vector2(1405, 620), Color.Red);
                spriteBatch.DrawString(Corsiva30, " Crit Range: " + encounters[tempi, tempj].encountercritrange.ToString(), Mousexlabelpos2 = new Vector2(1405, 650), Color.Red);
                spriteBatch.DrawString(Corsiva30, " Crit mod: " + encounters[tempi, tempj].encountercritmod.ToString(), Mousexlabelpos2 = new Vector2(1405,680), Color.Red);

                spriteBatch.DrawString(Corsiva30, " Unit life: " + unitlife.ToString(), Mousexlabelpos2 = new Vector2(1405, 710), Color.Yellow);
                spriteBatch.DrawString(Corsiva30, " unit Offense: " + unitoffense.ToString(), Mousexlabelpos2 = new Vector2(1405, 740), Color.Yellow);
                spriteBatch.DrawString(Corsiva30, " unit Defense: " + unitdefense.ToString(), Mousexlabelpos2 = new Vector2(1405, 770), Color.Yellow);
                spriteBatch.DrawString(Corsiva30, " unit Damage: " + unitdamage.ToString(), Mousexlabelpos2 = new Vector2(1405, 800), Color.Yellow);
                spriteBatch.DrawString(Corsiva30, " unit NOA: " + unitNOA.ToString(), Mousexlabelpos2 = new Vector2(1405, 830), Color.Yellow);
                spriteBatch.DrawString(Corsiva30, " unit Crit Range: " + unitcritrange.ToString(), Mousexlabelpos2 = new Vector2(1405, 860), Color.Yellow);
                spriteBatch.DrawString(Corsiva30, " unit Crit mod: " + unitcritmod.ToString(), Mousexlabelpos2 = new Vector2(1405, 890), Color.Yellow);
                spriteBatch.DrawString(Corsiva30, " general mod: " + playerinitiativeroll.ToString(), Mousexlabelpos2 = new Vector2(1405, 920), Color.Yellow);
                spriteBatch.DrawString(Corsiva30, " Units Killed: " + casualties + "| " +  "Opponents Killed: "+kills+ " | " + "Total Attacks"  + marks, Mousexlabelpos2 = new Vector2(205, 900), Color.Yellow);
             
                
                
                
            }
            if (gamestate == "buygeneral")
            {
                DrawPrevButton(600, 700);
                DrawNextButton(1050, 700);
                DrawGeneral(550, 50);
                Drawbuygeneral(835, 715);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        
        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);
            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder), rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder, rectangleToDraw.Width, thicknessOfBorder), borderColor);
        }

        public void drawrollbtn(int rollboxX, int rollboxY)
        {


            if (gamestate == "kingmaker")
            {

                DrawBorder(rollbutton = new Rectangle(rollboxX, rollboxY, 170, 70), 3, Color.Yellow);
                spriteBatch.DrawString(MSGothic31, "Roll", new Vector2(rollboxX + 37, rollboxY + 6), Color.LawnGreen);

                if (mouse.hover == "roll")
                {
                    DrawBorder(rollbutton, 50, Color.Ivory);
                    spriteBatch.DrawString(MSGothic31, "Roll", new Vector2(rollboxX + 37, rollboxY + 6), Color.Blue);

                }
                if (mouse.clickstate == "roll")
                {
                    //rollbox.X = (Convert.ToInt32((float)Math.Round(random.NextDouble() * (1300 - 10) + 10, 2)));
                    // rollbox.Y = (Convert.ToInt32((float)Math.Round(random.NextDouble() * (800 - 10) + 10, 2)));

                    one.assignnumbers();
                    one.setland();
                    mouse.clickstate = "null";
                    MouseCord.mousetf = false;
                }
            }
        }
        public void drawconfirmbtn(int confirmboxX, int confirmboxY)
        {


            if (gamestate == "kingmaker" )
            {

                DrawBorder(confirmbutton = new Rectangle(confirmboxX, confirmboxY, 180, 70), 3, Color.Yellow);
                spriteBatch.DrawString(MSGothic31, "Confirm", new Vector2(confirmboxX, confirmboxY + 6), Color.LawnGreen);
                if (mouse.hover == "confirm")
                {
                    DrawBorder(confirmbutton, 50, Color.Ivory);
                    spriteBatch.DrawString(MSGothic31, "Confirm", new Vector2(confirmboxX, confirmboxY + 6), Color.Blue);

                }
                if (mouse.clickstate == "confirm" && gamestate != "main")
                {

                    //confirmbox.X = (Convert.ToInt32((float)Math.Round(random.NextDouble() * (1300 - 10) + 10, 2)));
                    // confirmbox.Y = (Convert.ToInt32((float)Math.Round(random.NextDouble() * (800 - 10) + 10, 2)));
                    
                    gamestate = "generalmaker";
                    mouse.clickstate = "null";
                    MouseCord.mousetf = false;
                }
            }
            else if (gamestate == "generalmaker")
            {
                DrawBorder(confirmbutton = new Rectangle(confirmboxX, confirmboxY, 180, 70), 3, Color.Yellow);
                spriteBatch.DrawString(MSGothic31, "Confirm", new Vector2(confirmboxX, confirmboxY + 6), Color.LawnGreen);
                if (mouse.hover == "confirm")
                {
                    DrawBorder(confirmbutton, 50, Color.Ivory);
                    spriteBatch.DrawString(MSGothic31, "Confirm", new Vector2(confirmboxX, confirmboxY + 6), Color.Blue);

                }
                if (mouse.clickstate == "confirm")
                {
                    MouseCord.generalcount = 0;
                    gamestate = "main";
                    mouse.clickstate = "null";
                    MouseCord.mousetf = false;
                }
            }
            else if (gamestate=="main")
            {
                DrawBorder(confirmbutton = new Rectangle(confirmboxX, confirmboxY, 180, 70), 3, Color.Yellow);
                spriteBatch.DrawString(MSGothic31, "Confirm", new Vector2(confirmboxX, confirmboxY + 6), Color.LawnGreen);
                if (mouse.hover == "confirm")
                {
                    DrawBorder(confirmbutton, 50, Color.Ivory);
                    spriteBatch.DrawString(MSGothic31, "Confirm", new Vector2(confirmboxX, confirmboxY + 6), Color.Blue);

                }
                if (mouse.clickstate == "confirm")
                {
                    createMap.mark = false;
                    tempstate = 0;
                  
                    playergenerallist[MouseCord.generalcount].encounter = 0;
                   
                   
                    MouseCord.generalcount = 0;
                    mouse.clickstate = "null";
                    MouseCord.mousetf = false;
                    
                }
            }
        }
        public void DrawPrevButton(int prevboxX, int prevboxY)
        {
            if (gamestate == "generalmaker" || gamestate == "main"||gamestate=="buygeneral")
            {
                if (mouse.hover == "previous")
                {
                    DrawBorder(previousbutton = new Rectangle(prevboxX, prevboxY, 110, 70), 3, Color.Transparent);
                    spriteBatch.Draw(lrgleft, new Vector2(prevboxX - 20, prevboxY - 15), Color.White);
                    spriteBatch.DrawString(Corsiva30, "Previous", new Vector2(prevboxX - 10, prevboxY + 50), Color.DarkBlue);
                }
                else
                {
                    DrawBorder(previousbutton = new Rectangle(prevboxX, prevboxY, 100, 70), 3, Color.Transparent);
                    spriteBatch.Draw(smleft, new Vector2(prevboxX, prevboxY), Color.White);
                    spriteBatch.DrawString(Corsiva20, "Previous", new Vector2(prevboxX + 10, prevboxY + 50), Color.DarkBlue);
                }

                //spriteBatch.DrawString(MSGothic31, "Previous", new Vector2(prevboxX, prevboxY + 6), Color.LawnGreen);
            }
        }
        public void DrawNextButton(int nextboxX, int nextboxY)
        {

            if (gamestate == "generalmaker" || gamestate == "main" || gamestate == "buygeneral")
            {
                if (mouse.hover == "next")
                {
                    DrawBorder(nextbutton = new Rectangle(nextboxX, nextboxY, 110, 70), 3, Color.Transparent);
                    spriteBatch.Draw(lrgright, new Vector2(nextboxX - 20, nextboxY - 15), Color.White);
                    spriteBatch.DrawString(Corsiva30, "Next", new Vector2(nextboxX - 10, nextboxY + 50), Color.DarkBlue);
                }
                else
                {
                    DrawBorder(nextbutton = new Rectangle(nextboxX, nextboxY, 100, 70), 3, Color.Transparent);
                    spriteBatch.Draw(smright, new Vector2(nextboxX + 10, nextboxY), Color.White);
                    spriteBatch.DrawString(Corsiva20, "Next", new Vector2(nextboxX + 25, nextboxY + 50), Color.DarkBlue);
                }
            }
        }
        public void Drawbuygeneral(int buygeneralboxX, int buygeneralboxY)
        {

            DrawBorder(buygeneralbutton = new Rectangle(buygeneralboxX, buygeneralboxY, 180, 70), 3, Color.Yellow);
            spriteBatch.DrawString(Corsiva30, "Buy", new Vector2(buygeneralboxX+65, buygeneralboxY-10), Color.LawnGreen);
            spriteBatch.DrawString(Corsiva30, "General", new Vector2(buygeneralboxX+30, buygeneralboxY+20 ), Color.LawnGreen);
            if (mouse.hover == "buygeneral")
            {
                DrawBorder(buygeneralbutton, 50, Color.Ivory);
                spriteBatch.DrawString(Corsiva30, "Buy", new Vector2(buygeneralboxX + 65, buygeneralboxY - 10), Color.Blue);
                spriteBatch.DrawString(Corsiva30, "General", new Vector2(buygeneralboxX + 30, buygeneralboxY + 20), Color.Blue);

            }
          
            if (mouse.clickstate == "buygeneral")
            {
                
                MouseCord.generalcount = 0;
                gamestate = "buygeneral";
                mouse.clickstate = "null";
                MouseCord.mousetf = false;

            }
            if (mouse.clickstate == "main")
            {
                playergenerallist.Add(hiregenerallist[MouseCord.generalcount]);
                hiregenerallist.RemoveAt(MouseCord.generalcount);
                MouseCord.generalcount = 0;
                gamestate = "main";
                mouse.clickstate = "null";
                MouseCord.mousetf = false;

            }
            if (mouse.clickstate=="main"&&gamestate=="buygeneral")
            {
               
            }
        }
        public void DrawEmpire(int empireboxX, int empireboxY)
        {

            if (gamestate == "kingmaker")
            {
                DrawBorder(empireRec = new Rectangle(empireboxX, empireboxY, 385, 270), 3, Color.Red);
                spriteBatch.DrawString(Corsiva40, "Kingdom ", new Vector2(empireboxX + 60, empireboxY - 50), Color.Red);
                spriteBatch.DrawString(Corsiva40, "Commerce", new Vector2(empireboxX + 20, empireboxY), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Military", new Vector2(empireboxX + 20, empireboxY + 50), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Education", new Vector2(empireboxX + 20, empireboxY + 100), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Workforce", new Vector2(empireboxX + 20, empireboxY + 150), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Social", new Vector2(empireboxX + 20, empireboxY + 200), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, one.commerce.ToString(), new Vector2(empireboxX + 250, empireboxY), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, one.military.ToString(), new Vector2(empireboxX + 250, empireboxY + 50), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, one.education.ToString(), new Vector2(empireboxX + 250, empireboxY + 100), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, one.workforce.ToString(), new Vector2(empireboxX + 250, empireboxY + 150), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, one.social.ToString(), new Vector2(empireboxX + 250, empireboxY + 200), Color.LawnGreen);
            }
            if (gamestate == "generalmaker" || gamestate == "main")
            {
                DrawBorder(empireRec = new Rectangle(empireboxX, empireboxY, 230, 220), 3, Color.Red);
                spriteBatch.DrawString(Corsiva30, "Kingdom ", new Vector2(empireboxX + 40, empireboxY - 50), Color.Red);
                spriteBatch.DrawString(Corsiva20, "Commerce", new Vector2(empireboxX + 20, empireboxY), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Military", new Vector2(empireboxX + 20, empireboxY + 30), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Education", new Vector2(empireboxX + 20, empireboxY + 60), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Workforce", new Vector2(empireboxX + 20, empireboxY + 90), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Social", new Vector2(empireboxX + 20, empireboxY + 120), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Land ", new Vector2(empireboxX + 20, empireboxY + 150), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Gold ", new Vector2(empireboxX + 20, empireboxY + 180), Color.LawnGreen);

                spriteBatch.DrawString(Corsiva20, one.commerce.ToString(), new Vector2(empireboxX + 150, empireboxY), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, one.military.ToString(), new Vector2(empireboxX + 150, empireboxY + 30), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, one.education.ToString(), new Vector2(empireboxX + 150, empireboxY + 60), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, one.workforce.ToString(), new Vector2(empireboxX + 150, empireboxY + 90), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, one.social.ToString(), new Vector2(empireboxX + 150, empireboxY + 120), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, one.land.ToString(), new Vector2(empireboxX + 150, empireboxY + 150), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, one.gold.ToString(), new Vector2(empireboxX + 150, empireboxY + 180), Color.LawnGreen);
            }
        }

        public void DrawGeneral(int generalboxX, int generalboxY)
        {
            if (gamestate == "generalmaker")
            {
                DrawBorder(new Rectangle(generalboxX, generalboxY, 550, 600), 2, Color.Yellow);
                DrawBorder(new Rectangle(generalboxX + 480, generalboxY + 5, 64, 64), 40, Color.Black);
                spriteBatch.DrawString(Corsiva40, "General", new Vector2(generalboxX + 170, generalboxY - 50), Color.Red);
                spriteBatch.DrawString(Corsiva40, "Name ", new Vector2(generalboxX + 20, generalboxY + 20), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Class", new Vector2(generalboxX + 20, generalboxY + 60), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Life", new Vector2(generalboxX + 20, generalboxY + 100), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Speed", new Vector2(generalboxX + 20, generalboxY + 140), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Offense", new Vector2(generalboxX + 20, generalboxY + 180), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Defense", new Vector2(generalboxX + 20, generalboxY + 220), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Damage", new Vector2(generalboxX + 20, generalboxY + 260), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Number of Attacks", new Vector2(generalboxX + 20, generalboxY + 300), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Critical Range", new Vector2(generalboxX + 20, generalboxY + 340), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Critical Modifier", new Vector2(generalboxX + 20, generalboxY + 380), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Army Size", new Vector2(generalboxX + 20, generalboxY + 420), Color.LawnGreen);

                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].generalname.ToString(), new Vector2(generalboxX + 350, generalboxY + 20), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].classprof.ToString(), new Vector2(generalboxX + 350, generalboxY + 60), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].life.ToString(), new Vector2(generalboxX + 400, generalboxY + 100), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].speed.ToString(), new Vector2(generalboxX + 400, generalboxY + 140), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].offense.ToString(), new Vector2(generalboxX + 400, generalboxY + 180), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].defense.ToString(), new Vector2(generalboxX + 400, generalboxY + 220), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].damage.ToString(), new Vector2(generalboxX + 400, generalboxY + 260), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].noa.ToString(), new Vector2(generalboxX + 400, generalboxY + 300), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].critrange.ToString(), new Vector2(generalboxX + 400, generalboxY + 340), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].critmod.ToString(), new Vector2(generalboxX + 400, generalboxY + 380), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].army.ToString(), new Vector2(generalboxX + 400, generalboxY + 420), Color.LawnGreen);
               // spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].spritename.ToString(), new Vector2(generalboxX + 400, generalboxY + 450), Color.Green);
                spriteBatch.Draw(generallist[MouseCord.generalcount].spritename, new Vector2(generalboxX + 485, generalboxY + 10), Color.White);
                if (mouse.clickstate == "confirm")
                {
                    playergenerallist.Add(generallist[MouseCord.generalcount]);
                    hiregenerallist.RemoveAt(MouseCord.generalcount);
                    MouseCord.generalcount=0;
                }

            }
            else if (gamestate == "main")
            {
                DrawBorder(new Rectangle(generalboxX, generalboxY, 230, 400), 2, Color.Yellow);
                //DrawBorder(new Rectangle(generalboxX + 480, generalboxY + 5, 64, 64), 40, Color.Black);
                spriteBatch.DrawString(Corsiva30, "General", new Vector2(generalboxX + 60, generalboxY - 50), Color.Red);
                spriteBatch.DrawString(Corsiva20, "Name ", new Vector2(generalboxX + 20, generalboxY), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Class", new Vector2(generalboxX + 20, generalboxY + 30), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Life", new Vector2(generalboxX + 20, generalboxY + 60), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Speed", new Vector2(generalboxX + 20, generalboxY + 90), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Offense", new Vector2(generalboxX + 20, generalboxY + 120), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Defense", new Vector2(generalboxX + 20, generalboxY + 150), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Damage", new Vector2(generalboxX + 20, generalboxY + 180), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva15, "Number of Attacks", new Vector2(generalboxX + 20, generalboxY + 215), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva15, "Critical Range", new Vector2(generalboxX + 20, generalboxY + 245), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva15, "Critical Modifier", new Vector2(generalboxX + 20, generalboxY + 275), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, "Army Size", new Vector2(generalboxX + 20, generalboxY + 300), Color.LawnGreen);

                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].generalname.ToString(), new Vector2(generalboxX + 130, generalboxY), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].classprof.ToString(), new Vector2(generalboxX + 130, generalboxY + 30), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].life.ToString(), new Vector2(generalboxX + 160, generalboxY + 60), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].speed.ToString(), new Vector2(generalboxX + 160, generalboxY + 90), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].offense.ToString(), new Vector2(generalboxX + 160, generalboxY + 120), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].defense.ToString(), new Vector2(generalboxX + 160, generalboxY + 150), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].damage.ToString(), new Vector2(generalboxX + 160, generalboxY + 180), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].noa.ToString(), new Vector2(generalboxX + 160, generalboxY + 210), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].critrange.ToString(), new Vector2(generalboxX + 160, generalboxY + 240), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].critmod.ToString(), new Vector2(generalboxX + 160, generalboxY + 270), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva20, playergenerallist[MouseCord.generalcount].army.ToString(), new Vector2(generalboxX + 160, generalboxY + 300), Color.LawnGreen);
                // spriteBatch.Draw(generallist[generalcount].spritename, new Vector2(generalboxX + 485, generalboxY + 10), Color.White);            
            }
            else if (gamestate == "buygeneral" )
            {
                DrawBorder(new Rectangle(generalboxX, generalboxY, 550, 600), 2, Color.Yellow);
                DrawBorder(new Rectangle(generalboxX + 480, generalboxY + 5, 64, 64), 40, Color.Black);
                spriteBatch.DrawString(Corsiva40, "General", new Vector2(generalboxX + 170, generalboxY - 50), Color.Red);
                spriteBatch.DrawString(Corsiva40, "Name ", new Vector2(generalboxX + 20, generalboxY + 20), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Class", new Vector2(generalboxX + 20, generalboxY + 60), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Life", new Vector2(generalboxX + 20, generalboxY + 100), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Speed", new Vector2(generalboxX + 20, generalboxY + 140), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Offense", new Vector2(generalboxX + 20, generalboxY + 180), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Defense", new Vector2(generalboxX + 20, generalboxY + 220), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Damage", new Vector2(generalboxX + 20, generalboxY + 260), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Number of Attacks", new Vector2(generalboxX + 20, generalboxY + 300), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Critical Range", new Vector2(generalboxX + 20, generalboxY + 340), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Critical Modifier", new Vector2(generalboxX + 20, generalboxY + 380), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, "Army Size", new Vector2(generalboxX + 20, generalboxY + 420), Color.LawnGreen);

                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].generalname.ToString(), new Vector2(generalboxX + 350, generalboxY + 20), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].classprof.ToString(), new Vector2(generalboxX + 350, generalboxY + 60), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].life.ToString(), new Vector2(generalboxX + 400, generalboxY + 100), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].speed.ToString(), new Vector2(generalboxX + 400, generalboxY + 140), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].offense.ToString(), new Vector2(generalboxX + 400, generalboxY + 180), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].defense.ToString(), new Vector2(generalboxX + 400, generalboxY + 220), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].damage.ToString(), new Vector2(generalboxX + 400, generalboxY + 260), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].noa.ToString(), new Vector2(generalboxX + 400, generalboxY + 300), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].critrange.ToString(), new Vector2(generalboxX + 400, generalboxY + 340), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].critmod.ToString(), new Vector2(generalboxX + 400, generalboxY + 380), Color.LawnGreen);
                spriteBatch.DrawString(Corsiva40, hiregenerallist[MouseCord.generalcount].army.ToString(), new Vector2(generalboxX + 400, generalboxY + 420), Color.LawnGreen);
                //spriteBatch.DrawString(Corsiva40, generallist[MouseCord.generalcount].spritename.ToString(), new Vector2(generalboxX + 400, generalboxY + 450), Color.Green);
                spriteBatch.Draw(hiregenerallist[MouseCord.generalcount].spritename, new Vector2(generalboxX + 485, generalboxY + 10), Color.White);
                
            }
            }
        }
}
