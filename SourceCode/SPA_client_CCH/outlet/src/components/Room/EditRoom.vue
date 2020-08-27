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
      >
        <v-icon>edit</v-icon>
      </v-btn>

       <v-card>
            <!-- Noti -->
            <v-layout row wrap v-if="noti != null">
                <app-alert
                    :noti="noti"
                    @dismissed="onDismissed"
                    >
                </app-alert>
            </v-layout>
            <v-card-title>
                <h2> Edit room</h2>
                <v-btn
                    color="success"
                    absolute right
                    @click="saveNewRoom(room)"
                    :loading="loading"
                >
                    Save
                </v-btn>
            </v-card-title>
            <v-divider></v-divider>
            <v-form class="px-3">
                <v-text-field
                    v-model="room.name"
                    label="Room name"
                    required
                    :rules="nameRules"
                ></v-text-field>
                <v-switch class="pt-0"
                    color="primary"
                    label="Enable"
                    v-model="enable"
                ></v-switch>
                    <v-textarea
                    v-model="room.note"
                    label="Extra note"
                ></v-textarea>
            </v-form>
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                    color="primary"
                    flat outline
                    @click="dialog = false; onDismissed()"
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
        room: Object
    },
    data () {
      return {
        dialog: false,
        nameRules: [
                v => !!v || 'Name room is required'
        ]
      }
    },
    computed: {
        ...mapState({
            loading: state => state.share.loading,
            noti: state => state.share.noti
        }),
        enable: {
            get(){
                return this.formatEnable()
            },
            set(payload){
                    this.setEnable(payload)
                    // enable=payload
            }

        }
    },
    methods:{
        formatEnable(){
            if(this.room.deleted == 0)
                return true
            else
                return false
        },
        setEnable(payload){
          if (payload == true)
                this.room.deleted=0
          else
                this.room.deleted=1
        },
        saveNewRoom(room){
            
            var payload = {
                id: room.id,
                name:room.name,
                outlet:room.outlet,
                deleted: room.deleted,
                recordversion: room.recordversion,
            }
            this.$store.dispatch('room/editRoom', payload)
            console.log('save new room')
        },
         onDismissed () {
            this.$store.dispatch('share/clearNoti')
        }
    }
  }
</script>