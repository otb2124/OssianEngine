using Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System.Collections.Generic;
using static Resources.StaticSpriteFactory;
using Utils;
using System;


namespace Entities
{
    public class TileEntity : PhysicalEntity
    {

        public enum TileSets
        {
            TILE_0,
            TILE_1,
            WATER_0,
        }

        public AnimationSet[] aManagers;
        public int[][] Indicies;
        public bool IsGround;
        public bool DisableEntityBodyGroundingStatusOnWalls;
        public TileSets TileSet;

        public TileEntity(Vector2 pos, Point layout, TileSets tileSet, float rot = 0f, bool isGround = false, bool disableEntityGrounding = false) : base()
        {
            TileSet = tileSet;
            Indicies = GenerateIndicies(layout.X, layout.Y, isGround);
            Model = ModelFactory.CreateModel(StaticSprites.NONE, PhysicalBodyFactory.CreatePhysicalBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(32 * layout.X, 32 * layout.Y), 1f, 0.5f));
            Init(pos, rot);

            IsGround = isGround;
            DisableEntityBodyGroundingStatusOnWalls = disableEntityGrounding;
        }

        public TileEntity(Vector2 pos, int[][] indiciesMap, TileSets tileSet, float rot = 0f) : base()
        {
            TileSet = tileSet;
            Indicies = indiciesMap;
            Model = ModelFactory.CreateModel(StaticSprites.NONE, PhysicalBodyFactory.CreatePhysicalBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(32 * indiciesMap[0].Length, 32 * indiciesMap.Length), 1f, 0.5f));
            Init(pos, rot);
        }

        public void Init(Vector2 pos, float rot)
        {
            Model.Body.MoveTo(PhysicalConverter.ToFlatVector(pos));
            Model.Body.RotateTo(rot);
            //Physics.Physics.flatWorld.AddBody(Model.Body);
            Model.Body.Owner = this;

            StaticSprite[] data = TileSetCut(TileSet);
            aManagers = new AnimationSet[data.Length];

            for (int i = 0; i < aManagers.Length; i++)
            {
                aManagers[i] = new AnimationSet(data[i]);
            }
        }

        public override void DrawCollider()
        {
            Color drawColor = new Color((byte)Color.Green.R, (byte)Color.Green.G, (byte)Color.Green.B, (byte)64);
            Graphics.Graphics.shapes.DrawBoxFill(PhysicalConverter.ToVector2(Model.Body.Position), Model.Body.Width, Model.Body.Height, Model.Body.Angle, drawColor);
        }

        private static int[][] GenerateIndicies(int width, int height, bool isGrounding)
        {
            if (!isGrounding && LayoutToTilesetIndicies.TryGetValue(new Vector2(width, height), out int[][] predefined))
            {
                return predefined;
            }

            int[][] indicies = new int[height][];
            for (int y = 0; y < height; y++)
            {
                indicies[y] = new int[width];
                for (int x = 0; x < width; x++)
                {
                    if (height == 1)
                    {
                        if (x == 0)
                            indicies[y][x] = 2; // Outer corner left bottom
                        else if (x == width - 1)
                            indicies[y][x] = 3; // Outer corner right bottom
                        else
                            indicies[y][x] = 5; // Bottom border
                    }
                    else
                    {
                        if (y == 0 && !isGrounding)
                        {
                                if (x == 0)
                                    indicies[y][x] = 0; // Outer corner left top
                                else if (x == width - 1)
                                    indicies[y][x] = 1; // Outer corner right top
                                else
                                    indicies[y][x] = 4; // Top border

                            
                        }
                        else if (y == height - 1)
                        {
                            if (x == 0)
                                indicies[y][x] = 2; // Outer corner left bottom
                            else if (x == width - 1)
                                indicies[y][x] = 3; // Outer corner right bottom
                            else
                                indicies[y][x] = 5; // Bottom border
                        }
                        else
                        {
                            if (x == 0)
                                indicies[y][x] = 11; // Left border
                            else if (x == width - 1)
                                indicies[y][x] = 12; // Right border
                            else
                                indicies[y][x] = (x + y) % 2 == 0 ? 13 : 14; // Inner or Inner alt
                        }
                    }
                }
            }
            return indicies;
        }

        public override void Draw()
        {
            // Tile indices:
            // 0: outer corner left top
            // 1: outer corner right top
            // 2: outer corner left bottom
            // 3: outer corner right bottom
            // 4: top border
            // 5: bottom border
            // 6: inner corner right top
            // 7: inner corner left top
            // 8: inner corner right bottom
            // 9: inner corner left bottom
            // 10: pillar top
            // 11: left border
            // 12: right border
            // 13: inner
            // 14: inner alt
            // 15: pillar bottom

            Matrix rotationMatrix = Matrix.CreateRotationZ(Model.Body.Angle);

            // Draw normal tiles
            for (int x = 0; x < Indicies.Length; x++)
            {
                for (int y = 0; y < Indicies[0].Length; y++)
                {
                    Vector2 localPos = new Vector2(y * 32, x * 32);
                    Vector2 rotatedPos = Vector2.Transform(localPos, rotationMatrix);
                    Vector2 worldPos = PhysicalConverter.ToVector2(Model.Body.Position) + rotatedPos;

                    aManagers[Indicies[x][y]].DrawCurrent(
                        worldPos,
                        Color.White,
                        Model.Body.Angle,
                        new Vector2(Model.Body.Width / 2f, Model.Body.Height / 2f),
                        Vector2.One,
                        0f,
                        true
                    );
                }
            }

            // Draw grounding rows if IsGround is true
            if (IsGround)
            {
                for (int x = Indicies.Length; x < Indicies.Length + 25; x++)
                {
                    for (int y = 0; y < Indicies[0].Length; y++)
                    { 
                        int tileIndex = (x + y) % 8 == 0 ? 14 : 13;
                        Vector2 localPos = new Vector2(y * 32, x * 32);
                        Vector2 rotatedPos = Vector2.Transform(localPos, rotationMatrix);
                        Vector2 worldPos = new Vector2(PhysicalConverter.ToVector2(Model.Body.Position).X + rotatedPos.X, PhysicalConverter.ToVector2(Model.Body.Position).Y - rotatedPos.Y + (Indicies.Length-1) * 32);

                        aManagers[tileIndex].DrawCurrent(
                            worldPos,
                            Color.White,
                            Model.Body.Angle,
                            new Vector2(Model.Body.Width / 2f, Model.Body.Height / 2f),
                            Vector2.One,
                            0f,
                            true
                        );
                    }
                }
            }
        }



        public static Dictionary<Vector2, int[][]> LayoutToTilesetIndicies = new()
        {
            {
                //vert flip
                new Vector2(2, 1),
                new int[][]
                {
                    new int[]{ 2, 3 },
                }
            },
            { 
                //vert flip
                new Vector2(3, 1),
                new int[][]
                {
                    new int[]{ 2, 5, 3 },
                }
            },
            {
                new Vector2(3, 2),
                new int[][]
                {
                    new int[]{ 0, 4, 1 },
                    new int[]{ 2, 5, 3 },
                }
            },
            {
                new Vector2(3, 3),
                new int[][]
                {
                    new int[]{ 0, 4, 1 },
                    new int[]{ 11, 13, 12 },
                    new int[]{ 2, 5, 3 },
                }
            },
        };
    }





}
