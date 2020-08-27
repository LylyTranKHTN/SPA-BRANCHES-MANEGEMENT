<template>
  <div class="text-xs-center">
    <v-dialog
      v-model="dialog"
      width="500"
    >
      <v-btn
        slot="activator"
        color="red lighten-2"
        icon flat
        absolute right
        @click="loadServices()"
      >
        <v-icon>edit</v-icon>
      </v-btn>

       <v-card>
            <v-card-title>
                <h2> Edit bed</h2>
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
                <v-text-field
                    v-model="bed.bedId"
                    readonly
                    label="Bed ID"
                    required
                    :rules="nameRules"
                ></v-text-field>
                <v-select
                    @mousedown="loadRooms()"
                    v-model="bed.roomID"
                    :items="rooms"
                    item-value="id"
                    item-text="name"
                    label="Room ID"
                    required
                    :rules="nameRules"
                ></v-select>

                <!-- Assigned Service -->
                 <v-select
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
                    v-model="bed.note"
                    label="Extra note"
                ></v-textarea>
            </v-form>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                    color="primary"
                    flat outline
                    @click="dialog = false"
                >
                    Back
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
  </div>
</template>
<script>
import { mapState, mapActions } from 'vuex'
  export default {
    props:{
        bed: Object,
        currentOutlet: Number
    },
    data () {
      return {
        dialog: false,
        nameRules: [
                v => !!v || 'Name bed is required'
        ]
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
        enable: {
            get(){
                return this.formatEnable()
            },
            set(payload){
                    this.setEnable(payload)
                    // enable=payload
            }
        },
        bedServices: {
            get() {
                return this.$store.state.bed.bedServices
            },
            set(newServiceID) {
                this.$store.dispatch('bed/setBedServices', newServiceID)
            }
        }
    },
    methods:{
        formatEnable(){
            if(this.bed.isEnable == true)
                return true
            else
                return false
        },
        setEnable(payload){
          if (payload == true)
                this.bed.isEnable=true
          else
                this.bed.isEnable=false
        },
        saveNewbed(){
            var payload = {
                bedID: this.bed.bedId,
                roomName:this.bed.roomName,
                roomID:this.bed.roomID,
                isEnable: this.bed.isEnable,
                serviceID: this.bedServices
            }
            this.$store.dispatch('bed/editbed', payload)
            console.log('save new bed')
        },
        loadRooms(){
            var payload = {
                outletID: this.currentOutlet,
            }
            this.$store.dispatch('room/loadRoomByOutletID', payload)
            this.onDismissed()
        },
        loadServices(){
            this.$store.dispatch('bed/loadServices')
            this.$store.dispatch('bed/loadbedBedService', this.bed.bedId)
            this.loadRooms()
        }
        ,
        onDismissed () {
            this.$store.dispatch('share/clearNoti')
        }
    }
  }
</script>