import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { faMapMarker } from '@fortawesome/free-solid-svg-icons';
import 'leaflet';
import { MapMarkersDTO } from 'src/app/models/map-markers-dto';
declare let L;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent implements OnInit, AfterViewInit {

  markerIcon = faMapMarker;
  @Input() inputPickupMarker: MapMarkersDTO;
  @Input() inputDropOffMarker: MapMarkersDTO;
  markerState;
  marker1;
  marker2;
  markers: MapMarkersDTO[] = new Array();
  @Output() addMarkerEvent = new EventEmitter();
  pickupMarker: MapMarkersDTO = { Latitude: null, Longitude: null, type: 'pickup' }
  dropoffMarker: MapMarkersDTO = { Latitude: null, Longitude: null, type: 'dropoff' }
  vis: boolean = true;

  pickupIcon = {
    icon: L.icon({
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      shadowSize: [41, 41],
      iconUrl: 'assets/images/marker-icon-2x-green.png',
      shadowUrl: 'assets/images/marker-shadow.png',
    })
  };
  dropoffIcon = {
    icon: L.icon({
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      shadowSize: [41, 41],
      iconUrl: 'assets/images/marker-icon-2x-red.png',
      shadowUrl: 'assets/images/marker-shadow.png',
    })
  };
  map;

  constructor() {
  }

  ngOnInit() {
    console.log('map initiated!');
    this.markerState = 'default';
    this.map = L.map('map').setView([29.966667, 31.25], 15);
    this.initMap();
    this.map.on("click", e => {
      this.actualClick(e);
    });
  }

  ngAfterViewInit(): void {
    console.log('input pickup', this.inputPickupMarker)
    if (this.inputPickupMarker.Latitude != null) {
      this.pickupMarker = this.inputPickupMarker;
      var inputPick = L.marker([this.inputPickupMarker.Latitude, this.inputPickupMarker.Longitude], this.pickupIcon);
      this.marker1 = inputPick;
      this.map.addLayer(this.marker1);
    }

    if (this.inputDropOffMarker.Latitude != null) {
      this.dropoffMarker = this.inputDropOffMarker;
      var inputDrop = L.marker([this.dropoffMarker.Latitude, this.dropoffMarker.Longitude], this.dropoffIcon);
      this.marker2 = inputDrop;
      this.map.addLayer(this.marker2);
    }
  }

  initMap() {
    const tiles = L.tileLayer('https://tiles.wmflabs.org/hikebike/{z}/{x}/{y}.png', {
      attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a>',
      maxZoom: 20,
    });
    tiles.addTo(this.map);

  }

  actualClick(e) {
    if (this.markerState == 'pickup') {
      if (this.marker1 && this.map.hasLayer(this.marker1)) {
        this.map.removeLayer(this.marker1);
      }
      if (this.marker2 && this.map.hasLayer(this.marker2)) {
        this.pickupMarker = { Latitude: e.latlng.lat, Longitude: e.latlng.lng, type: 'pickup' };
        var Marker1 = L.marker([this.pickupMarker.Latitude, this.pickupMarker.Longitude], this.pickupIcon);
        this.marker1 = Marker1;
        this.map.addLayer(this.marker1);
        Marker1.bindPopup("<b>Pickup Point</b>").openPopup();
        this.markerState = 'pickup';
      }
      else {
        this.pickupMarker = { Latitude: e.latlng.lat, Longitude: e.latlng.lng, type: 'pickup' };
        this.dropoffMarker = { Latitude: e.latlng.lat, Longitude: e.latlng.lng, type: 'pickup' };
        var Marker1 = L.marker([this.pickupMarker.Latitude, this.pickupMarker.Longitude], this.pickupIcon);
        var Marker2 = L.marker([this.dropoffMarker.Latitude, this.dropoffMarker.Longitude], this.dropoffIcon);
        this.marker1 = Marker1;
        this.marker2 = Marker2;
        this.map.addLayer(this.marker2);
        this.map.addLayer(this.marker1);
        Marker2.bindPopup("<b>Dropoff Point</b>").openPopup();
        Marker1.bindPopup("<b>Pickup Point</b>").openPopup();
        this.markerState = 'pickup';
      }
    }

    else if (this.markerState == 'drop') {
      if (this.marker2 && this.map.hasLayer(this.marker2)) {
        this.map.removeLayer(this.marker2);
      }
      this.dropoffMarker = { Latitude: e.latlng.lat, Longitude: e.latlng.lng, type: 'dropoff' };
      var Marker2 = L.marker([this.dropoffMarker.Latitude, this.dropoffMarker.Longitude], this.dropoffIcon);
      this.marker2 = Marker2;
      this.map.addLayer(this.marker2);
      Marker2.bindPopup("<b>Dropoff Point</b>").openPopup();
      this.markerState = 'drop';
    }

    this.markers = [];
    this.markers.push(this.pickupMarker, this.dropoffMarker);
    this.addMarkerEvent.emit(this.markers);
  }

  AddPickupMarker() {
    this.markerState = 'pickup';
  }

  AddDropoffMarker() {
    this.markerState = 'drop';
  }
}
