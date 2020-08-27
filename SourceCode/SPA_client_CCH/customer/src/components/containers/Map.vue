<template>
  <div>
    <gmap-map :center="center" :zoom="12" style="width:100%;  height: 500px;">
      <gmap-marker
        :key="index"
        v-for="(m, index) in markers"
        :position="m.position"
        @click="center=m.position"
      ></gmap-marker>
    </gmap-map>
  </div>
</template>

<script>
import { mapState, mapActions } from "vuex";
export default {
  data() {
    return {
      // default to Montreal to keep it simple
      // change this to whatever makes sense
      center: { lat: 10.854029, lng: 106.6284 },
      markers: [],
      places: [],
    };
  },
  computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
        })
    },

  created() {
    //console.log(this.outlets)
   // this.initMap();
  },

  mounted() {
    this.geolocate();
    this.addMarker();
  },

  methods: {
    // receives a place object via the autocomplete component
    setPlace(place) {
      this.currentPlace = place;
    },
    addMarker() {
      console.log(outlets)
      for (let i = 0; i < this.outlets.length; i++){
        this.getLatitudeLongitude(outlets[i].address)
        console.log(outlets[i])
      }
    },
    geolocate() {
      navigator.geolocation.getCurrentPosition(position => {
        this.center = {
          lat: position.coords.latitude,
          lng: position.coords.longitude
        };
      });
      console.log("<<<<", this.center);
      this.markers.push({ position: this.center });
      console.log("------", this.markers);
    },
    getLatitudeLongitude(address) {
      // Initialize the Geocoder
      geocoder = new google.maps.Geocoder();
      if (geocoder) {
          geocoder.geocode({
              'address': address
          }, function (results, status) {
              if (status == google.maps.GeocoderStatus.OK) {
                var o = {
                  lat: results[0].geometry.location.lat(),
                  lng: results[0].geometry.location.lng()
                }
                this.markers.push({position: o})
                console.log(o)
              }
          });
    }
}
  }
};
</script>