<template>
    <v-container fluid ma-0 px-0>
        <!-- Choose outlet -->
        <v-layout px-2 pb wrap>
            <v-select
                outline
                label="Choose outlet"
                :items="outlets"
                 v-model="currentOutlet"
                    item-text="name"
                    item-value="id"
                    hide-details
                    @mousedown="loadOutlet"
            >
            </v-select>
        </v-layout>
        <v-divider></v-divider>

        <!-- Book new appointment -->
        <v-layout wrap>
            <v-btn
                block flat
                to="/addbooking"
            >
                  <v-icon>
                        event
                    </v-icon>
                        Book new apoinment
            </v-btn>
        </v-layout>
        <v-divider></v-divider>

        <!-- Customer list + Booking list -->
        <v-layout row align-center>
            <v-flex md-6>
                <v-btn  block flat
                    to="/customerlist"
                >
                     <v-icon>
                        people
                    </v-icon>
                    customer list
                </v-btn>
            </v-flex>
            <v-divider vertical></v-divider>
            <v-flex md6>
                 <v-btn  block flat
                    to='/bookinglist'
                 >
                    <v-icon>
                        list
                    </v-icon>
                    booking list
                </v-btn>
            </v-flex>
        </v-layout>
        <v-divider></v-divider>

        <!-- Up-coming customer title + picker-->
        <v-layout pa-2 align-center justify-space-around
            class="text-md-center text-xs-center">
            <v-flex md8 xs7>
               <h2>Up-coming Customer</h2>
            </v-flex>
            <v-flex md4 xs5>
                <v-menu
                ref="menu1"
                :close-on-content-click="false"
                v-model="menu1"
                :nudge-right="40"
                lazy
                transition="scale-transition"
                offset-y
                full-width
                max-width="290px"
                min-width="290px"
                >
                <v-text-field
                    height="0"
                    outline
                    slot="activator"
                    v-model="dateFormatted"
                    label="Pick a date"
                    persistent-hint
                    append-icon="event"
                    @blur="date = parseDate(dateFormatted)"
                    hide-details
                ></v-text-field>
                <v-date-picker v-model="date" no-title @input="menu1 = false"></v-date-picker>
                </v-menu>

            </v-flex>
        </v-layout>

        <!-- Up-coming customer list -->
        <v-layout wrap column>
           <v-flex v-for="i in 3" :key="i">
                <up-coming-customer></up-coming-customer>
           </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex'
export default {
    data () {
        return{
            outlet: ''
            //items: ['Outlet 1', 'Outlet2', 'Outlet3']
        }
    },
     created(){
        this.loadOutlet()
    },
    computed: {
        ...mapState({
            outlets: state => state.outlet.outlets,
        }),
        currentOutlet: {
            get() {
                return this.$store.state.outlet.outlet
            },
            set(newID) {
                this.$store.dispatch('outlet/changeIDCurrentOutLet', newID)
            }
        },
    },
    methods: {
        loadOutlet (){
            this.$store.dispatch('outlet/getAllOutlet')
        },
        
    },
}
</script>
