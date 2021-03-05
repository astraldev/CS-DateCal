using System;
using System.Collections.Generic;
using static System.Console;
using System.Text;
using System.IO;
using System.Linq;

namespace Application
{
  class App
  {
    public static string[] Months = 
    {"January","Febuary","March","April","May","June","July","August","September","October","November","December"};
    public static int[] Days = 
    {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    public static int[] LeapDays = 
    {31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};

    public static string ParseDate(int y, int m, int d){
      int numOfDays = d;
      int[] days;
      if(IsLeapYear(y))
      days = LeapDays;
      else
      days = Days;

      for(int i = 0; i < m-1; i++){
        numOfDays+=days[i];
      }
      return $"{ParseDay(d)} {Months[m]} is the {ParseDay(numOfDays)} day of the year {y}";
    }

    public static void SaveToFile(StreamWriter f,string date){
      f.WriteLine(date);
    }
    public static string ParseDay(int dd){
      List<char> end = new();
      
      foreach(char ch in dd.ToString()){
        end.Add(ch);
      }
      char lastnumber = end.Last<char>();

      StringBuilder OutDay = new();
      switch(lastnumber){

        case '1' :  
          OutDay.Append(dd);
          OutDay.Append("st");
          break;

        case '2' :  
          OutDay.Append(dd);
          OutDay.Append("nd");
          break;

        case '3' :  
          OutDay.Append(dd);
          OutDay.Append("rd");
          break;

        default:
          OutDay.Append(dd);
          OutDay.Append("th");
          break;
      }

      return OutDay.ToString();
    }

    public static bool IsLeapYear(int year){
      if(year%4==0)
        return true;
      else
        return false;  
    }

    public static List<int> GenerateDate(){
      Random day = new();
      Random month = new();
      Random year = new();
      List<int> outList = new();

      int yy = year.Next(1960, 2021);
      int mm = month.Next(1, 12);
      int rangeOfMonth;

      if(App.IsLeapYear(yy))
        rangeOfMonth = Days[mm];
      else
        rangeOfMonth = LeapDays[mm];
      

      int dd = day.Next(1, rangeOfMonth);

      outList.Add(yy);
      outList.Add(mm);
      outList.Add(dd);

      return outList;
    }
    public static void Main(string[] args)
    {
      ForegroundColor = ConsoleColor.Blue;
      WriteLine("Application Started....");
      ResetColor();
      Write("");

      StreamWriter st = new("./date.txt");

      int i = 0;
      while (i < 20){
        List<int> date = GenerateDate();
        SaveToFile(st, ParseDate(date[0], date[1], date[2]));
        ++i;
      }

      st.Close();

      ForegroundColor = ConsoleColor.Green;
      WriteLine("Created 20 random dates, saved them to a file \"date.txt\"");
      ResetColor();
      Write("");
      
      ForegroundColor = ConsoleColor.Cyan;
      WriteLine("Press any key to exit...");
      ResetColor();
      Write("");
      ReadKey();
    }
  }
}