// <copyright file="main.cs" company="MarcusMedina.pro">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>Marcus Medina</author>
// <date>2020-02-04 14:14</date>
// <summary>Class for generating CSV with dates</summary>

using System;

public class Program
{
	static string lastRow = "";
	public static void Main()
	{
		Console.WriteLine("Start date, Start time, End date, End time, subject, detail,All Day Event");
		int year = DateTime.Now.Year;
		for (int ycount = 0; ycount < 6; ycount++)
		{
			for (int month = 1; month < 13; month++)
			{
				System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
				string strMonthName = mfi.GetMonthName(month).ToString();
				//Console.WriteLine(strMonthName);
				int MaxDays = DateTime.DaysInMonth(year + ycount, month);
				for (int day = 1; day <= MaxDays; day++)
				{
					CheckPlaindromeDate(ycount + year, month, day, 0, 0, 0, "yyyy-MM-dd", "Palindrome whole day");
					CheckPlaindromeDate(ycount + year, month, day, 0, 0, 0, "yy-MM-dd", "Palindrome whole day without decennium");
					for (int hour = 0; hour < 24; hour++)
					{
						for (int minute = 0; minute < 60; minute++)
						{
							CheckPlaindromeDate(ycount + year, month, day, hour, minute, 0, "yyyy-MM-dd hh:mm", "Palindrome day with time");
							CheckPlaindromeDate(ycount + year, month, day, hour, minute, 0, "yy-MM-dd hh:mm", "Palindrome day with time without decennium");
						}

						CheckPlaindromeDate(ycount + year, month, day, hour, 0, 0, "yyyy-MM-dd hh:", "Palindrome day with whole hours");
						CheckPlaindromeDate(ycount + year, month, day, hour, 0, 0, "yy-MM-dd hh:", "Palindrome day with whole hours without decennium");
					}
				}
			}
		}
	}

	private static bool CheckPlaindromeDate(int year, int month, int day, int hour, int minute, int second, string pattern, string comment)
	{
		bool isCool = true;
		string searchPattern = pattern.Replace(':', ' ').Replace('-', ' ').Replace(" ", "");
		try
		{
			var curDate = new DateTime(year, month, day, hour, minute, second);
			var stringDate = curDate.ToString(searchPattern);
			var stringDateReversed = ReverseString(stringDate);
			if (stringDate == stringDateReversed && stringDate != lastRow)
			{
				lastRow = stringDate;
				GenerateCSV(curDate, comment, pattern);
			}
		}
		catch
		{
			isCool = false;
		}

		return isCool;
	}

	public static void GenerateCSV(DateTime time, string subject, string pattern)
	{
		bool allDay = !pattern.Contains(":");
		int addMinutes = pattern.Contains(":mm") ? 10 : 60;
		pattern = pattern.TrimEnd(':');
		var end = time.AddMinutes(addMinutes);
		Console.Write(time.ToString("yyyy-MM-dd") + ",");
		if (!allDay)
			Console.Write(time.ToString("hh:mm") + ",");
		Console.Write(end.ToString("yyyy-MM-dd") + ",");
		if (!allDay)
			Console.Write(end.ToString("hh:mm") + ",");
		Console.Write(subject + " " + time.ToString(pattern) + ",");
		Console.Write("Seen as " + time.ToString(pattern) + ",");
		Console.Write(allDay);
		Console.WriteLine();
	}

	// Code borrowed from https://stackoverflow.com/a/228060
  public static string ReverseString(string s)
	{
		char[] arr = s.ToCharArray();
		Array.Reverse(arr);
		return new string (arr);
	}
}
