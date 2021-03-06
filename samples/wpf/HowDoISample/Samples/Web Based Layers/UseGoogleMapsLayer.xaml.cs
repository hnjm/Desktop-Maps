﻿using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for UseGoogleMapsLayer.xaml
    /// </summary>
    public partial class UseGoogleMapsLayer : UserControl
    {
        public UseGoogleMapsLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // There are 3 kinds of authentication for Google Maps as below:
            //  1. ApiKey + UriSigningSecret
            //  2. ApiKey
            //  3.ClientId + PrivateKey
            var apiKey = "AIzaSyApIWa5nx0KknyLQp4pNaTpz13a4w2kLZE";
            var uriSigningSecret = "6CVKqVDeZNfZjARYA5CaKYPT1Eg=";
            GoogleMapsLayer worldLayer = new GoogleMapsLayer();
            worldLayer.ClientId = "gme-valmontindustries";
            worldLayer.PrivateKey = "-J7YzyRJpepGq9HwRM3LGXyDM04=";

            LayerOverlay worldOverlay = new LayerOverlay();
            //worldOverlay.TileType = TileType.SingleTile;
            worldOverlay.Layers.Add("WorldLayer", worldLayer);

            ShapeFileFeatureLayer shapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates_3857.shp"));
            AreaStyle areaStyle = new AreaStyle();
            areaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(125, 233, 232, 214));
            areaStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, 118, 138, 69), 1);
            shapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = areaStyle;
            shapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldOverlay.Layers.Add(shapeLayer);

            mapView.ZoomLevelSet = new GoogleMapsZoomLevelSet();
            mapView.Overlays.Add("WorldOverlay", worldOverlay);
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-10000000, 10000000, 10000000, -10000000);
            mapView.Refresh();
        }
    }
}
