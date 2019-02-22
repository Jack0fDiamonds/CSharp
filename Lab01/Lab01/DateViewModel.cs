using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Lab01.Tools;
using Lab01.Tools.Managers;

namespace Lab01
{
    internal class DateViewModel : BaseViewModel
    {
        private bool initial = true;
        private DateTime _date = DateTime.Now;
        private string _age;
        private bool validAge = false;

        private readonly string[] ChineseZodiacSigns = new string[] { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged("Date");
                OnPropertyChanged("Age");
                OnPropertyChanged("ChineseZodiac");
                OnPropertyChanged("WesternZodiac");
            }
        }

        public string Age
        {
            get
            {
                CalculateAge();
                return _age;
            }
        }

        public string ChineseZodiac
        {
            get { return DetermineChineseZodiac(); }
        }

        private string DetermineChineseZodiac()
        {
            if (!validAge)
                return "";
            return ChineseZodiacSigns[_date.Year % 12];
        }

        public string WesternZodiac
        {
            get { return DetermineWesternZodiac(); }
        }

        private string DetermineWesternZodiac()
        {
            if (!validAge)
                return "";
            int day = _date.DayOfYear;
            if (day > 355 || day < 20)
                return "Capricorn";
            else if (day < 50)
                return "Aquarius";
            else if (day < 80)
                return "Pisces";
            else if (day < 110)
                return "Aries";
            else if (day < 141)
                return "Taurus";
            else if (day < 172)
                return "Gemini";
            else if (day < 204)
                return "Cancer";
            else if (day < 235)
                return "Leo";
            else if (day < 266)
                return "Virgo";
            else if (day < 296)
                return "Libra";
            else if (day < 326)
                return "Scorpio";
            return "Sagittarius";
        }

        private void CalcAge()
        {
            int age = DateTime.Now.Year - _date.Year;
            if (DateTime.Now.DayOfYear < _date.DayOfYear)
                age--;
            if (age < 0 || age > 135)
            {
                validAge = false;
                _age = "";
            }
            else
            {
                validAge = true;
                _age = age.ToString();
            }
        }

        private async void CalculateAge()
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                CalcAge();
                if (!initial)
                {
                    if (!validAge)
                    {
                        MessageBox.Show("Error!\nInvalid date or birth");
                    }
                    else if (_date.DayOfYear == DateTime.Today.DayOfYear)
                    {
                        MessageBox.Show("Happy Birthday");
                    }
                }
                initial = false;
                Thread.Sleep(500);
            });
            LoaderManager.Instance.HideLoader();
        }
    }
}
