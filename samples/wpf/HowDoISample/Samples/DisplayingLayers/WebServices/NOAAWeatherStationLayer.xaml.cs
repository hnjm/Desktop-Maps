﻿using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;
using System;
using System.Diagnostics;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class NOAAWeatherStationLayer : UserControl
    {
        public NOAAWeatherStationLayer()
        {
            InitializeComponent();
        }

        public delegate void RefreshWeatherStations();

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            LayerOverlay weatherOverlay = new LayerOverlay();
            mapView.Overlays.Add("Weather", weatherOverlay);

            NoaaWeatherStationMonitor.StationsUpdated += NoaaWeatherStationMonitor_StationsUpdated;

            NoaaWeatherStationFeatureLayer nOAAWeatherStationLayer = new NoaaWeatherStationFeatureLayer();
            nOAAWeatherStationLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);

            nOAAWeatherStationLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new NoaaWeatherStationStyle());
            nOAAWeatherStationLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            weatherOverlay.Layers.Add(nOAAWeatherStationLayer);

            mapView.Refresh();
        }

        private void NoaaWeatherStationMonitor_StationsUpdated(object sender, StationsUpdatedNoaaWeatherStationMonitorEventArgs e)
        {
            mapView.Dispatcher.Invoke(new RefreshWeatherStations(this.UpdateWeatherStations), new object[] { });
        }

        private void mapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            Debug.WriteLine(e.NewExtent.ToString());
        }

        private void UpdateWeatherStations()
        {
            mapView.Refresh(mapView.Overlays["Weather"]);
        }
    }
}
