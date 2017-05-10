﻿using System;
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
using System.Windows.Shapes;

namespace Karteikarten_APP
{
    /// <summary>
    /// Interaktionslogik für NeueKarte.xaml
    /// </summary>
    public partial class NeueKarte : Window
    {
        public static sqlkram sqq = new sqlkram();
        public NeueKarte()
        {
            InitializeComponent();


            List<int> StapelIds = new List<int>(sqq.HoleAlleStapelIDs());
            ComboBoxItem cbi = new ComboBoxItem();

            sqlkram.VerbindungAufbauen();
            for (int i = 0; i < sqq.ZaehleStapel(); i++)
            {
                if (!sqq.HatUnter(StapelIds[i]))
                {
                    KategorieCombo.Items.Add(sqq.zeigeKategorie(StapelIds[i]));

                    sqlkram.VerbindungBeenden();
                }
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            int StapelID=sqq.HoleStapelID((string)KategorieCombo.SelectedItem);

            try
            {
                sqq.neueKarte(FrageBox.Text, AntwortBox.Text, StapelID, (string)KategorieCombo.SelectedItem);
                msg.Text = "Karte wurde erfolgreich hinzugefügt";
            }
            catch
            {
                msg.Text = "Sorry es ist ein Fehler aufgetreten";
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}