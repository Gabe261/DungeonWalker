using DungeonWalker.Obstacles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonWalker.Components
{
    /// <summary>
    /// Room designer is a helper method designed to generate levels at random 
    /// Some levels might be a maze, others an obstacle course
    /// </summary>
    public class RoomDesigner : GameComponent
    {

        private Obstacle[,] room;
        private Game game;
        private Game1 g;
        private Vector2 startPos = Vector2.Zero;
        private Vector2 endPos = Vector2.Zero;
        private SpriteBatch sb;

        private Texture2D woodWall;
        private Texture2D voidTile;
        private Texture2D spikeTile;
        private Texture2D flameTile;
        private Texture2D pointTile;

        private const int VOID = 0, START = 1, END = 2, WALL = 3, SPIKE = 4, FLAME = 5, POINT = 6;
        private const int XQUAD1 = 2, XQUAD2 = 9, XQUAD3 = 16, XQUAD4 = 23;
        private const int YQUAD1 = 2, YQUAD2 = 9;
        private const int LEVEL_MIN = 1, LEVEL_MAX = 10;
        private const int ROOM_EDGE_BOT = 16, ROOM_EDGE_RIGHT = 30, WALL_DISPLACMENT = 2;

        /// <summary>
        /// Constructor for RoomDesigner sets all the classes variables and loads the obstacles starting image
        /// </summary>
        /// <param name="game"></param>
        public RoomDesigner(Game game) : base(game)
        {
            this.game = game;
            this.g = (Game1)game;
            this.sb = g._spriteBatch;
            this.room = new Obstacle[Shared.width, Shared.height];
            this.woodWall = g.Content.Load<Texture2D>("images/backgroud/WoodWall");
            this.voidTile = g.Content.Load<Texture2D>("images/backgroud/void");
            this.spikeTile = g.Content.Load<Texture2D>("images/backgroud/spikesIn");
            this.flameTile = g.Content.Load<Texture2D>("images/backgroud/flameIn");
            this.pointTile = g.Content.Load<Texture2D>("images/backgroud/Points");
        }

        /// <summary>
        /// GenerateRoom creates a room for the player to start and finish
        /// </summary>
        /// <returns> an 2D array of obstacles to be generated in each PlayScene </returns>
        public Obstacle[,] GenerateRoom()
        {
            Random r = new Random();
            int level = r.Next(LEVEL_MIN, LEVEL_MAX);

            LoadArrayToLevel(baseDesign);

            int[,] tempDesign = new int[baseDesign.GetLength(0), baseDesign.GetLength(1)];

            for (int i = 0; i < baseDesign.GetLength(0); i++)
            {
                for (int j = 0; j < baseDesign.GetLength(1); j++)
                {
                    tempDesign[i, j] = baseDesign[i, j];
                }
            }
            // Loads a setup 2D array to the obstables array
            switch (level)
            {
                case 1:
                    LoadArrayToLevel(mazeDesign);
                    break;
                case 2:
                    LoadArrayToLevel(mazeDesign2);
                    break;
                case 3:
                    
                    AddSection(XQUAD1, YQUAD1, START, tempDesign);
                    AddSection(XQUAD4, YQUAD2, END, tempDesign);
                    AddSection(XQUAD2, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD1, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD2, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD2, WALL, tempDesign);
                    LoadArrayToLevel(tempDesign);
                    break;
                case 4:
                    AddSection(XQUAD2, YQUAD1, START, tempDesign);
                    AddSection(XQUAD3, YQUAD2, END, tempDesign);
                    AddSection(XQUAD1, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD1, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD2, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD2, WALL, tempDesign);
                    LoadArrayToLevel(tempDesign);
                    break;
                case 5:
                    AddSection(XQUAD4, YQUAD2, START, tempDesign);
                    AddSection(XQUAD1, YQUAD1, END, tempDesign);
                    AddSection(XQUAD1, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD2, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD2, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD1, WALL, tempDesign);
                    LoadArrayToLevel(tempDesign);
                    break;
                case 6:
                    AddSection(XQUAD1, YQUAD1, START, tempDesign);
                    AddSection(XQUAD1, YQUAD2, END, tempDesign);
                    AddSection(XQUAD2, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD2, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD2, WALL, tempDesign);
                    LoadArrayToLevel(tempDesign);
                    break;
                case 7:
                    AddSection(XQUAD3, YQUAD2, START, tempDesign);
                    AddSection(XQUAD1, YQUAD2, END, tempDesign);
                    AddSection(XQUAD2, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD1, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD2, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD1, WALL, tempDesign);
                    LoadArrayToLevel(tempDesign);
                    break;
                case 8:
                    AddSection(XQUAD4, YQUAD1, START, tempDesign);
                    AddSection(XQUAD2, YQUAD2, END, tempDesign);
                    AddSection(XQUAD1, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD2, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD1, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD2, WALL, tempDesign);
                    LoadArrayToLevel(tempDesign);
                    break;
                case 9:
                    AddSection(XQUAD1, YQUAD1, START, tempDesign);
                    AddSection(XQUAD3, YQUAD1, END, tempDesign);
                    AddSection(XQUAD2, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD1, WALL, tempDesign);
                    AddSection(XQUAD1, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD2, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD3, YQUAD2, WALL, tempDesign);
                    AddSection(XQUAD4, YQUAD2, WALL, tempDesign);
                    LoadArrayToLevel(tempDesign);
                    break;
                default:
                    LoadArrayToLevel(mazeDesign);
                    break;
            }
            // Gives the room borders
            SquareEdges();

            return room;
        }

        /// <summary>
        /// Changes an int 2D array to a full 2D obstacle array the the playscenes can use to generate the level
        /// </summary>
        /// <param name="array"> The int array to be used as a template for the obstacle array </param>
        private void LoadArrayToLevel(int[,] array)
        {

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == VOID)
                    {
                        room[i, j] = new VoidTile(game, sb, voidTile, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth));
                    }
                    else if (array[i, j] == START)
                    {
                        startPos = new Vector2(i * Shared.tileWidth, j * Shared.tileWidth);
                        room[i, j] = new VoidTile(game, sb, voidTile, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth));
                    }
                    else if (array[i, j] == END)
                    {
                        endPos = new Vector2(i * Shared.tileWidth, j * Shared.tileWidth);
                        room[i, j] = new VoidTile(game, sb, voidTile, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth));
                    }
                    else if (array[i, j] == WALL)
                    {
                        room[i, j] = new Wall(game, sb, woodWall, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth));
                    }
                    else if (array[i, j] == SPIKE)
                    {
                        room[i, j] = new Spike(game, sb, spikeTile, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth));
                    }
                    else if (array[i, j] == FLAME)
                    {
                        room[i, j] = new Flame(game, sb, flameTile, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth), 10);
                    }
                    else if (array[i, j] == POINT)
                    {
                        room[i, j] = new Points(game, sb, pointTile, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth), 5);
                    }
                    else
                    {
                        room[i, j] = new VoidTile(game, sb, voidTile, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth));
                    }
                }
            }
        }

        /// <summary>
        /// Adds a section on an int array to a large temporary array to generate a small section on the level randomly
        /// </summary>
        /// <param name="roomStartX"> The x quadrant to generate the small section </param>
        /// <param name="roomStartY"> The y quadrant to generate the small section </param>
        /// <param name="sectionType"> either a start, end or obstacle block to generate </param>
        /// <param name="addTo"> The reference of the temp array that will be used as the templete </param>
        private void AddSection(int roomStartX, int roomStartY, int sectionType, int[,] addTo)
        {
            int[,] mazeSection = new int[7, 7];

            Random rs = new Random();
            int num = 0;
            switch (sectionType)
            {
                case START:
                    num = rs.Next(1, 3);
                    switch (num)
                    {
                        case 1:
                            mazeSection = startSection;
                            break;
                        case 2:
                            mazeSection = startSection2;
                            break;
                        default:
                            mazeSection = startSection;
                            break;
                    }
                    break;
                case END:
                    num = rs.Next(1, 3);
                    switch (num)
                    {
                        case 1:
                            mazeSection = endSection;
                            break;
                        case 2:
                            mazeSection = endSection2;
                            break;
                        default:
                            mazeSection = endSection;
                            break;
                    }
                    break;
                case WALL:
                    num = rs.Next(1, 8);
                    switch (num)
                    {
                        case 1:
                            mazeSection = wallSection;
                            break;
                        case 2:
                            mazeSection = wallSection2;
                            break;
                        case 3:
                            mazeSection = wallSection3;
                            break;
                        case 4:
                            mazeSection = wallSection4;
                            break;
                        case 5:
                            mazeSection = wallSection5;
                            break;
                        case 6:
                            mazeSection = wallSection6;
                            break;
                        case 7:
                            mazeSection = wallSection7;
                            break;
                        default:
                            mazeSection = wallSection;
                            break;
                    }
                    break;
            }

            for (int i = roomStartX; i < roomStartX + mazeSection.GetLength(0); i++)
            {
                for (int j = roomStartY; j < roomStartY + mazeSection.GetLength(1); j++)
                {
                    addTo[i, j] = mazeSection[i - roomStartX, j - roomStartY];
                }
            }
        }


        /// <summary>
        /// Makes walls all around the edge of the room to keep player in bounds
        /// </summary>
        private void SquareEdges()
        {
            int j = 0;

            j = 1;
            for (int i = 1; i < Shared.width - 1; i++) // Top
                room[i, j] = new Wall(game, sb, woodWall, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth));

            j = ROOM_EDGE_BOT;
            for (int i = 1; i < Shared.width - 1; i++) // Bottom
                room[i, j] = new Wall(game, sb, woodWall, new Vector2(i * Shared.tileWidth, j * Shared.tileWidth));

            j = 1;
            for (int i = WALL_DISPLACMENT; i < Shared.height - 1; i++) // Left
                room[j, i] = new Wall(game, sb, woodWall, new Vector2(j * Shared.tileWidth, i * Shared.tileWidth));

            j = ROOM_EDGE_RIGHT;
            for (int i = WALL_DISPLACMENT; i < Shared.height - 1; i++) // Right
                room[j, i] = new Wall(game, sb, woodWall, new Vector2(j * Shared.tileWidth, i * Shared.tileWidth));
        }

        /// <summary>
        /// A Getter method for the end position of the room
        /// </summary>
        /// <returns> the end position of the room </returns>
        public Vector2 GetEndPosition()
        {
            return endPos;
        }

        /// <summary>
        /// A Getter method for the start position of the room
        /// </summary>
        /// <returns> the start position of the room </returns>
        public Vector2 GetStartPosition()
        {
            return startPos;
        }

        // The 2D int arrays to generate the level; numbers are not magic but based of constants for easy human access
        // VOID = 0, START = 1, END = 2, WALL = 3, SPIKE = 4, FLAME = 5, POINT = 6
        private int[,] baseDesign =
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        private int[,] mazeDesign =
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 3, 0, 6, 6, 6, 6, 6, 6, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 3, 0, 3, 3, 3, 0, 3, 3, 3, 3, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 3, 3, 3, 3, 3, 0, 3, 3, 3, 0, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 3, 6, 0, 0 },
            { 0, 0, 0, 3, 3, 3, 0, 3, 3, 0, 3, 3, 3, 0, 3, 6, 0, 0 },
            { 0, 0, 6, 3, 0, 0, 0, 3, 0, 0, 0, 0, 3, 0, 3, 6, 0, 0 },
            { 0, 0, 6, 3, 0, 3, 0, 3, 0, 3, 6, 0, 3, 0, 3, 6, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 3, 6, 0, 3, 0, 3, 6, 0, 0 },
            { 0, 0, 3, 3, 3, 3, 3, 3, 3, 3, 6, 0, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 6, 6, 6, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 3, 3, 3, 0, 3, 3, 3, 3, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 3, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 3, 3, 0, 3, 0, 3, 0, 0, 3, 0, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 3, 0, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 3, 3, 0, 3, 0, 3, 3, 0, 3, 3, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 3, 0, 3, 0, 3, 3, 3, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 3, 3, 0, 3, 0, 3, 0, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0, 3, 0, 3, 0, 0, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 6, 3, 3, 3, 0, 3, 0, 0, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 6, 3, 0, 0, 0, 3, 0, 3, 3, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 6, 3, 6, 0, 0, 3, 0, 3, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 6, 3, 6, 3, 3, 3, 3, 3, 0, 3, 3, 3, 0, 0 },
            { 0, 0, 0, 3, 0, 3, 6, 3, 6, 6, 6, 3, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 3, 0, 3, 0, 3, 0, 3, 3, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 3, 0, 0, 0, 3, 0, 0, 6, 0, 0, 2, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        private int[,] mazeDesign2 =
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 6, 6, 6, 3, 6, 6, 3, 0, 0, 0, 3, 0, 6, 6, 0, 0 },
            { 0, 0, 6, 6, 6, 3, 6, 6, 3, 0, 3, 0, 3, 0, 6, 6, 0, 0 },
            { 0, 0, 6, 6, 6, 3, 6, 6, 3, 0, 3, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 6, 6, 6, 3, 0, 3, 3, 0, 3, 0, 3, 0, 3, 3, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 3, 0, 3, 0, 0, 3, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 3, 0, 3, 3, 0, 3, 3, 0, 3, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 3, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 3, 0, 3, 0, 3, 0, 1, 0, 3, 3, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 3, 0, 3, 0, 3, 0, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0, 3, 0, 3, 3, 3, 3, 3, 0, 3, 0, 0 },
            { 0, 0, 0, 3, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 3, 0, 3, 0, 3, 3, 3, 3, 3, 3, 3, 0, 3, 0, 0 },
            { 0, 0, 3, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 0, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 6, 6, 0, 0 },
            { 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 3, 0, 0, 0, 6, 6, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 6, 6, 0, 0 },
            { 0, 0, 0, 3, 3, 3, 3, 0, 3, 0, 3, 3, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 6, 6, 3, 0, 3, 0, 3, 0, 0, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 6, 6, 3, 0, 3, 0, 3, 0, 3, 3, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 6, 6, 3, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0 },
            { 0, 0, 3, 3, 3, 3, 3, 3, 3, 0, 3, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 6, 3, 0, 0, 3, 0, 3, 0, 3, 3, 3, 3, 3, 0, 0, 0 },
            { 0, 0, 6, 3, 2, 0, 3, 0, 3, 0, 3, 6, 6, 6, 3, 0, 0, 0 },
            { 0, 0, 6, 3, 0, 0, 0, 0, 3, 0, 3, 6, 6, 6, 3, 0, 0, 0 },
            { 0, 0, 6, 3, 3, 3, 3, 0, 3, 3, 3, 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 6, 3, 0, 0, 0, 0, 0, 0, 3, 3, 0, 3, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        private int[,] startSection = {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };
        private int[,] startSection2 = {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0 },
            { 0, 3, 3, 0, 3, 0, 0 },
            { 0, 0, 0, 1, 0, 3, 0 },
            { 0, 3, 3, 0, 3, 0, 0 },
            { 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };

        private int[,] endSection = {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };
        private int[,] endSection2 = {
            { 0, 0, 4, 4, 4, 0, 0 },
            { 0, 4, 4, 4, 4, 4, 0 },
            { 4, 4, 0, 0, 0, 4, 0 },
            { 4, 4, 0, 2, 0, 4, 0 },
            { 4, 4, 0, 0, 0, 4, 0 },
            { 0, 4, 4, 4, 4, 4, 0 },
            { 0, 0, 4, 4, 4, 0, 0 },
        };

        private int[,] wallSection = {
            { 3, 3, 3, 3, 0, 4, 4 },
            { 3, 3, 6, 6, 0, 4, 4 },
            { 3, 5, 5, 6, 6, 0, 0 },
            { 3, 0, 5, 5, 6, 6, 0 },
            { 0, 0, 0, 5, 5, 6, 0 },
            { 0, 0, 0, 0, 5, 6, 0 },
            { 0, 0, 0, 3, 3, 3, 3 },
        };
        private int[,] wallSection2 = {
            { 5, 0, 3, 3, 3, 6, 6 },
            { 5, 0, 0, 3, 6, 6, 0 },
            { 0, 0, 0, 3, 0, 0, 0 },
            { 0, 0, 4, 3, 4, 0, 0 },
            { 0, 0, 4, 4, 4, 0, 6 },
            { 5, 0, 4, 3, 4, 0, 6 },
            { 5, 5, 0, 3, 0, 0, 6 },
        };
        private int[,] wallSection3 = {
            { 0, 6, 6, 0, 3, 3, 3 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 5, 5, 3, 3, 4, 4, 0 },
            { 5, 0, 3, 3, 0, 4, 0 },
            { 5, 0, 6, 6, 0, 4, 0 },
            { 5, 0, 6, 6, 0, 4, 0 },
            { 3, 3, 3, 3, 0, 4, 0 },
        };
        private int[,] wallSection4 = {
            { 0, 0, 4, 4, 4, 4, 0 },
            { 3, 6, 3, 3, 3, 3, 4 },
            { 3, 6, 3, 3, 6, 3, 4 },
            { 3, 6, 3, 3, 6, 3, 4 },
            { 3, 5, 6, 6, 5, 3, 4 },
            { 3, 3, 3, 3, 3, 3, 4 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };
        private int[,] wallSection5 = {
            { 0, 0, 0, 6, 6, 6, 0 },
            { 4, 3, 3, 3, 3, 3, 0 },
            { 4, 3, 6, 6, 6, 3, 5 },
            { 0, 3, 6, 6, 6, 3, 5 },
            { 0, 3, 6, 6, 6, 3, 5 },
            { 0, 3, 3, 6, 3, 3, 5 },
            { 0, 0, 0, 0, 3, 0, 0 },
        };
        private int[,] wallSection6 = {
            { 3, 3, 3, 3, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 3 },
            { 0, 0, 0, 6, 6, 6, 3 },
            { 0, 3, 0, 6, 4, 4, 4 },
            { 0, 3, 0, 6, 4, 5, 5 },
            { 0, 0, 0, 6, 4, 5, 5 },
            { 0, 0, 0, 3, 3, 3, 3 },
        };
        private int[,] wallSection7 = {
            { 4, 6, 6, 4, 4, 0, 0 },
            { 4, 4, 6, 6, 4, 4, 0 },
            { 0, 4, 4, 6, 3, 4, 4 },
            { 0, 0, 4, 4, 6, 6, 4 },
            { 0, 3, 3, 4, 4, 6, 6 },
            { 3, 3, 3, 0, 4, 4, 6 },
            { 3, 3, 0, 0, 0, 4, 4 },
        };
    }
}
