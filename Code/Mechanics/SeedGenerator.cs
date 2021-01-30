using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UglyDuckling.Code.ChickenControl;
using UglyDuckling.Code.Entities;

namespace UglyDuckling.Code.Mechanics
{
    class SeedGenerator
    {
        private readonly Random random = new Random();
        /// <summary>
        /// Reference to the EntityManager chickens will be added to
        /// </summary>
        private readonly EntityManager EntityManager;

        public SeedGenerator(EntityManager entityManager)
        {
            this.EntityManager = entityManager;
        }

        public void GenerateSeeds()
        {
            for (int i = 0; i < 2; i++)
            {
                Seed s = new Seed(CheckpointGenerator.RandomOffset(NamedPositions.ChickenCoopOutside, 300));
                EntityManager.AddEntity(s);
            }

            for (int i = 0; i < 5; i++)
            {
                Seed s = new Seed(CheckpointGenerator.RandomOffset(NamedPositions.Sandbox, 600));
                EntityManager.AddEntity(s);
            }

            for (int i = 0; i < 2; i++)
            {
                Seed s = new Seed(CheckpointGenerator.RandomOffset(NamedPositions.Lake, 100));
                EntityManager.AddEntity(s);
            }

            for (int i = 0; i < 7; i++)
            {
                Seed s = new Seed(CheckpointGenerator.RandomOffset(NamedPositions.Grass, 600));
                EntityManager.AddEntity(s);
            }

        }

    }
}
