<template>
    <v-container>
        <v-card-text>
            <h2 class="text-xs-center">My bookings</h2>
        </v-card-text>

        <v-layout>
            <v-spacer></v-spacer>
                <v-select
                    outline
                    label="Choose outlet"
                    :items="outletList"
                >
                </v-select>
                   <v-btn fab dark color="green" small> <v-icon dark>add</v-icon>
                    </v-btn>
        </v-layout>

         <v-divider></v-divider>
         <v-layout>
            <v-flex xs12 sm6 md3>
            <v-text-field
                label="Customer search"
                solo
            ></v-text-field>
            </v-flex>
            <v-btn>
                <v-icon icon>search</v-icon>
            </v-btn>
         </v-layout>

         <v-divider vertical></v-divider>
          <v-layout row align-center>
            <v-flex md-6>
                <v-select
                v-model="value"
                    outline
                    label="Date Rage"
                    :items="days"
                >
                </v-select>
            </v-flex>
            <v-divider vertical></v-divider>
            <v-flex md6>
                 <v-select
                    outline
                    label="Status"
                    :items="statusList"
                >
                </v-select>
            </v-flex>

            <v-list>
            </v-list>
        </v-layout>
    </v-container>
</template>
<script>
export default {
    data () {
        return{
            outletList: ['Outlet 1', 'Outlet2', 'Outlet3'],
            days: [ 'Today','Yesterday', 'Last Week', 'Last 2 weeks', 'Last 30 days'],
            statusList: ['Stt1', 'Stt2', 'All Status']
        }
    }
}
</script>
