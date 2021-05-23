﻿using Matches;
using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kopakabana
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Tournament Tour = new Tournament();
        public MainWindow()
        {
            InitializeComponent();
            Referee.Visibility = Visibility.Hidden;
            Team.Visibility = Visibility.Hidden;
            Match.Visibility = Visibility.Hidden;
            ///Czytanie danych, genreowanie meczy wczytywanie danych wynikow do wygenrewanych meczow i na tej podstawie zlicznaie wygranych
            Tour.Read();
            Tour.GenerateMatches();
            //Tour.ReadScore();
            Tour.CountWins();
            DataContext = this;
        }
        public void Refresh()
        {
            /*
             * Metoda ktora odswieza liste
             * Generuje ponownie mecze gdy zostana dodane lub usuniete druzyny 
             * Na podstawie ponownie wygenerwoanych meczow przypsiuje wyniki
             * zlicza wygrane
             */
            Lista.Items.Refresh();
            Tour.GenerateMatches();
            Tour.Save();
            //Tour.ReadScore();
            Tour.CountWins();
        }
        private void Mecze_Click(object sender, RoutedEventArgs e)
        {
            Lista.Items.Clear();
            SetScore.Visibility = Visibility.Visible;
            string poptype = " ";
            foreach (Match M in Tour.getMatches())
            {
                if (M.GetType().Name != poptype)
                {
                    poptype = M.GetType().Name;
                    Lista.Items.Add(new ListBoxItem() { Content = poptype.ToUpper(), Foreground = Brushes.Black });
                }
                poptype = M.GetType().Name;
                ListBoxItem Item = new ListBoxItem() { Content = M };
                // Lista.SelectionChanged += new SelectionChangedEventHandler(SelectMatch);
                Lista.Items.Add(Item);
            }
            Referee.Visibility = Visibility.Hidden;
            Team.Visibility = Visibility.Hidden;
        }
        private void Sedzia(object sender, RoutedEventArgs e)
        {
            Lista.Items.Clear();
            SetScore.Visibility = Visibility.Hidden;
            foreach (Referee Ref in Tour.getReferees())
            {
                ListBoxItem Item = new ListBoxItem() { Content = Ref };
                Lista.Items.Add(Item);
            }

            Referee.Visibility = Visibility.Visible;
            Team.Visibility = Visibility.Hidden;
        }

        private void Druzyny_Click(object sender, RoutedEventArgs e)
        {
            Lista.Items.Clear();
            SetScore.Visibility = Visibility.Hidden;
            foreach (Team T in Tour.getTeams())
            {
                ListBoxItem Item = new ListBoxItem() { Content = T };
                Lista.Items.Add(Item);
            }
            Referee.Visibility = Visibility.Hidden;
            Team.Visibility = Visibility.Visible;
        }

        private void RefAdd(object sender, RoutedEventArgs e)
        {

        }

        private void RefDelete(object sender, RoutedEventArgs e)
        {

        }

        private void RefEdit(object sender, RoutedEventArgs e)
        {

        }

        private void TAdd(object sender, RoutedEventArgs e)
        {
            Manage Man = new Manage();
            //Man.Show();
            Man.AddTeam.Visibility = Visibility.Visible;
            if (true == Man.ShowDialog())
            {
                Tour.Teams.Add(new Team(Man.NameT.Text, new Player(Man.NameP1.Text, Man.SurnameP1.Text), new Player(Man.NameP2.Text, Man.SurnameP2.Text), new Player(Man.NameP3.Text, Man.SurnameP3.Text), new Player(Man.NameP4.Text, Man.SurnameP4.Text)));
                Druzyny_Click(sender, e);
                Refresh();
            }
        }
        private void TDelete(object sender, RoutedEventArgs e)
        {
            var I = Tour.getTeams();
            I.RemoveAt(Lista.SelectedIndex);
            Tour.setTeams(I);
            Druzyny_Click(sender, e);
            Refresh();
        }

        private void TEdit(object sender, RoutedEventArgs e)
        {
            Manage Man = new Manage();
            string pop;
            //Man.Show();
            Man.AddTeam.Visibility = Visibility.Visible;
            //if(Lista.SelectedItem is Team)
            pop=Man.NameT.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).Name;
            Man.NameP1.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).P1.Name;
            Man.SurnameP1.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).P1.Surname;
            Man.NameP2.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).P2.Name;
            Man.SurnameP2.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).P2.Surname;
            Man.NameP3.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).P3.Name;
            Man.SurnameP3.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).P3.Surname;
            Man.NameP4.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).P4.Name;
            Man.SurnameP4.Text = ((Team)((ListBoxItem)Lista.SelectedItem).Content).P4.Surname;
            int ID = ((Team)((ListBoxItem)Lista.SelectedItem).Content).ID;
            if (true == Man.ShowDialog())
            {
                Tour.setTeams(Lista.SelectedIndex, Man.NameT.Text, new Player(Man.NameP1.Text, Man.SurnameP1.Text), new Player(Man.NameP2.Text, Man.SurnameP2.Text), new Player(Man.NameP3.Text, Man.SurnameP3.Text), new Player(Man.NameP4.Text, Man.SurnameP4.Text));
                //Tour.writenameid(NameT.Text,ID,Lista.SelectedIndex);
                Tour.ChangeName(pop, Man.NameT.Text);
                Tour.SearchName(Man.NameT.Text);
                Druzyny_Click(sender, e);
                Refresh();
                //Refresh();

            }

        }
        private void LeftDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Lista.SelectedItem == null)
                    throw new Exception();
                var I = (Match)((ListBoxItem)(Lista.SelectedItem)).Content;
                Main.Visibility = Visibility.Hidden;
                Match.Visibility = Visibility.Visible;
                Name1.Text = I.T1.getName();
                Name2.Text = I.T2.getName();
                if (I is VolleyBall)
                {
                    ASS.Visibility = Visibility.Visible;
                    Res1.Text = ((VolleyBall)I).Result1.ToString();
                    Res2.Text = ((VolleyBall)I).Result2.ToString();
                    Type.Text = I.GetType().Name;
                    Ref.Text = ((VolleyBall)I).REF.ToString();
                    Ass1.Text= ((VolleyBall)I).AS1.ToString();
                    Ass2.Text= ((VolleyBall)I).AS2.ToString();
                }
                else
                {
                    ASS.Visibility = Visibility.Hidden;
                    Res1.Text = I.Result1.ToString();
                    Res2.Text = I.Result2.ToString();
                    Type.Text = I.GetType().Name;
                    Ref.Text = I.REF.ToString();
                    
                }
            }
            catch (Exception) { }
            /*ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            Match I = (Match)(lbi.Content);*/
            // MessageBox.Show(I.T1.ToString(), "Error", MessageBoxButton.OK);
        }

        private void BackMain(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            Match.Visibility = Visibility.Hidden;
            Lista.SelectedItem = false;
            ((ListBoxItem)(Lista.SelectedItem)).IsSelected = false;
        }

        private void SetScore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Score SC = new Score();
                Match Ob = ((Match)((ListBoxItem)Lista.SelectedItem).Content);
                SC.Volley.Visibility = Visibility.Visible;
                SC.Team1.Text = Ob.T1.Name;
                SC.Team2.Text = Ob.T2.Name;
                SC.Score1.Text = Ob.Result1.ToString();
                SC.Score2.Text = Ob.Result2.ToString();
                /*if (Ob is VolleyBall)
                {
                    SC.Score1.Text = ((VolleyBall)Ob).Result1.ToString();
                    SC.Score2.Text = ((VolleyBall)Ob).Result2.ToString();
                }*/
                if (true == SC.ShowDialog())
                {
                    int r = 0;
                    if (Ob is VolleyBall)
                        r = 1;
                    if (Ob is TugOfWar)
                        r = 2;
                    if (Ob is DodgeBall)
                        r = 3;
                    Tour.setMatch(Lista.SelectedIndex - r, int.Parse(SC.Score1.Text), int.Parse(SC.Score2.Text));
                    //Ob = getMatch(Lista.SelectedIndex - 1);
                    Ob.SetWhoWon();
                    Mecze_Click(sender, e);
                    //Refresh();
                    Tour.CountWins();
                    Tour.SaveScore(Ob,true);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Zła wartość", "Bład", MessageBoxButton.OK);
            }

        }
    }
}