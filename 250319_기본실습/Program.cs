namespace _250319_기본실습
{
	internal class Program
	{


		const int mapWidth = 81;
		const int mapHeight = 21;

		static int playerX = 1;
		static int playerY = 1;

		static int goalX = 79;
		static int goalY = 19;



		static int score = 0;
		static bool gameOver = false;


		static void Main(string[] args)
		{
			Console.SetWindowSize(mapWidth * 2 + 2, mapHeight + 5);
			Console.CursorVisible = false;


			char[,] map = new char[mapHeight, mapWidth];





			// 시작
			Start(map);

			while (gameOver == false)
			{
				//설정값 업데이트
				ConsoleKey key = Input();
				UpdateSetting(map, key);

				//렌더링
				Render(map);


			}

			// 종료화면출력
			PrintEndScreen();





		}


		static void Start(char[,] map)
		{
			PrintStartScreen();
			//키입력시 화면초기화
			Console.ReadKey();
			Console.Clear();

			GenerateMap(map);
			Render(map);
		}

		static void GenerateMap(char[,] map)
		{

			// 틀
			SetFrame(map);


			// 벽
			SetWall(map);


			// 플레이어
			SetPlayer(map);


			// 탈출구
			SetGoal(map);

		}


		static void SetFrame(char[,] map)
		{
			for (int i = 0; i < mapHeight; i++)
			{
				for (int j = 0; j < mapWidth; j++)
				{
					if (i == 0 || i == mapHeight - 1 || j == 0 || j == mapWidth - 1)
					{
						map[i, j] = '▒';
					}
					else
						map[i, j] = ' ';
				}
			}
		}


		static void SetWall(char[,] map)
		{
			for (int i = 1; i < mapHeight - 1; i++)
			{
				for (int j = 1; j < mapWidth - 1; j++)
				{
					if (j % 2 == 0)
						map[i, j] = '▒';
					else
						map[i, j] = ' ';

				}

			}

			for (int i = 2; i < mapWidth - 2; i += 2)
			{
				Random random = new Random();
				map[random.Next(1, mapHeight - 2), i] = ' ';
			}

		}


		static void SetPlayer(char[,] map)
		{
			map[playerX, playerY] = '▼';

		}


		static void SetGoal(char[,] map)
		{
			map[goalY, goalX] = '♥';
		}


		static void PrintStartScreen()
		{
			Console.Write("▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\n\n\n\n\n\n\n\n\n\n                                        시작\n\n\n\n\n\n\n\n\n\n▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒");
		}


		static ConsoleKey Input()
		{
			return Console.ReadKey(true).Key;
		}





		static void UpdateSetting(char[,] map, ConsoleKey key)
		{
			// 플레이어위치 세팅
			SetPlayerPos(map, key);

			// 하트 도착 확인
			CheckArrival(map);
		}


		static void SetPlayerPos(char[,] map, ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.A:
				case ConsoleKey.LeftArrow:
					if (map[playerX, playerY - 1] == '♥')
					{
						map[playerX, playerY--] = ' ';
						map[playerX, playerY] = '▼';
					}
					if (map[playerX, playerY - 1] != '▒')
					{
						map[playerX, playerY--] = ' ';
						map[playerX, playerY] = '▼';
					}
					break;

				case ConsoleKey.D:
				case ConsoleKey.RightArrow:
					if (map[playerX, playerY + 1] == '♥')
					{
						map[playerX, playerY++] = ' ';
						map[playerX, playerY] = '▼';
					}
					if (map[playerX, playerY + 1] != '▒')
					{
						map[playerX, playerY++] = ' ';
						map[playerX, playerY] = '▼';
					}
					break;


				case ConsoleKey.W:
				case ConsoleKey.UpArrow:
					if (map[playerX - 1, playerY] == '♥')
					{
						map[playerX--, playerY] = ' ';
						map[playerX, playerY] = '▼';
					}
					if (map[playerX - 1, playerY] != '▒')
					{
						map[playerX--, playerY] = ' ';
						map[playerX, playerY] = '▼';
					}
					break;

				case ConsoleKey.S:
				case ConsoleKey.DownArrow:
					if (map[playerX + 1, playerY] == '♥')
					{
						map[playerX++, playerY] = ' ';
						map[playerX, playerY] = '▼';
					}
					if (map[playerX + 1, playerY] != '▒')
					{
						map[playerX++, playerY] = ' ';
						map[playerX, playerY] = '▼';
					}
					break;

				case ConsoleKey.Escape:
					gameOver = true;
					break;
			}
		}


		static void CheckArrival(char[,] map)
		{
			if (map[goalY, goalX] != '♥')
			{
				// 하트위치 변경
				if (goalX == 79 && goalY == 19)
				{
					goalX = 1;
					goalY = 19;
				}
				else if (goalX == 1 && goalY == 19)
				{
					goalX = 79;
					goalY = 1;
				}
				else if (goalX == 79 && goalY == 1)
				{
					goalX = 1;
					goalY = 1;
				}
				GenerateMap(map);
				score += 100;
			}
		}


		static void PrintScore(int score)
		{
			Console.WriteLine($"\n현재점수: {score}");
		}

		static void PrintEndScreen()
		{
			Console.Clear();
			Console.WriteLine("끗");
		}

		static void PrintAll(char[,] map)
		{
			for (int i = 0; i < mapHeight; i++)
			{
				for (int j = 0; j < mapWidth; j++)
				{
					if (map[i, j] == '▒')
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write(map[i, j]);
						Console.ResetColor();
					}
					else if (map[i, j] == ' ')
						Console.Write(map[i, j]);
					else if (map[i, j] == '▼')
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write(map[i, j]);
						Console.ResetColor();
					}
					else if (map[i, j] == '♥')
					{
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.Write(map[i, j]);
						Console.ResetColor();
					}
				}
				Console.WriteLine();
			}
		}


		static void Render(char[,] map)
		{
			Console.SetCursorPosition(0, 0);
			PrintAll(map);
			PrintScore(score);
		}


	}
}
