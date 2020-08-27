<template>
    <v-container fluid ma-0 px-0>
        <!-- Image -->
        <v-img>

        </v-img>
        <v-divider></v-divider>

        <!-- Book new appointment -->
        <v-layout wrap>
            <v-btn
                block flat
                to="/add_new_booking"
            >
                <span>Book new apoinment</span>
                <v-icon>
                    event
                </v-icon>
            </v-btn>
        </v-layout>
        <v-divider></v-divider>

        <!-- Customer list + Booking list -->
        <v-layout row align-center>
            <v-flex md-6>
                <v-btn  block flat
                    to="/outlets"
                >
                     <v-icon>
                        people
                    </v-icon>
                    outlet list
                </v-btn>
            </v-flex>
            <v-divider vertical></v-divider>
            <v-flex md6>
                 <v-btn  block flat
                    to='/booking_history'
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
      <v-layout row align-center>
            <v-flex md4 xs4>
                <v-btn  block flat
                    to="/notification_list"
                >
                     <v-icon>

                    </v-icon>
                    News
                </v-btn>
            </v-flex>
            <v-divider vertical></v-divider>
            <v-flex md4 xs4>
                 <v-btn  block flat
                    to='/'
                 >
                    <v-icon>

                    </v-icon>
                        Calorine intake
                </v-btn>
            </v-flex>
              <v-divider vertical></v-divider>
            <v-flex md4 xs4>
                 <v-btn  block flat
                    to='/bookinglist'
                 >
                    <v-icon>
                        share
                    </v-icon>
                    share
                </v-btn>
            </v-flex>
        </v-layout>
        <v-divider></v-divider>
    </v-container>
</template>

<script>
export default {
    data () {
        return{
        }
    },
}
</script>
