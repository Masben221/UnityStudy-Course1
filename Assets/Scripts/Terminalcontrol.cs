using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminalcontrol : MonoBehaviour
{
   enum Screen{HelloMenu,MainMenu,Password,Win};
   Screen CurrentScreen = Screen.HelloMenu;
   const string MenuHint = "Напиши *меню*, чтобы вернуться в главное меню";
   int level;
   string PlayerName;
   string Password;
   string Podskazka;
   string[] Level1Passwords = {"книга", "ручка", "ключ","шкаф","очки"};
   string[] Level2Passwords = {"наручники","начальник","охрана","камера","дубинка"};
   string[] Level3Passwords = {"млечный путь","космонавт","космос","черная дыра","рэйнджер"};
   void Start()
     {
        ShowHelloMenu (PlayerName); 
     }

   void ShowHelloMenu (string PlayerName) // экран знакомства
     {
        Terminal.ClearScreen();
        Terminal.WriteLine("Введи свое имя:");
     }
   void ShowMainMenu (string PlayerName)// экран выбора сложности
     {
        CurrentScreen = Screen.MainMenu;
        level=0;
        Terminal.ClearScreen();
        Terminal.WriteLine("Привет, "+PlayerName+"!");
        Terminal.WriteLine("Какой терминал ты хочешь взломать сегодня?");
        Terminal.WriteLine(" ");
        Terminal.WriteLine("Введите 1 - городская библиотека");
        Terminal.WriteLine("Введите 2 - полицейский учаcток");
        Terminal.WriteLine("Введите 3 - космический корабль");
        Terminal.WriteLine("Ваш выбор:");
     }   
   void OnUserInput(string input) // Ввод имени, сложности и пароля
	{
      if (input=="меню")
         {
           ShowMainMenu ("рад видеть тебя снова. "+PlayerName);
         }
         else if (CurrentScreen == Screen.HelloMenu)
         {
           ShowHelloMenu(input);
           PlayerName=input;
           ShowMainMenu (PlayerName);
         }  
         else if (CurrentScreen == Screen.MainMenu)
         {
           RunMainMenu(input);
         }  
         else if (CurrentScreen == Screen.Password)
         {
           CheckPassword(input);
         }  
          else if (CurrentScreen == Screen.Win)
         {
           ShowMainMenu ("ты опять здесь?");
         }  
	}
   void RunMainMenu(string input)
   {
      bool isValidLevelNumber = (input=="1" || input=="2" || input=="3");
      if(isValidLevelNumber)
        {
           Terminal.ClearScreen();
           level=int.Parse(input);
           GameStart();
        }
      else switch(input)
      {
         case "007":
           Terminal.WriteLine("Hello. mr Bond!");
             break;
         case "царь":
           Terminal.WriteLine("Здарова, ваше величество!");
             break;
         case "генерал":
           Terminal.WriteLine("Здравия желаю, товарищ генерал!");
             break;
         default:
            Terminal.WriteLine("Введите правильное значение!");
            break;
      }
        
   }
   void GameStart() // экран ввода пароля
   {
      CurrentScreen=Screen.Password;
      Terminal.ClearScreen();
      switch(level)
      {
         case 1:
           Password=Level1Passwords[Random.Range (0,Level1Passwords.Length)];
           Terminal.WriteLine("Ты в городской библиотеке");
             break;
         case 2:
           Password=Level2Passwords[Random.Range (0,Level2Passwords.Length)];
           Terminal.WriteLine("Ты в полицейском участке");
             break;
         case 3:
           Password=Level3Passwords[Random.Range (0,Level3Passwords.Length)];
           Terminal.WriteLine("Ты на космическом корабле");
             break;
         default:
            Debug.LogError("Такого уровня не существует!");
            break;
      }
      Podskazka=Password.Anagram();
      Terminal.WriteLine("Подсказка: "+Podskazka);
      Terminal.WriteLine(MenuHint);
      Terminal.WriteLine("Введи пароль:");
   }
     void CheckPassword(string input) //сравнение пароля - экран победы
    {
      if (input==Password) 
        {
         ShowWinScreen ();
         CurrentScreen=Screen.Win;
        }
      else
        {
          GameStart();
        }
    }
   void ShowWinScreen()   //метод экрана победы
     {
     Terminal.ClearScreen();
     Reward(); 
     Terminal.WriteLine("Нажми 'enter' чтобы вернуться в главное меню");
     }  
   void Reward()
   {
     switch(level)
     {
         case 1:
           Terminal.WriteLine("Поздравляю, ты взломал библиотеку!");
           Terminal.WriteLine(
             @"
,         ,
|\\\\ ////|
| \\\V/// |
|  |~~~|  |
|  |===|  |
|  |j  |  |
|  | g |  |
 \ |  s| /
  \|===|/
   '---'
             " 
           );
             break;
         case 2:
           Terminal.WriteLine("Поздравляю, ты взломал полицейский участок!");
           Terminal.WriteLine(
             @"
      __,_____
     / __.==--)
    /#(-'
    `-'
             " 
           );
             break;
         case 3:
           Terminal.WriteLine("Поздравляю, ты взломал космический корабль!");
           Terminal.WriteLine(
             @"
           ___
     |     | |
    / \    | |
   |--o|===|-|
   |---|   |d|
  /     \  |w|
 | U     | |b|
 | S     |=| |
 | A     | | |
 |_______| |_|
  |@| |@|  | |
___________|_|_
             " 
           );

             break;
         default:
            Debug.LogError("Такого уровня не существует!");
             break;
      }
   }
}
