# DatePalindrome
A fun script I did while bored at work using net Fiddle. It generates a CSV that can be imported to Google Calendar

It will find dates like 2020-02-02, 20-11-02, 2021-12-21 12:02 etc

example output

    Start date, Start time, End date, End time, subject, detail,All Day Event
    2020-02-02,2020-02-02,Palindrome whole day 2020-02-02,Seen as 2020-02-02,True
    2020-02-20,02:02,2020-02-20,02:12,Palindrome day with time 2020-02-20 02:02,Seen as 2020-02-20 02:02,False
    2020-02-20,02:00,2020-02-20,03:00,Palindrome day with whole hours without decade 20-02-20 02,Seen as 20-02-20 02,False
    2020-11-02,2020-11-02,Palindrome whole day without decade 20-11-02,Seen as 20-11-02,True
