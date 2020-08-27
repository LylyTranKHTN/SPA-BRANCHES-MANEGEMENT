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
                ></v-select>
           </v-flex>
           <v-flex xs1>
               <v-btn
                @click="addNewbed()"
                icon color="success">
                   <v-icon>add</v-icon>
                </v-btn>
           </v-flex>
        </v-layout>

        <!-- Bed list -->
        <v-layout justify-center row wrap>
            <v-flex
                v-for="bed in beds" :key="bed.index"
                xs6 sm5 md5>
                <v-card>
                    <edit-bed :bed="bed" :currentOutlet="currentOutlet.id"></edit-bed>
                    <v-card-title primary-title class="pb-0">
                        <div>
                            <h3> Bed {{bed.bedId}}</h3>
                            <h3> Room {{bed.roomID}}: {{bed.roomName}}</h3>
                        </div>
                    </v-card-title>
                    <v-card-actions>
                        <v-switch class="pt-0"
                            :readonly="true"
                            color="primary"
                            label="Enable"
                            v-model="bed.isEnable"
                        ></v-switch>
                    </v-card-actions>
                </v-card>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import EditBed from './EditBed'
export default {
    components:{
        EditBed
    },
    data(){
        return{
            // currentOulet: {id: '1', name: 'Outlet 1'},
            // outlets:[
            // //     {id: '1', name: 'Outlet 1'},
            // //     {id: '2', name: 'Outlet 2'},
            // //     {id: '3', name: 'Outlet 3'},
            // // ],
            // beds:[
            //     {name: 'bed 1', id: '1', enable: true},
            //     {name: 'bed 2', id: '2', enable: false},
            //     {name: 'bed 3', id: '3', enable: true},
            //     {name: 'bed 4', id: '4', enable: false},
            //     {name: 'bed 5', id: '5', enable: false},
            //     {name: 'bed 6', id: '6', enable: true},
            // ]
        }
    },
    created(){
        this.$store.dispatch('outlet/getAllOutlet')
        this.loadbedsByOuletID()
    },
    computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
            beds: state => state.bed.beds,
            loading: state => state.share.loading
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
        addNewbed(){
            this.$router.push('./bed/add')
        },
        editbed(){
            this.$router.push('./bed/edit')
        },
        loadbedsByOuletID(){
            var payload = {
                outletID: this.currentOutlet.id,
            }
            this.$store.dispatch('bed/loadbedByOutletID', payload)
        },
    }
}
</script>
