﻿using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadAMrSidImage : UserControl
    {
        public LoadAMrSidImage()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            MrSidRasterLayer sidImageLayer = new MrSidRasterLayer("SampleData/world.sid");
            LayerOverlay imageOverlay = new LayerOverlay();
            imageOverlay.Layers.Add("SidImageLayer", sidImageLayer);
            mapView.Overlays.Add(imageOverlay);
           
            mapView.CurrentExtent = new RectangleShape(-118.098, 84.3, 118.098, -84.3);
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            //
            // UserControl
            //
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}