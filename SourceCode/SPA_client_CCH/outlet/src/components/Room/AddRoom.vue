<template>
    <v-container grid-list-xs>
        <!-- Noti -->
        <v-layout row wrap v-if="noti != null">
            <app-alert
                :noti="noti"
                @dismissed="onDismissed"
                >
            </app-alert>
        </v-layout>
        <v-layout row wrap justify-center>
            <v-flex xs12 sm8 md6>
                <v-card>
                    <v-card-title>
                        <h2> Add new room</h2>
                        <v-btn
                            color="success"
                            absolute right
                            @click="saveNewRoom()"
                            :disabled="name == ''"
                            :loading="loading"
                        >
                            Save
                        </v-btn>
                    </v-card-title>
                    <v-divider></v-divider>
                    <v-form class="px-3">
                        <v-text-field
                            v-model="name"
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
    data(){
        return{
            name: '',
            enable: false,
            note:'',
            nameRules: [
                v => !!v || 'Name room is required'
            ]
        }
    },
    computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
            rooms: state => state.room.rooms,
            loading: state => state.share.loading,
            noti: state => state.share.noti
        }),
        currentOutlet: {
            get() {
                return this.$store.state.outlet.outlet
            },
            set(newOutlet) {
                this.$store.dispatch('outlet/changeIDCurrentOutLet', newOutlet)
            }
        },
    },
    methods:{
        saveNewRoom(){
            var deleted = null
            if (this.enable == true)
                deleted=0
            else
                deleted=1
            var payload = {
                name: this.name,
                numofbed: 7,
                outlet: this.currentOutlet.id,
                deleted: deleted,
                recordversion: 0
            }
            this.$store.dispatch('room/addNewRoom', payload)
            console.log('save new room')
        },
        onDismissed () {
            this.$store.dispatch('share/clearNoti')
            this.name = '',
            this.enable = false,
            this.note = ''
        }
    }
}
</script>
