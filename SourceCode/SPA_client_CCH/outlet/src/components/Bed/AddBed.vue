<template>
    <v-container grid-list-xs>
        <v-layout row wrap justify-center>
            <v-flex xs12 sm8 md6>
                <v-card>
                    <v-card-title>
                        <h2> Add new Bed</h2>
                        <v-btn
                            color="success"
                            absolute right
                            @click="saveNewbed()"
                            :loading="loading"
                        >
                            Save
                        </v-btn>
                    </v-card-title>
                    <v-divider></v-divider>
                    <v-form class="px-3">
                        <v-select
                            @mousedown="loadRooms()"
                            v-model="room"
                            :items="rooms"
                            item-value="id"
                            item-text="name"
                            label="Room ID"
                            required
                        ></v-select>

                        <!-- Assigned Service -->
                        <v-select
                            @mousedown="loadServices()"
                            :items="services"
                            item-text="name"
                            item-value="id"
                            v-model="bedServices"
                            label="Assigned services"
                            multiple
                            chips
                            persistent-hint
                        ></v-select>
                        <v-switch class="pt-0"
                            color="primary"
                            label="Enable"
                            v-model="enable"
                        ></v-switch>
                         <v-textarea
                            v-model="note"
                            label="Extra note"
                        ></v-textarea>
                    </v-form>
                </v-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'
  export default {
    
    data () {
      return {
        room:'',
        enable: false,
        bedServices: [],
        note:''
      }
    },
    // created(){
    //     this.loadServices()
    // },
    computed: {
        ...mapState({
            rooms: state => state.room.rooms,
            loading: state => state.share.loading,
            noti: state => state.share.noti,
            services: state => state.bed.services,
            // bedServices: state => state.bed.bedServices
        }),
    },
    methods:{
        saveNewbed(){
            var payload = {
                roomID:this.room,
                isEnable: this.enable,
                serviceID: this.bedServices
            }
            this.$store.dispatch('bed/addNewbed', payload)
            console.log('save new bed')
        },
        loadRooms(){
            var payload = {
                outletID: this.currentOutlet,
            }
            this.$store.dispatch('room/loadRooms')
            this.onDismissed()
        },
        loadServices(){
            this.$store.dispatch('bed/loadServices')
        }
        ,
        onDismissed () {
            this.$store.dispatch('share/clearNoti')
        }
    }
  }
</script>