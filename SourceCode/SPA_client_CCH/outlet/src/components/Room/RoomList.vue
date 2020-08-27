<template>
    <v-container grid-list-xl>
        <!-- Choose outlet -->
        <v-layout row wrap justify-center>
           <v-flex xs9 sm7 md7>
                <v-select
                    :items="outlets"
                    v-model="currentOutlet"
                    item-text="name"
                    item-value="id"
                    return-object
                    label="Choose outlet"
                    outline hide-details
                    @change="loadRoomsByOuletID()"
                ></v-select>
           </v-flex>
           <v-flex xs1>
               <v-btn
                @click="addNewRoom()"
                icon color="success">
                   <v-icon>add</v-icon>
                </v-btn>
           </v-flex>
        </v-layout>

        <!-- Noti -->
        <v-layout row wrap v-if="noti != null">
            <app-alert
                :noti="noti"
                @dismissed="onDismissed"
                >
            </app-alert>
        </v-layout>

        <!-- Loading -->
        <v-layout row wrap v-if="loading">
            <v-progress-linear width="4" :indeterminate="loading"></v-progress-linear>
        </v-layout>

        <!-- Room list -->
        <v-layout justify-center row wrap>
            <v-flex
                v-for="room in rooms" :key="room.index"
                xs6 sm5 md5>
                <v-card>
                    <edit-room :room="room"></edit-room>
                    <v-card-title primary-title class="pb-0">
                        <div>
                            <h3> Name: {{room.name}}</h3>
                            <span>RoomID: {{room.id}}</span>
                            <br>
                            <span>Beds: {{room.numofbed}}</span>
                        </div>
                    </v-card-title>
                    <v-card-actions>
                        <v-switch class="pt-0"
                            :readonly="true"
                            color="primary"
                            label="Enable"
                            v-model="room.deleted == 0"
                        ></v-switch>
                    </v-card-actions>
                </v-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import EditRoom from './EditRoom'
export default {
    components:{
        EditRoom
    },
    data(){
        return{
            // currentOulet: {id: '1', name: 'Outlet 1'},
            // outlets:[
            // //     {id: '1', name: 'Outlet 1'},
            // //     {id: '2', name: 'Outlet 2'},
            // //     {id: '3', name: 'Outlet 3'},
            // // ],
            // rooms:[
            //     {name: 'Room 1', id: '1', enable: true},
            //     {name: 'Room 2', id: '2', enable: false},
            //     {name: 'Room 3', id: '3', enable: true},
            //     {name: 'Room 4', id: '4', enable: false},
            //     {name: 'Room 5', id: '5', enable: false},
            //     {name: 'Room 6', id: '6', enable: true},
            // ]
        }
    },
    created(){
        this.$store.dispatch('outlet/getAllOutlet')
        this.loadRoomsByOuletID()
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
        addNewRoom(){
            this.$router.push('./room/add')
        },
        editRoom(){
            this.$router.push('./room/edit')
        },
        loadRoomsByOuletID(){
            var payload = {
                outletID: this.currentOutlet.id,
            }
            this.$store.dispatch('room/loadRoomByOutletID', payload)
            this.onDismissed()
        },
        onDismissed () {
            this.$store.dispatch('share/clearNoti')
        }
    }
}
</script>
