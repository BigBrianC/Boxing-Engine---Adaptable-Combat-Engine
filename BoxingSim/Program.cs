using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingSim
{
    class Program
    {
        //Power, Speed, Technique, Quickness, Stamina, Heart, Chin, Head, Body, Punch Tendency, Distance Tendency
        public static int[] player1 = new int[11]{99, 99, 99, 50, 99, 99, 99, 100, 100,50,50};
        public static int[] player2 = new int[11]{99, 99, 99, 50, 99, 99, 99, 100, 100,50,50};
        public static string player1Name = "Player One";
        public static string player2Name = "Player Two";
        public static string part;
        public static string[,] ring = new string[5, 5];
        public static int[,] player1Position = new int[5, 5];
        public static int[,] player2Position = new int[5, 5];
        public static int distance;
        public static Random rnd = new Random();

        public static void RingPrint()
        {
            for (int i = 0; i < ring.GetLength(0); i++)
            {
                for (int j = 0; j < ring.GetLength(1); j++)
                {
                    if (j % 5 == 0)
                    {
                        Console.WriteLine();
                    }
                    Console.Write(ring[i, j] + " ");
                }
            }
            Console.WriteLine();
        }

        public static void RingAssign()
        {
            for (int i = 0; i < ring.GetLength(0); i++)
            {
                for (int j = 0; j < ring.GetLength(1); j++)
                {
                    ring[i, j] = "Empty";
                }
            }
            ring[0, 2] = "Player One";
            ring[4, 2] = "Player Two";

        }

        public static void NormalRange(int chin, int power, int technique, ref int health, string defenseName, string attackName, string part)
        {
            Strike(chin, power, technique, ref health, defenseName, attackName, part);
        }

        public static void FarRange(int distanceTend, string movingPlayer, string standingPlayer) //If too far to strike
        {
            Console.WriteLine("Far");
            Movement(distanceTend, movingPlayer, standingPlayer);
        }

        public static void Movement(int distanceTend, string movingPlayer, string standingPlayer)
        {
            int distanceRand = rnd.Next(1, 100);
                for (int i = 0; i < ring.GetLength(0); i++)
                {
                    for (int j = 0; j < ring.GetLength(1); j++)
                    {
                        if (ring[i, j] == movingPlayer)
                        {
                             for (int a = 0; a < ring.GetLength(0); a++)
                              {
                                 for (int w = 0; w < ring.GetLength(1); w++)
                                     {
                        if (ring[a, w] == standingPlayer)
                        {
                            if (distanceRand > distanceTend) //If players are close enough to each other, fight, else, move around whether away or towards
                                {
                            if(a > (i+1)){
                                ring[i,j] = "empty";
                                if(i > 1){
                                    ring[i-1,j] = movingPlayer;
                                }else if(j > 1){
                                    ring[i,j-1] = movingPlayer;
                                }else{
                                    Console.WriteLine(movingPlayer + " backed into a corner!");
                                }
                            }else{
                                ring[i,j] = "empty";
                                if(i < 5){
                                    ring[i+1,j] = movingPlayer;
                                }else if(j < 5){
                                    ring[i,j+1] = movingPlayer;
                                }else{
                                    Console.WriteLine(movingPlayer + " backed into a corner!");
                                }
                            }
                        }else{
                         if((a+1) < i){
                                ring[i,j] = "empty";
                                if(i > 1){
                                    ring[i-1,j] = movingPlayer;
                                }else if(j > 1){
                                    ring[i,j-1] = movingPlayer;
                                }else{
                                    Console.WriteLine(movingPlayer + " backed into a corner!");
                                }
                            }else{
                                ring[i,j] = "empty";
                                if(i < 5){
                                    ring[i+1,j] = movingPlayer;
                                }else if(j < 5){
                                    ring[i,j+1] = movingPlayer;
                                }else{
                                    Console.WriteLine(movingPlayer + " backed into a corner!");
                                }
                                            }
                                        }
                                     }
                                }
                            }
                        }
                    }
                }
           
            DistanceFinder();
            RingPrint();
                
        }


        public static void Strike(int chin, int power, int technique, ref int health, string defenseName, string attackName, string part)
        {
            
            double damageRand = rnd.NextDouble();
            double defenseRand = rnd.NextDouble();
            int damage = Convert.ToInt32((chin - (chin * defenseRand)) - (power - (power * damageRand)));
          
            int haymakerRand = rnd.Next(1, 100);
            if (haymakerRand > ((power * damageRand) + (technique * damageRand) / 2))
            {
                if (damage == 0)
                {
                    damage = 5;
                    Console.WriteLine(attackName + " throws a haymaker!");
                    Console.WriteLine(defenseName + " tries to block and limits damage!");
                }
                else
                {
                    damage *= 2;
                    Console.WriteLine(attackName + " lands a haymaker!");
                }
            }
            if (damage < 0)
            {
                damage = 0;
                Console.WriteLine(attackName + " goes for the strike");
                Console.WriteLine(damage + " damage done on " + defenseName + "'s " + part);
                Console.WriteLine(health + " health remaining for " + defenseName + "'s " + part);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(attackName + " goes for the strike");
                Console.WriteLine(damage + " damage done on " + defenseName + "'s " + part);
                health -= Convert.ToInt16(damage);
                Console.WriteLine(health + " health remaining for " + defenseName + "'s " + part);
                Console.WriteLine();
            }
        }

        public static void DistanceFinder() //Wrong formula
        {
            int x1 = 0;
            int x2 = 0;
            int y1 = 0;
            int y2 = 0;

            for (int i = 0; i < ring.GetLength(0); i++)
            {
                for (int j = 0; j < ring.GetLength(1); j++)
                {
                    if (ring[i, j] == "Player One")
                    {
                        x1 = i;
                        y1 = j;
                    }
                   if (ring[i, j] == "Player Two")
                    {
                        x2 = i;
                        y2 = j;
                    }
                }
            }
            if (y2 - y1 == 0)
            {
                distance = x2-x1;
            }
            else
            {
            }
        }

        public static void Fight(int tendencyRand, double speedRand, double speedRand2){
                double p1speed = (player1[3] - (player1[3] * speedRand));
                double p2speed = (player2[3] - (player2[3] * speedRand2));

                if (distance > 2)
                {
                    if (p1speed > p2speed)
                    {
                        FarRange(player1[10], "Player One", "Player Two");
                    }
                    else
                    {
                        FarRange(player2[10], "Player Two", "Player One");
                    }
                }
                else if (distance <= 2)
                {
                    if (p1speed > p2speed)
                    {
                        if (tendencyRand > player1[9]) //If punch head
                        {
                            if (tendencyRand > player1[10]) //If move
                            {
                                Movement(player1[10], "Player One", "Player Two");
                            }
                            else //If punch
                            {
                                NormalRange(player2[6], player1[0], player1[2], ref player2[7], player2Name, player1Name, "Head");
                            }
                        }
                        else //If punch body
                        {
                            NormalRange(player2[6], player1[0], player1[2], ref player2[8], player2Name, player1Name, "Body");
                        }
                    } 
                    else //Player two turn
                    {
                        if (tendencyRand > player2[9]) //Repeat of same above sequence
                        {
                            if (tendencyRand > player2[10])
                            {
                                Movement(player2[10], "Player Two", "Player One");
                            }
                            else { 
                            NormalRange(player1[6], player2[0], player2[2], ref player1[7], player1Name, player2Name, "Head");
                            }
                        }
                        else
                        {
                            NormalRange(player1[6], player2[0], player2[2], ref player1[8], player1Name, player2Name, "Body");
                        }
                        
                    }
                }

            }
        
         
        static void Main(string[] args)
        {
            RingAssign();
            RingPrint();
            DistanceFinder();
            do
            {
                int tendencyRand = rnd.Next(0, 100);
                double speedRand = rnd.NextDouble();
                double speedRand2 = rnd.NextDouble();
                Fight(tendencyRand, speedRand, speedRand2);
            }while (player1[7] > 0 && player2[7] > 0 && player1[8] > 0 && player2[8] > 0);
            Console.ReadKey();
        }
    }
}
