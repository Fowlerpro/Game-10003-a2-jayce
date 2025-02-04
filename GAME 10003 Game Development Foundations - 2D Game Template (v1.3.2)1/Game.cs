// Include the namespaces (code libraries) you need below.
using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        //Tank color
        //https://lospec.com/palette-list/autumn-decay site colors grabbed from
        Color deepGray = new Color("313638");
        //ground color
        Color Tan = new Color("ffe596");
        //Catus color
        Color paleBrown = new Color("574729");
        //sky
        Color brightOrange = new Color("ffad3b");
        //clouds
        Color deepOrange = new Color("975330");
        //dirt
        int dirtCount = 30;
        int[] dirtPositionsX;
        int[] dirtPositionsY;
        //incoming bullets now dust particles
        int bulletCount = 30;
        int[] bulletPositionsX;
        int[] bulletPositionsY;
        float bulletSpeed = 200;
        Vector2[] bulletPositions;

        //tried getting tracks to move
        //Vector2[] tracks = new Vector2[9];
        //int tTL = 160;
        //int tTH = 110;
        //Vector2 trackSize = new Vector2(10, 10);
        //bulet variables 
        float buletSpeed = 100;
        float buletX = -1;
        float shotCooldown = 0.3f;
        float shotResetTimer = 0.0f;
        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetTitle("Tankgame");
            Window.SetSize(400, 400);

            Draw.LineSize = 0;
            bulletPositions = new Vector2[bulletCount];
            for (int i = 0; i < bulletCount; i++)
            {
                bulletPositions[i].X = Random.Integer(400, 800);
                bulletPositions[i].Y = Random.Integer(0, 300);
            }
            //Dirt Count
            dirtPositionsX = new int[dirtCount];
            dirtPositionsY = new int[dirtCount];
            Draw.FillColor = paleBrown;
            //for (int i = 0; i < tracks.Length; i++)
            //{
            //    Vector2 trackPosition = new Vector2(tTL, tTH);
            //    Draw.Rectangle(trackPosition, trackSize);
            //    tTL += 20;
            //}
            for (int i = 0; i < dirtCount; i++)
            {
                dirtPositionsX[i] = Random.Integer(0, 400);
                dirtPositionsY[i] = Random.Integer(202, 400);
            }

        }


        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Tan);
            //dirt
            Draw.LineSize = 0;
            Draw.FillColor = deepGray;
            for (int i = 0; i < dirtCount; i++)
            {
                Draw.Circle(dirtPositionsX[i], dirtPositionsY[i], 5);
            }
            //sky
            Draw.FillColor = brightOrange;
            Draw.Rectangle(0, 0, 400, 240);
            //clouds
            Draw.FillColor = deepOrange;
            for (int i = 0; i < 10; i++)
            {
                int x = 30 + i * 100;
                Draw.Circle(x, Window.Height - 400, 75);

            }
            //incoming bullets
            void bullets()
            {
                for (int i = 0; i < bulletPositions.Length; i++)
                {
                    if (bulletPositions[i].X < -50)
                    {
                        bulletPositions[i].X = 400;
                    }
                    Draw.FillColor = Tan;
                    bulletPositions[i].X -= Time.DeltaTime * bulletSpeed;
                    Draw.Circle(bulletPositions[i].X, bulletPositions[i].Y, 2);
                }
            }
            bullets();
            //TANK

            //body
            Draw.FillColor = deepGray;
            Draw.Rectangle(100, 170, 180, 50);
            Draw.Rectangle(110, 160, 160, 10);
            //turret
            Draw.Rectangle(130, 130, 100, 30);
            Draw.FillColor = Color.LightGray;
            Draw.Rectangle(230, 140, 30, 10);
            Draw.FillColor = deepGray;
            Draw.Rectangle(130, 140, 100, 20);
            Draw.Rectangle(150, 120, 50, 10);
            //Top Tracks
            Draw.FillColor = Color.Black;
            Draw.Rectangle(100, 170, 180, 10);
            //Bottom Tracks
            Draw.Rectangle(100, 220, 180, 10);
            //Side Tracks Left
            Draw.Rectangle(90, 180, 10, 40);
            //Side Tracks Right
            Draw.Rectangle(280, 180, 10, 40);
            //Moving parts
            //Moving top Tracks could not figure out
            //for (int i = 0;i < tracks.Length;i++)
            //{

            //    Draw.Rectangle(tracks[i], trackSize);

            //    for (int x = 0;x < tracks.Length;x++)
            //    {
            //        tracks[x].X = +20;
            //        tracks[x].Y = 160;
            //    }
            //};
            Draw.Rectangle(110, 160, 10, 10);
            Draw.Rectangle(130, 160, 10, 10);
            Draw.Rectangle(150, 160, 10, 10);
            Draw.Rectangle(170, 160, 10, 10);
            Draw.Rectangle(190, 160, 10, 10);
            Draw.Rectangle(210, 160, 10, 10);
            Draw.Rectangle(230, 160, 10, 10);
            Draw.Rectangle(250, 160, 10, 10);
            Draw.Rectangle(270, 160, 10, 10);
            //Moving Bottom Tracks
            Draw.Rectangle(100, 230, 10, 10);
            Draw.Rectangle(120, 230, 10, 10);
            Draw.Rectangle(140, 230, 10, 10);
            Draw.Rectangle(160, 230, 10, 10);
            Draw.Rectangle(180, 230, 10, 10);
            Draw.Rectangle(200, 230, 10, 10);
            Draw.Rectangle(220, 230, 10, 10);
            Draw.Rectangle(240, 230, 10, 10);
            Draw.Rectangle(260, 230, 10, 10);
            //Moving Side Tracks Left
            Draw.Rectangle(80, 190, 10, 10);
            Draw.Rectangle(80, 210, 10, 10);
            //Moving Side Tracks Right
            Draw.Rectangle(290, 180, 10, 10);
            Draw.Rectangle(290, 200, 10, 10);
            //bullet
            if (buletX != -1)
            {
                buletX += Time.DeltaTime * buletSpeed;
                // bulet is made
                Draw.LineSize = 4;
                Draw.FillColor = paleBrown;
                Draw.Circle(buletX, 145, 10);
            }
            // checks when the bulet goes of screen
            if (buletX > Window.Width)
            {
                buletX = -1;
            }
            // actually shoots the bullet
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Space) && shotCooldown <= 0.3f)
            {
                shot();
                shotResetTimer = shotCooldown;
            }
            shotCooldown -= Time.DeltaTime;
            
        }
        //function that resets the bulet back to cannon barrel
        void shot()
                    {
            if (buletX == -1)
            {
                buletX = 250;
            }
            }
}
}

