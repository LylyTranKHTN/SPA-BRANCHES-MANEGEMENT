<template>
    <v-container>
        <!-- booking -->
        <v-layout v-for="booking in bookings" :key="booking.index" column outline>
            <!-- id -->
            <v-flex>
                <v-layout>
                    <v-flex xs4 md2>
                        <v-subheader> Booking: </v-subheader>
                    </v-flex>  
                    <v-flex xs6 md6>
                        <v-subheader> {{booking.id}} </v-subheader>
                    </v-flex>   
                </v-layout>
            </v-flex>
            <!-- Session date -->
            <v-flex>
                <v-layout>
                    <v-flex xs4 md2>
                        <v-subheader> Session Date: </v-subheader>
                    </v-flex>  
                    <v-flex xs6 md6>
                        <v-subheader> {{booking.date}} </v-subheader>
                    </v-flex>   
                </v-layout>
            </v-flex>
            <!-- Outlet -->
            <v-flex>
                <v-layout>
                    <v-flex xs4 md2>
                        <v-subheader> Outlet: </v-subheader>
                    </v-flex>  
                    <v-flex xs6 md6>
                        <v-subheader> {{booking.outlet}} </v-subheader>
                    </v-flex>   
                </v-layout>
            </v-flex>
            <!-- Therapist -->
            <v-flex>
                <v-layout>
                    <v-flex xs4 md2>
                        <v-subheader> Therapist: </v-subheader>
                    </v-flex>  
                    <v-flex xs6 md6>
                        <v-subheader> {{booking.therapist}} </v-subheader>
                    </v-flex>   
                </v-layout>
            </v-flex>
            <v-flex><br /></v-flex>
            <!-- text before after -->
            <v-flex>
                <v-layout>
                    <v-flex xs6 md6 class="text-xs-center"><h4>Before</h4></v-flex>
                    <v-flex xs6 md6 class="text-xs-center"><h4>After</h4></v-flex>
                </v-layout>
            </v-flex>
            <!-- image -->
            <v-flex>
                <v-layout>
                    <v-flex xs6 md6 class="text-xs-center">
                        <img :src=booking.before>
                    </v-flex>
                    <v-flex xs6 md6 class="text-xs-center">
                        <img :src=booking.after1>
                    </v-flex>
                </v-layout>
            </v-flex>
            <v-flex>
                <v-layout>
                    <v-flex xs6 md6 class="text-xs-center"></v-flex>
                    <v-flex xs6 md6 class="text-xs-center">
                        <img :src=booking.after2>
                    </v-flex>
                </v-layout>
            </v-flex>
            <!-- outline -->
            <v-divider></v-divider>
        </v-layout>
    </v-container>
</template>

<script>
export default {
    data () {
        return{
            bookings: [
                {
                    id: '01',
                    date: '12/06/2018',
                    outlet: 'Ngoc Trinh',
                    therapist: 'Tommy',
                    before: 'url',
                    after1: 'url1',
                    after2:'url2',
                },
                {
                    id: '02',
                    date: '12/06/2018',
                    outlet: 'Ngoc Trinh',
                    therapist: 'Tommy',
                    before: 'url',
                    after1: 'url1',
                    after2:'url2',
                },
                {
                    id: '03',
                    date: '12/06/2018',
                    outlet: 'Ngoc Trinh',
                    therapist: 'Tommy',
                    before: 'url',
                    after1: 'url1',
                    after2:'url2',
                }
            ]
        }
    }
}
</script>
